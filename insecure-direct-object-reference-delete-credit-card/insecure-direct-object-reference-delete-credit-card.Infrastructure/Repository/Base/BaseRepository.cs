using insecure_direct_object_reference_delete_credit_card.Core.Data;
using insecure_direct_object_reference_delete_credit_card.Core.Entity.Base;
using insecure_direct_object_reference_delete_credit_card.Core.Interface.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace insecure_direct_object_reference_delete_credit_card.Infrastructure.Repository.Base;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    protected BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(string id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        if (entity is BaseEntity baseEntity)
        {
            var totalCount = await _dbSet.CountAsync();
            baseEntity.Id = (totalCount + 1).ToString();
        }

        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}