using Microsoft.EntityFrameworkCore;
using NSE.Clientes.API.Data;

namespace NSE.Clientes.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddControllers();

            services.AddDbContext<ClienteContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddCors(options => 
            {
                options.AddPolicy("Total", builder =>
                {
                    builder
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

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
