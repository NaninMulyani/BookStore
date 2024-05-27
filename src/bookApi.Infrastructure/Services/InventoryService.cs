using bookApi.Core.Abstractions;
using bookApi.Domain.Entities;
using bookApi.Shared.Abstractions.Databases;
using bookApi.Shared.Abstractions.Encryption;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace bookApi.Infrastructure.Services;

public class InventoryService : IInventoryService
{
    private readonly IDbContext _dbContext;

    public InventoryService(IDbContext dbContext, ISalter salter)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Inventory> GetBaseQuery()
        => _dbContext.Set<Inventory>()
            .Include(e => e.Book)
            .AsQueryable();

    public Task<Inventory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => GetBaseQuery()
            .Where(e => e.InventoryId == id)
            .FirstOrDefaultAsync(cancellationToken);

    public Task<List<Inventory>> GetByBookIdAsync(Guid BookId, CancellationToken cancellationToken = default)
        => GetBaseQuery()
            .Where(e => e.BookId == BookId)
            .ToListAsync(cancellationToken);

    public async Task<bool?> CreateInventoryAsync(Inventory entity, CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbContext.InsertAsync(entity, cancellationToken);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            if(result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<Inventory?> CreateAsync(Inventory entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.InsertAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var existingInventory = await _dbContext.Set<Inventory>()
        .Where(e => e.InventoryId == id)
        .FirstOrDefaultAsync(cancellationToken);

        if (existingInventory is not null)
        {
            existingInventory.IsDeleted = true;
            existingInventory.StatusRecord = Shared.Abstractions.Enums.StatusRecord.InActive;

            // Attach the updated entity
            _dbContext.Set<Inventory>().Update(existingInventory);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public Task<Inventory?> GetByExpressionAsync(
        Expression<Func<Inventory, bool>> predicate,
        Expression<Func<Inventory, Inventory>> projection,
        CancellationToken cancellationToken = default)
        => GetBaseQuery()
            .Where(predicate)
            .Select(projection)
            .FirstOrDefaultAsync(cancellationToken);

    public Task<bool> IsInventoryExistAsync(Guid BookId, CancellationToken cancellationToken = default)
    {
        return GetBaseQuery().Where(e => e.BookId == BookId)
            .AnyAsync(cancellationToken);
    }
}