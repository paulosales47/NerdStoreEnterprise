using Microsoft.OpenApi.Models;

namespace NSE.Identidade.API.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services) 
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc(name: "v1", new OpenApiInfo
                {
                    Title = "NerdStore Enterprise Identity API",
                    Description = "API utilizada para ações de autenticação do sistema",
                    Contact = new OpenApiContact { Name = "Paulo Sampaio", Email = "paulohenrique.sales47@gmail.com" },
                    License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                });
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app) 
        {
            app.UseSwagger();
            app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
            return app; 
        }
    }
}
