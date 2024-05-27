using Microsoft.AspNetCore.Mvc;

namespace bookApi.WebApi.Endpoints.CartManagement.Requests;

public class GetCartByIdRequest
{
    [FromRoute(Name = "CartId")] public Guid CartId { get; set; }
}