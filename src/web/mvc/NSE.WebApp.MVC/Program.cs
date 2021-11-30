using NSE.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);

//SERVICE
IServiceCollection services = builder.Services;

services.AddIdentityConfiguration();
services.AddControllersWithViews();


//CONFIGURE
var app = builder.Build();

if (!app.Environment.IsDevelopment())
    app.UseMvcConfigurationDevelopment();

app.UseMvcConfiguration();
app.Run();
