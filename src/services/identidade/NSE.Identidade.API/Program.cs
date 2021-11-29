using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NSE.Identidade.API.Data;
using NSE.Identidade.API.Extensions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//SERVICES

builder.Services.AddDbContext<ApplicationDbContext>(optionsAction: options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddErrorDescriber<IdentityMensagensPortugues>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


var jwtSection = builder.Configuration.GetSection(nameof(JwtConfig));
builder.Services.Configure<JwtConfig>(jwtSection);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(bearerOptions =>
{
    var jwtConfig = jwtSection.Get<JwtConfig>();

    bearerOptions.RequireHttpsMetadata = true;
    bearerOptions.SaveToken = true;
    bearerOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.key)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = jwtConfig.Audience,
        ValidIssuer = jwtConfig.Issuer,
    };
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc(name: "v1", new OpenApiInfo
    {
        Title = "NerdStore Enterprise Identity API",
        Description = "API utilizada para ações de autenticação do sistema",
        Contact = new OpenApiContact { Name = "Paulo Sampaio", Email = "paulohenrique.sales47@gmail.com" },
        License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
    });
});

//CONFIGURE

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
