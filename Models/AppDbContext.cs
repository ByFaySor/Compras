using Microsoft.EntityFrameworkCore;

namespace Compra.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /* protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        } */

        public DbSet<Shopping> Shoppings { get; set; }
    }
}
