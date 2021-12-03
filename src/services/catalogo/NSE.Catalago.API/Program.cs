using Microsoft.EntityFrameworkCore;
using NSE.Catalago.API.Data;
using NSE.Catalago.API.Models;
using NSE.Catalago.API.Repository;

//SERVICES
var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;


services.AddDbContext<CatalagoContext>(
    options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

services.AddScoped<IProdutoRepository, ProdutoRepository>();
services.AddScoped<CatalagoContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//CONFIGURE
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
