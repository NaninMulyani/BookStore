using bookApi.Domain.Enums;
using bookApi.WebApi.Contracts.Responses;

namespace bookApi.WebApi.Endpoints.InventoryManagement.Responses;

public class InventoryResponse : BaseResponse
{
    public Guid? InventoryId { get; set; }
    public Guid? BookId { get; set; }
    public string? BookTitle { get; set; }
    public int? Qty { get; set; }
    public InventoryStatus? Status { get; set; }
}