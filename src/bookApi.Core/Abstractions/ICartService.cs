using bookApi.Domain.Entities;

namespace bookApi.Core.Abstractions;

/// <summary>
/// Default implementation is AsNoTracking true.
/// </summary>
public interface ICartService : IEntityService<Cart>
{
}