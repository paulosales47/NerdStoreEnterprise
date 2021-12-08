using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Configuration
{
    public static class WebAppConfig
    {
        public static void AddMvcConfiguration(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddControllersWithViews();

            var endPointSection = configuration.GetSection(nameof(EndPointSettings));
            services.Configure<EndPointSettings>(endPointSection);
        }

        public static void UseMvcConfiguration(this WebApplication app) 
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityConfiguration();
            app.UseExceptionHandler("/erro/500");
            app.UseStatusCodePagesWithRedirects("/erro/{0}");
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHsts();

            app.MapControllerRoute(name: "default", pattern: "{controller=Catalogo}/{action=Index}/{id?}");
        }

        public static void UseMvcConfigurationDevelopment(this IApplicationBuilder app) 
        {
            app.UseDeveloperExceptionPage();
        }
    }
}
