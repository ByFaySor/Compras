using Microsoft.EntityFrameworkCore;

using Compras.Models;
using Compras.Shared.Pagination;
using Compras.Models.DTOs;

namespace Compras.Repositories;

public class ShoppingRepository :  GenericRepository<Shopping, long>, IShoppingRepository
{
    public ShoppingRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PaginationResponseModel<ShoppingGetResponse>> GetAll(PaginationRequestModel request)
    {
        var count = await _dbSet.CountAsync();

        // TODO: validar que el request.Pagination.Offset no supere el totalPages
        // TODO: pasar este método al generic
        var items = await _dbSet
            .Select(shopping => new ShoppingGetResponse {
                Id = shopping.Id,
                Name = shopping.Name,
                Price = shopping.Price,
            })
            .Skip((request.Pagination.Page - 1) * request.Pagination.Limit)
            .Take(request.Pagination.Limit)
            .ToListAsync();

        return new PaginationResponseModel<ShoppingGetResponse>(
            count,
            items,
            request.Pagination.Page,
            request.Pagination.Limit
        );
    }
}
