using bookApi.Core.Abstractions;
using bookApi.Domain.Entities;
using bookApi.Infrastructure.Services;
using bookApi.Shared.Abstractions.Databases;
using bookApi.WebApi.Common;
using bookApi.WebApi.Endpoints.CartOrder.Requests;
using bookApi.WebApi.Endpoints.CartOrder.Scopes;
using bookApi.WebApi.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace bookApi.WebApi.Endpoints.CartOrder;

public class DeleteCartOrder : BaseEndpointWithoutResponse<DeleteCartOrderRequest>
{
    private readonly ICartOrderService _cartOrderService;
    private readonly IDbContext _dbContext;

    public DeleteCartOrder(ICartOrderService cartOrderService,
        IDbContext dbContext)
    {
        _cartOrderService = cartOrderService;
        _dbContext = dbContext;
    }

    [HttpDelete("CartOrder/{OrderId}")]
    [Authorize]
    [RequiredScope(typeof(CartOrderScope))]
    [SwaggerOperation(
        Summary = "Delete CartOrder API",
        Description = "",
        OperationId = "CartOrder.DeleteCartOrder",
        Tags = new[] { "CartOrder" })
    ]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public override async Task<ActionResult> HandleAsync([FromRoute] DeleteCartOrderRequest request,
        CancellationToken cancellationToken = new())
    {
        var CartOrder = await _cartOrderService.GetByIdAsync(request.OrderId, cancellationToken);
        if (CartOrder is null)
            return BadRequest(Error.Create("Order data not found"));

        if (CartOrder.Status != Domain.Enums.OrderStatus.Onprocess)
            return BadRequest(Error.Create("The Order has been processed"));

        await _cartOrderService.DeleteAsync(request.OrderId, cancellationToken);

        return NoContent();
    }
}