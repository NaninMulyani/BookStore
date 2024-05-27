using bookApi.Core.Abstractions;
using bookApi.Domain.Entities;
using bookApi.Shared.Abstractions.Databases;
using bookApi.Shared.Abstractions.Encryption;
using bookApi.WebApi.Common;
using bookApi.WebApi.Endpoints.CartManagement.Requests;
using bookApi.WebApi.Endpoints.CartManagement.Scopes;
using bookApi.WebApi.Mapping;
using bookApi.WebApi.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace bookApi.WebApi.Endpoints.CartManagement;

public class CreateCart : BaseEndpointWithoutResponse<CreateCartRequest>
{
    private readonly IDbContext _dbContext;
    private readonly ICartService _cartService;
    private readonly IBookService _bookService;
    private readonly IUserService _userService;

    public CreateCart(IDbContext dbContext,
        ICartService cartService,
        IBookService bookService,
        IUserService userService)
    {
        _dbContext = dbContext;
        _cartService = cartService;
        _bookService = bookService;
        _userService = userService;
    }

    [HttpPost("Cart")]
    [Authorize]
    [RequiredScope(typeof(CartScope))]
    [SwaggerOperation(
        Summary = "Create Cart API",
        Description = "",
        OperationId = "CreateCart",
        Tags = new[] { "Cart" })
    ]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public override async Task<ActionResult> HandleAsync(CreateCartRequest request,
        CancellationToken cancellationToken = new())
    {
        var User = await _userService.GetByIdAsync(request.UserId!.Value, cancellationToken);
        if (User is null)
            return BadRequest("User not found");

        var Book = await _bookService.GetByIdAsync(request.BookId!.Value, cancellationToken);
        if (Book is null)
            return BadRequest("Book not found");

        var Cart = new Domain.Entities.Cart
        {
            BookId = request.BookId!.Value,
            UserId = request.UserId!.Value,
            Price = Book.Price,
            Qty = request.Qty,
            TotalPrice = Book.Price * request.Qty,
        };

        await _cartService.CreateAsync(Cart, cancellationToken);

        return NoContent();
    }
}