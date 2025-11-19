using Microsoft.EntityFrameworkCore;
using WarGame.Api.Endpoints;
using WarGame.Domain.Implementation;
using WarGame.Domain.Interfaces;
using WarGame.Model.Configuration;
using WarGame.Model.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// register DbContext
builder.Services.AddDbContext<WargameContext>(
    options => options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion("9.4")
    )
);

// register repositories
builder.Services.AddTransient<IRepository<Tank>, RepositoryBase<Tank>>();
builder.Services.AddTransient<IRepository<Country>, RepositoryBase<Country>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    // enable Swagger UI based on the OpenApi document
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

var api = app.MapGroup("/api");
api.MapTankEndpoints();
api.MapCountryEndpoints();

app.Run();
