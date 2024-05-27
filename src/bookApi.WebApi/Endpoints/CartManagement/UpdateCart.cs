using bookApi.Core.Abstractions;
using bookApi.Domain.Entities;
using bookApi.Shared.Abstractions.Databases;
using bookApi.WebApi.Common;
using bookApi.WebApi.Endpoints.CartManagement.Requests;
using bookApi.WebApi.Endpoints.CartManagement.Scopes;
using bookApi.WebApi.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace bookApi.WebApi.Endpoints.CartManagement;

public class UpdateCart : BaseEndpointWithoutResponse<UpdateCartRequest>
{
    private readonly ICartService _cartService;
    private readonly IDbContext _dbContext;
    private readonly IStringLocalizer<UpdateCart> _localizer;

    public UpdateCart(ICartService cartService,
        IDbContext dbContext,
        IStringLocalizer<UpdateCart> localizer)
    {
        _cartService = cartService;
        _dbContext = dbContext;
        _localizer = localizer;
    }

    [HttpPut("Cart/{CartId}")]
    [Authorize]
    [RequiredScope(typeof(CartScope))]
    [SwaggerOperation(
        Summary = "Update Cart API",
        Description = "",
        OperationId = "Cart.UpdateCart",
        Tags = new[] { "Cart" })
    ]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public override async Task<ActionResult> HandleAsync([FromRoute] UpdateCartRequest request,
        CancellationToken cancellationToken = new())
    {
        var Cart = await _cartService.GetByIdAsync(request.CartId, cancellationToken);
        if (Cart is null)
            return BadRequest(Error.Create("Data not found"));

        _dbContext.AttachEntity(Cart);

        if (request.UpdateCartRequestPayload.BookId == Cart.BookId)
            Cart.BookId = request.UpdateCartRequestPayload.BookId.Value;

        if (request.UpdateCartRequestPayload.Qty != Cart.Qty)
            Cart.Qty = request.UpdateCartRequestPayload.Qty;

        Cart.TotalPrice = Cart.Price * request.UpdateCartRequestPayload.Qty;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}