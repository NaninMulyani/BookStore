using bookApi.Core.Abstractions;
using bookApi.Domain.Entities;
using bookApi.Shared.Abstractions.Databases;
using bookApi.Shared.Abstractions.Encryption;
using bookApi.WebApi.Common;
using bookApi.WebApi.Endpoints.InventoryManagement.Requests;
using bookApi.WebApi.Endpoints.InventoryManagement.Scopes;
using bookApi.WebApi.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mail;

namespace bookApi.WebApi.Endpoints.InventoryManagement;

public class CreateInventory : BaseEndpointWithoutResponse<CreateInventoryRequest>
{
    private readonly IDbContext _dbContext;
    private readonly IInventoryService _inventoryService;
    private readonly IBookService _bookService;

    public CreateInventory(IDbContext dbContext,
        IInventoryService inventoryService,
        IBookService bookService)
    {
        _dbContext = dbContext;
        _inventoryService = inventoryService;
        _bookService = bookService;
    }

    [HttpPost("Inventory")]
    [Authorize]
    [RequiredScope(typeof(InventoryManagementScope))]
    [SwaggerOperation(
        Summary = "Create Inventory API",
        Description = "",
        OperationId = "InventoryManagement.CreateInventory",
        Tags = new[] { "InventoryManagement" })
    ]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public override async Task<ActionResult> HandleAsync(CreateInventoryRequest request,
        CancellationToken cancellationToken = new())
    {
        var dataBook = await _dbContext.Set<Book>()
            .Where(e => e.BookId == request.BookId)
            .FirstOrDefaultAsync(cancellationToken);

        if (dataBook is null)
            return BadRequest(Error.Create("Book data not found"));

        var Inventory = new Inventory
        {
            BookId = request.BookId,
            QtyCurrent = request.Qty,
            Status = Domain.Enums.InventoryStatus.In,
        };

        var resultInventory = await _inventoryService.CreateAsync(Inventory, cancellationToken);

        //===========Update qty tbl Book
        dataBook.QtyAvailable = dataBook.QtyAvailable + request.Qty;

        _dbContext.Set<Book>().Update(dataBook);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        if (result == 0)
            await _inventoryService.DeleteAsync(resultInventory!.InventoryId, cancellationToken);

        return NoContent();
    }
}