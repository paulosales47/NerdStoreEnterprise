using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;
using Polly;
using Polly.Extensions.Http;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services) 
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();

            var retryWaitPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(sleepDurations: new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                }, onRetry:(outcome, timeSpan, retryCount, context) => { 
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Tentativa: {retryCount}");
                    Console.ForegroundColor = ConsoleColor.White;
                });

            services.AddHttpClient<ICatalogoService, CatalogoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(retryWaitPolicy);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUser, AspNetUser>();
            
        }
    }
}
