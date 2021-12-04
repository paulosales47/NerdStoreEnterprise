using NSE.Identidade.API.Configuration;
using NSE.Identidade.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
    builder.Configuration.AddUserSecrets<UserSecret>();

//SERVICES
IServiceCollection services = builder.Services; 
services.AddIdentityConfiguration(builder.Configuration);
services.AddApiConfiguration();
services.AddSwaggerConfiguration();

//CONFIGURE
var app = builder.Build();
app.UseApiConfiguration();
app.UseSwaggerConfiguration();
app.MapControllers();
app.Run();
