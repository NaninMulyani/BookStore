using bookApi.Core.Abstractions;
using bookApi.WebApi.Common;
using bookApi.WebApi.Endpoints.CartOrder.Responses;
using bookApi.WebApi.Endpoints.CartOrder.Requests;
using bookApi.WebApi.Endpoints.CartOrder.Scopes;
using bookApi.WebApi.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Linq;

namespace bookApi.WebApi.Endpoints.CartOrder;

public class GetCartOrderById : BaseEndpoint<GetCartOrderByIdRequest, CartOrderResponse>
{
    private readonly ICartOrderService _cartOrderService;

    public GetCartOrderById(ICartOrderService cartOrderService)
    {
        _cartOrderService = cartOrderService;
    }

    [HttpGet("CartOrder/{OrderId}")]
    [Authorize]
    [RequiredScope(typeof(CartOrderScope), typeof(CartOrderScopeReadOnly))]
    [SwaggerOperation(
        Summary = "Get CartOrder by id",
        Description = "",
        OperationId = "CartOrder.GetById",
        Tags = new[] { "CartOrder" })
    ]
    [ProducesResponseType(typeof(CartOrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public override async Task<ActionResult<CartOrderResponse>> HandleAsync([FromRoute] GetCartOrderByIdRequest request,
        CancellationToken cancellationToken = new())
    {
        var CartOrder = await _cartOrderService.GetByIdAsync(request.OrderId, cancellationToken);
        if (CartOrder is null)
            return BadRequest(Error.Create("Data not found"));

        return new CartOrderResponse
        {
            OrderId = CartOrder.OrderId,
            UserId = CartOrder.UserId,
            UserName = CartOrder.User!.FullName,
            OrderDate = CartOrder.OrderDate,
            TotalQty = CartOrder.OrderDetails.Count(),
            TotalPrice = CartOrder.Total,
            Status = CartOrder.Status,
            CreatedAt = CartOrder.CreatedAt,
            CreatedByName = CartOrder.CreatedByName,
            LastUpdatedAt = CartOrder.LastUpdatedAt,
            LastUpdatedByName = CartOrder.LastUpdatedByName
        };
    }
}