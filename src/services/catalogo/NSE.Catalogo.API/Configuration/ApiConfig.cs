using Microsoft.EntityFrameworkCore;
using NSE.Catalogo.API.Data;
using NSE.WebApi.Core.Identidade;

namespace NSE.Catalogo.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<CatalagoContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.RegisterServices();
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
