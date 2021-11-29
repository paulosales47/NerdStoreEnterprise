namespace NSE.Identidade.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app) 
        {
            app.UseHttpsRedirection();
            app.UseIdentityConfiguration();
            return app;
        }

        public static IApplicationBuilder UseApiConfigurationDevelopment(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
