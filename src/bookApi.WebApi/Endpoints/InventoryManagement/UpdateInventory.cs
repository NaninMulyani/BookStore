using bookApi.Core.Abstractions;
using bookApi.Domain.Entities;
using bookApi.Infrastructure.Services;
using bookApi.Shared.Abstractions.Databases;
using bookApi.WebApi.Common;
using bookApi.WebApi.Endpoints.InventoryManagement.Requests;
using bookApi.WebApi.Endpoints.InventoryManagement.Scopes;
using bookApi.WebApi.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace bookApi.WebApi.Endpoints.InventoryManagement;

public class UpdateInventory : BaseEndpointWithoutResponse<UpdateInventoryRequest>
{
    private readonly IDbContext _dbContext;
    private readonly IInventoryService _inventoryService;
    private readonly IBookService _bookService;

    public UpdateInventory(IDbContext dbContext,
        IInventoryService inventoryService,
        IBookService bookService)
    {
        _dbContext = dbContext;
        _inventoryService = inventoryService;
        _bookService = bookService;
    }

    [HttpPut("Inventorys/{InventoryId}")]
    [Authorize]
    [RequiredScope(typeof(InventoryManagementScope))]
    [SwaggerOperation(
        Summary = "Update Inventory API",
        Description = "",
        OperationId = "InventoryManagement.UpdateInventory",
        Tags = new[] { "InventoryManagement" })
    ]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public override async Task<ActionResult> HandleAsync([FromRoute] UpdateInventoryRequest request,
        CancellationToken cancellationToken = new())
    {
        var Inventory = await _inventoryService.GetByExpressionAsync(
            e => e.InventoryId == request.InventoryId,
            e => new Inventory
            {
                BookId = e.BookId,
                QtyCurrent = e.QtyCurrent,
            },
            cancellationToken);

        if (Inventory is null)
            return BadRequest(Error.Create("Data inventory not found"));

        _dbContext.AttachEntity(Inventory);

        if (request.UpdateInventoryRequestPayload.Qty != Inventory.QtyCurrent)
            Inventory.QtyCurrent = request.UpdateInventoryRequestPayload.Qty;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}