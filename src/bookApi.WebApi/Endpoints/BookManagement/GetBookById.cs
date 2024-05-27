using bookApi.Core.Abstractions;
using bookApi.WebApi.Common;
using bookApi.WebApi.Endpoints.BookManagement.Responses;
using bookApi.WebApi.Endpoints.BookManagement.Requests;
using bookApi.WebApi.Endpoints.BookManagement.Scopes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace bookApi.WebApi.Endpoints.BookManagement;

public class GetBookById : BaseEndpoint<GetBookByIdRequest, BookResponse>
{
    private readonly IBookService _BookService;
    private readonly IStringLocalizer<GetBookById> _localizer;

    public GetBookById(IBookService BookService,
        IStringLocalizer<GetBookById> localizer)
    {
        _BookService = BookService;
        _localizer = localizer;
    }

    [HttpGet("Books/{BookId}")]
    [Authorize]
    [RequiredScope(typeof(BookManagementScope), typeof(BookManagementScopeReadOnly))]
    [SwaggerOperation(
        Summary = "Get Book by id",
        Description = "",
        OperationId = "BookManagement.GetById",
        Tags = new[] { "BookManagement" })
    ]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public override async Task<ActionResult<BookResponse>> HandleAsync([FromRoute] GetBookByIdRequest request,
        CancellationToken cancellationToken = new())
    {
        var Book = await _BookService.GetByIdAsync(request.BookId, cancellationToken);
        if (Book is null)
            return BadRequest(Error.Create(_localizer["data-not-found"]));

        return new BookResponse
        {
            BookId = Book.BookId,
            Title = Book.Title,
            Code = Book.Title,
            Author = Book.Author,
            Publisher = Book.Publisher,
            YearPublish = Book.YearPublish,
            Description = Book.Description,
            Genre = Book.Genre,
            Price = Book.Price,
            Qty = Book.QtyAvailable,

            CreatedAt = Book.CreatedAt,
            CreatedByName = Book.CreatedByName,
            LastUpdatedAt = Book.LastUpdatedAt,
            LastUpdatedByName = Book.LastUpdatedByName
        };
    }
}