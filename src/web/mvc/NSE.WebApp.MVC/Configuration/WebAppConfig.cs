﻿using Microsoft.AspNetCore.Localization;
using NSE.WebApp.MVC.Extensions;
using System.Globalization;

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
            var supportedCultures = new[] { new CultureInfo("pt-BR") };

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityConfiguration();
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
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
