using MediatR;
using NSE.Clientes.API.Configuration;

var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;

services.AddApiConfiguration(configuration);
services.AddSwaggerConfiguration();
services.AddMediatR(typeof(Program));
services.RegisterServices();
services.AddMessageBusConfiguration(configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.UseSwaggerConfiguration();

app.UseApiConfiguration();
app.Run();
