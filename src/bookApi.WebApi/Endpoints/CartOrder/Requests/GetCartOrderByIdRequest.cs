using Microsoft.AspNetCore.Mvc;

namespace bookApi.WebApi.Endpoints.CartOrder.Requests;

public class GetCartOrderByIdRequest
{
    [FromRoute(Name = "CartOrderId")] public Guid OrderId { get; set; }
}