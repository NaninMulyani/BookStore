using bookApi.Domain.Enums;
using bookApi.Shared.Abstractions.Queries;

namespace bookApi.WebApi.Endpoints.CartOrder.Requests;

public class GetAllCartOrderPaginatedRequest : BasePaginationCalculation
{
    public string? UserName { get; set; }
    public DateOnly? OrderDate { get; set; }
    public OrderStatus ? Status { get; set; }
}