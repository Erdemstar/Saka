using Microsoft.EntityFrameworkCore;
using sql_injection_search.Core.Data;
using sql_injection_search.Core.Entity.Base;
using sql_injection_search.Core.Interface.Repository.Base;

namespace sql_injection_search.Infrastructure.Repository.Base;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    protected BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<List<T>> GetByUsernameAsync(string username)
    {
        var sql = $"SELECT * FROM {typeof(T).Name.Replace("Entity", "")} WHERE Username LIKE '%{username}%'";
        var users = await _context.Set<T>().FromSqlRaw(sql).ToListAsync();
        return users.ToList();
    }
}