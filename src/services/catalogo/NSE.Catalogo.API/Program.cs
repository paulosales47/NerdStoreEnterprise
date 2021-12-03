using NSE.Catalogo.API.Configuration;

//SERVICES
var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;

services.AddApiConfiguration(configuration);

services.AddSwaggerConfiguration();


//CONFIGURE
var app = builder.Build();

if(app.Environment.IsDevelopment())
    app.UserSwaggerConfiguration();

app.UseApiConfiguration();
app.Run();
