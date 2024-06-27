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
        int count = await _dbSet.CountAsync();

        // TODO: validar que el request.Pagination.Offset no supere el totalPages
        // TODO: pasar este método al generic
        var db = _dbSet
            .Select(shopping => new ShoppingGetResponse {
                Id = shopping.Id,
                Name = shopping.Name,
                Price = shopping.Price,
            });

        // Filter
        if (request.Filter is not null)
        {
            if (request.Filter.Id is not null)
            {
                db = db.Where(shopping => shopping.Id == request.Filter.Id);
            }

            if (request.Filter.Name is not null)
            {
                db = db.Where(shopping => shopping.Name == request.Filter.Name);
            }

            if (request.Filter.Price is not null)
            {
                db = db.Where(shopping => shopping.Price == request.Filter.Price);
            }

            if (request.Filter.PriceLessThan is not null)
            {
                db = db.Where(shopping => shopping.Price > request.Filter.PriceLessThan);
            }

            if (request.Filter.PriceGreaterThan is not null)
            {
                db = db.Where(shopping => shopping.Price < request.Filter.PriceGreaterThan);
            }

            // Filter search
            if (request.Filter.Search is not null)
            {
                db = db.Where(
                    shopping =>
                        shopping.Id.ToString().Contains(request.Filter.Search) ||
                        shopping.Name.Contains(request.Filter.Search) ||
                        shopping.Price.ToString().Contains(request.Filter.Search)
                );
            }
        }

        // Sort
        string orderByDefault = "Id";
        string orderBy = orderByDefault;

        if (request.Sort is not null)
        {
            Dictionary<string, OrderBy?> sortColumns = new Dictionary<string, OrderBy?>
            {
                { "Id", request.Sort.Id },
                { "Name", request.Sort.Name },
                { "Price", request.Sort.Price }
            };

            foreach (KeyValuePair<string, OrderBy?> sortColumn in sortColumns.Where(sortColumn => sortColumn.Value is not null))
            {
                string orderByColumn = $"{sortColumn.Key} {sortColumn.Value}";

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
