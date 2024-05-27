using bookApi.Shared.Abstractions.Queries;

namespace bookApi.WebApi.Endpoints.InventoryManagement.Requests;

public class GetAllInventoryPaginatedRequest : BasePaginationCalculation
{
    public string? BookTitle { get; set; }
    public int? Qty { get; set; }
}