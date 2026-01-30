using App.Data;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class TodosDbContextFactory
    : IDesignTimeDbContextFactory<TodosDbContext>
{
    public TodosDbContext CreateDbContext(string[] args)
    {
        // Load .env explicitly
        Env.Load();

        var host = Environment.GetEnvironmentVariable("MYSQL_HOST")
            ?? "localhost";

        var port = Environment.GetEnvironmentVariable("MYSQL_PORT") ?? "3306";
        var database = Environment.GetEnvironmentVariable("MYSQL_DATABASE")
            ?? "myapp_dev";

        var user = Environment.GetEnvironmentVariable("MYSQL_USER")
            ?? "myapp";

        var password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD")
            ?? "myapp_password";

        var connectionString =
            $"Server={host};" +
            $"Port={port};" +
            $"Database={database};" +
            $"User={user};" +
            $"Password={password}";

        var options = new DbContextOptionsBuilder<TodosDbContext>()
            .UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            )
            .Options;

        return new TodosDbContext(options);
    }
}