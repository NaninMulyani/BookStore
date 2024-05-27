using Microsoft.AspNetCore.Mvc;

namespace bookApi.WebApi.Endpoints.InventoryManagement.Requests;

public class DeleteInventoryRequest
{
    [FromRoute(Name = "InventoryId")] public Guid InventoryId { get; set; }
}