using bookApi.Domain.Enums;
using bookApi.WebApi.Contracts.Responses;

namespace bookApi.WebApi.Endpoints.CartOrder.Responses;

public class CartOrderResponse : BaseResponse
{
    public Guid? OrderId { get; set; }
    public Guid? UserId { get; set; }
    public string? UserName { get; set; }
    public DateOnly? OrderDate { get; set; }
    public int? TotalQty { get; set; }
    public decimal? TotalPrice { get; set; }
    public OrderStatus? Status { get; set; }
}