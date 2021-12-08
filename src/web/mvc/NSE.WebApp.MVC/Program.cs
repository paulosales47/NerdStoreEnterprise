using NSE.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);

//SERVICE
IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;

services.AddIdentityConfiguration();
services.AddMvcConfiguration(configuration);
services.RegisterServices(configuration);

//CONFIGURE
var app = builder.Build();

if (!app.Environment.IsDevelopment())
    app.UseMvcConfigurationDevelopment();

app.UseMvcConfiguration();
app.Run();
