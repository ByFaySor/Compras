using Microsoft.EntityFrameworkCore;

using Compras.Models;

namespace Compras.Repositories;

public class ShoppingRepository :  GenericRepository<Shopping, long>, IShoppingRepository
{
    public ShoppingRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Shopping>> GetAll()
    {
        return await _dbSet
            .Select(shopping => new Shopping {
                Id = shopping.Id,
                Name = shopping.Name,
                Price = shopping.Price,
            })
            .ToListAsync();
    }
}
