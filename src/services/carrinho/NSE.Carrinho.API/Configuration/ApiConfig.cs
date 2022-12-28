using NSE.WebApi.Core.Identidade;

namespace NSE.Carrinho.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("Total", policy => {
                    policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }

        public static void UseApiConfiguration(this WebApplication app) 
        {
            app.UseHttpsRedirection();
            app.UseCors("Total");
            app.UseAuthConfiguration();
            app.MapControllers();
        }
    }
}
