using App.Enums;
using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Data;

public class TodosDbContext : DbContext
{
    public TodosDbContext(DbContextOptions<TodosDbContext> options) : base(options) { }

    public DbSet<TodoModel> Todos { get; set; }
}