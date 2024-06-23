using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

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
        var db = _dbSet
            .Select(shopping => new ShoppingGetResponse {
                Id = shopping.Id,
                Name = shopping.Name,
                Price = shopping.Price,
            });
        
        // Sort
        string orderByDefault = "Id";
        string orderBy = orderByDefault;

        if (request.Sort is not null)
        {
            var sortColumns = new Dictionary<string, OrderBy?>
            {
                { "Id", request.Sort.Id },
                { "Name", request.Sort.Name },
                { "Price", request.Sort.Price }
            };

            foreach (KeyValuePair<string, OrderBy?> sortColumn in sortColumns.Where(sortColumn => sortColumn.Value is not null))
            {
                var orderByColumn = $"{sortColumn.Key} {sortColumn.Value}";

                if (orderBy == orderByDefault)
                {
                    orderBy = orderByColumn;
                }
                else
                {
                    orderBy = $"{orderBy}, {orderByColumn}";
                }
            }
        }

        db = db.OrderBy(orderBy);

        // Pagination
        var items = await db
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
