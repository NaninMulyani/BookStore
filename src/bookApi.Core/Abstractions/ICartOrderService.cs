using bookApi.Domain.Entities;

namespace bookApi.Core.Abstractions;

/// <summary>
/// Default implementation is AsNoTracking true.
/// </summary>
public interface ICartOrderService : IEntityService<Order>
{
    Task<List<OrderDetail>> GetDetailByIdAsync(Guid OrderId, CancellationToken cancellationToken = default);
}