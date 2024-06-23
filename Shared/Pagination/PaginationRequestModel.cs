using System.ComponentModel.DataAnnotations;

namespace Compras.Shared.Pagination;

public class PaginationModel
{
    [Required]
    [Range(1, Int32.MaxValue)]
    public int Page { get; set; }

    [Required]
    [Range(1, Int32.MaxValue)]
    public int Limit { get; set; }
}

public class PaginationRequestModel
{
    public required PaginationModel Pagination { get; set; }
}

public class PaginationResponseModel<T>
{
    public int Page { get; private set; }
    public int TotalPages { get; private set; }
    public int Total { get; private set; }
    public List<T> Data { get; private set; }

    public PaginationResponseModel(int count, List<T> items, int pageIndex, int pageSize)
    {
        Page = pageIndex;
        TotalPages = (int) Math.Ceiling(count / (double) pageSize);
        Total = count;

        Data = items;
    }
}
