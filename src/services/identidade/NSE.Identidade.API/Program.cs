using NSE.Identidade.API.Configuration;
using NSE.Identidade.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;

if (builder.Environment.IsDevelopment())
    builder.Configuration.AddUserSecrets<UserSecret>();

//SERVICES
services.AddIdentityConfiguration(builder.Configuration);
services.AddApiConfiguration();
services.AddSwaggerConfiguration();
services.AddMessageBusConfiguration(configuration);

//CONFIGURE
var app = builder.Build();
app.UseApiConfiguration();
app.UseSwaggerConfiguration();
app.MapControllers();
app.Run();
