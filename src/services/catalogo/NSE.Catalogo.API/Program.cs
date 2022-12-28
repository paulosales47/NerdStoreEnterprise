using NSE.Catalogo.API.Configuration;
using NSE.WebApi.Core.Identidade;

//SERVICES
var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;

services.AddApiConfiguration(configuration);
services.AddJwtConfiguration(configuration);
services.AddSwaggerConfiguration();
services.RegisterServices();

//CONFIGURE
var app = builder.Build();

if(app.Environment.IsDevelopment())
    app.UseSwaggerConfiguration();

app.UseApiConfiguration();
app.Run();
