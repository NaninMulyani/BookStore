using bookApi.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace bookApi.WebApi.Endpoints.CartOrder.Requests;

public class UpdateStatusCartOrderRequest
{
    [FromRoute(Name = "CartOrderId")] public Guid CartOrderId { get; set; }
    [FromRoute(Name = "Status")] public OrderStatus Status { get; set; }
}