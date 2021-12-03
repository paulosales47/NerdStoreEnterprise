using Microsoft.OpenApi.Models;

namespace NSE.Catalogo.API.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services) 
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc(name: "v1", new OpenApiInfo
                {
                    Title = "NerdStore Enterprise Catálogo API",
                    Description = "API utilizada para fornecer o catálogo do sistema",
                    Contact = new OpenApiContact { Name = "Paulo Sampaio", Email = "paulohenrique.sales47@gmail.com" },
                    License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                });
            });
        }

        public static void UserSwaggerConfiguration(this IApplicationBuilder app) 
        {
            app.UseSwagger();
            app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
        }
    }
}
