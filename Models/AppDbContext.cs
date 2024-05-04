using Microsoft.EntityFrameworkCore;

namespace Compra.Models;

public class AppDbContext : DbContext
{
    public DbSet<Shopping> Shoppings { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public override int SaveChanges()
    {
        AddTimestamps();

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        AddTimestamps();

        return base.SaveChangesAsync();
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseEntity<long> && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow; // current datetime

            if (entity.State == EntityState.Added)
            {
                ((BaseEntity<long>) entity.Entity).CreatedAt = now;
            }

            ((BaseEntity<long>) entity.Entity).UpdatedAt = now;
        }
    }
}
