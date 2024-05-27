using bookApi.Core.Abstractions;
using bookApi.Domain.Entities;
using bookApi.Shared.Abstractions.Databases;
using bookApi.Shared.Abstractions.Encryption;
using bookApi.WebApi.Common;
using bookApi.WebApi.Endpoints.BookManagement.Requests;
using bookApi.WebApi.Endpoints.BookManagement.Scopes;
using bookApi.WebApi.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace bookApi.WebApi.Endpoints.BookManagement;

public class CreateBook : BaseEndpointWithoutResponse<List<CreateBookRequest>>
{
    private readonly IDbContext _dbContext;
    private readonly IBookService _bookService;
    private readonly IInventoryService _inventoryService;

    public CreateBook(IDbContext dbContext,
        IBookService bookService,
        IInventoryService inventoryService)
    {
        _dbContext = dbContext;
        _bookService = bookService;
        _inventoryService = inventoryService;
    }

    [HttpPost("Book")]
    [Authorize]
    [RequiredScope(typeof(BookManagementScope))]
    [SwaggerOperation(
        Summary = "Create Book API",
        Description = "",
        OperationId = "BookManagement.CreateBook",
        Tags = new[] { "BookManagement" })
    ]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public override async Task<ActionResult> HandleAsync(List<CreateBookRequest> request,
        CancellationToken cancellationToken = new())
    {
        if (request != null && request.Count() > 0)
        {
            foreach (var item in request)
            {
                var BookExist = await _bookService.IsBookExistAsync(item.Title!, cancellationToken);
                if (BookExist)
                    return BadRequest(Error.Create("Bookname-exists"));

                var Book = new Book
                {
                    Code = item.Code,
                    Title = item.Title,
                    Author = item.Author,
                    Publisher = item.Publisher,
                    YearPublish = item.YearPublish,
                    Description = item.Description,
                    Genre = item.Genre,
                    Price = item.Price,
                    QtyAvailable = item.Qty,
                };

                await _bookService.CreateAsync(Book, cancellationToken);

                //====================Inventory Initial
                var Inventory = new Inventory
                {
                    BookId = Book.BookId,
                    QtyCurrent = item.Qty,
                };

                var result = await _inventoryService.CreateInventoryAsync(Inventory, cancellationToken);

                if(result == false)
                    await _bookService.DeleteAsync(Book.BookId, cancellationToken);
            }
        }

        return NoContent();
    }
}