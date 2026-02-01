using CategoryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoryApi.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEntity).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
