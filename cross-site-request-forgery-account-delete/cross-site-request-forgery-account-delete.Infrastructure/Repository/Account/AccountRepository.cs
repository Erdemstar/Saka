using cross_site_request_forgery_account_delete.Core.Data;
using cross_site_request_forgery_account_delete.Core.Entity.Account;
using cross_site_request_forgery_account_delete.Core.Interface.Repository.Account;
using cross_site_request_forgery_account_delete.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace cross_site_request_forgery_account_delete.Infrastructure.Repository.Account;

public class AccountRepository : BaseRepository<AccountEntity>, IAccountRepository
{
    private readonly DbSet<AccountEntity> _dbSet;

    public AccountRepository(ApplicationDbContext context) : base(context)
    {
        _dbSet = context.Set<AccountEntity>(); // _dbSet'i burada context üzerinden alıyoruz
    }

    public async Task<AccountEntity> GetByEmailPasswordAsync(string email, string password)
    {
        return await _dbSet.FirstOrDefaultAsync(account => account.Email == email && account.Password == password);
    }
}