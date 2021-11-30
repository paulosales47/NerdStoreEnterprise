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

            app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
        }

        public static void UseMvcConfigurationDevelopment(this IApplicationBuilder app) 
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
    }
}
