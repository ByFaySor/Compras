using Microsoft.EntityFrameworkCore;

namespace Compra.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Shopping> Shoppings { get; set; }
}
