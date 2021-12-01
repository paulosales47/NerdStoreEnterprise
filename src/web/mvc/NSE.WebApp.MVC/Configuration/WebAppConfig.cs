using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Configuration
{
    public static class WebAppConfig
    {
        public static void UseMvcConfiguration(this WebApplication app) 
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityConfiguration();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseExceptionHandler("/erro/500");
            app.UseStatusCodePagesWithRedirects("/erro/{0}");
            app.UseHsts();

            app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
        }

        public static void UseMvcConfigurationDevelopment(this IApplicationBuilder app) 
        {
            app.UseDeveloperExceptionPage();
        }
    }
}
