using App.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Read env vars (runtime only)
var host = Environment.GetEnvironmentVariable("MYSQL_HOST")
    ?? throw new InvalidOperationException("MYSQL_HOST is not set");

var port = Environment.GetEnvironmentVariable("MYSQL_PORT") ?? "3306";
var database = Environment.GetEnvironmentVariable("MYSQL_DATABASE")
    ?? throw new InvalidOperationException("MYSQL_DATABASE is not set");

var user = Environment.GetEnvironmentVariable("MYSQL_USER")
    ?? throw new InvalidOperationException("MYSQL_USER is not set");

var password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD")
    ?? throw new InvalidOperationException("MYSQL_PASSWORD is not set");

var connectionString =
    $"Server={host};" +
    $"Port={port};" +
    $"Database={database};" +
    $"User={user};" +
    $"Password={password}";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<TodosDbContext>(options =>
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 0))
    ));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TodosDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
