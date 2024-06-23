using Compras.Models;
using Compras.Models.DTOs;
using Compras.Shared.Pagination;

namespace Compras.Repositories;

public interface IShoppingRepository : IGenericRepository<Shopping, long>
{
    Task<PaginationResponseModel<ShoppingGetResponse>> GetAll(PaginationRequestModel request);
}
