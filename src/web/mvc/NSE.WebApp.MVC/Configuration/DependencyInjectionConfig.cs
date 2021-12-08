using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();

            services.AddHttpClient("CatalogoService", options =>
            {
                options.BaseAddress = new Uri(
                    configuration.GetSection("EndPointSettings").GetSection("CatalogoUrl").Value);
            })
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddTypedClient(Refit.RestService.For<ICatalogoServiceRefit>);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUser, AspNetUser>();
            
        }
    }
}
