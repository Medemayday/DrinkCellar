using DrinkCellar.Core.Interfaces.Repositories;
using DrinkCellar.Core.Interfaces.Services;
using DrinkCellar.Core.Services;
using DrinkCellar.Infrastructure.Data;
using DrinkCellar.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DrinkCellarDbContext>
        (options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DrinkCellarDb")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICellarRepository, CellarRepository>();
builder.Services.AddScoped<IDrinkTypeRepository, DrinkTypeRepository>();
builder.Services.AddScoped<IDrinkRepository, DrinkRepository>();

builder.Services.AddScoped<ICellarService, CellarService>();
builder.Services.AddScoped<IDrinkTypeService, DrinkTypeService>();
builder.Services.AddScoped<IDrinkService, DrinkService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
