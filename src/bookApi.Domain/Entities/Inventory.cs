using bookApi.Domain.Enums;
using bookApi.Shared.Abstractions.Entities;

namespace bookApi.Domain.Entities;

public sealed class Inventory : BaseEntity
{
    public Guid InventoryId { get; set; } = Guid.NewGuid();
    public Guid BookId { get; set; }
    public int? QtyCurrent { get; set; }
    public InventoryStatus Status { get; set; }
    public Book? Book { get; set; }
}