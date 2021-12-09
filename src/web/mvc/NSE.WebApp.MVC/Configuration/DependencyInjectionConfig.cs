using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;
using Polly;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services) 
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();

            services.AddHttpClient<ICatalogoService, CatalogoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddTransientHttpErrorPolicy(
                    builder => builder.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUser, AspNetUser>();
            
        }
    }
}
