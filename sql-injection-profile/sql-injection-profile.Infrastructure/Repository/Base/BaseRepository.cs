using Microsoft.EntityFrameworkCore;
using sql_injection_profile.Core.Data;
using sql_injection_profile.Core.Entity.Base;
using sql_injection_profile.Core.Interface.Repository.Base;

namespace sql_injection_profile.Infrastructure.Repository.Base;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    protected BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<List<T>> GetByIdAsync(string id)
    {
        var sql = $"SELECT * FROM {typeof(T).Name.Replace("Entity", "")} WHERE Id = {id}";
        var result = await _context.Set<T>()
            .FromSqlRaw(sql)
            .ToListAsync();

        return result.ToList();
    }
}