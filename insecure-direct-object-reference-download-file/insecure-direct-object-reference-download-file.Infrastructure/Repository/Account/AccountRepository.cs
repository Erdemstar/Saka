using insecure_direct_object_reference_download_file.Core.Data;
using insecure_direct_object_reference_download_file.Core.Entity.Account;
using insecure_direct_object_reference_download_file.Core.Interface.Repository.Account;
using insecure_direct_object_reference_download_file.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace insecure_direct_object_reference_download_file.Infrastructure.Repository.Account;

public class AccountRepository : BaseRepository<AccountEntity>, IAccountRepository
{
    private readonly DbSet<AccountEntity> _dbSet;

    public AccountRepository(ApplicationDbContext context) : base(context)
    {
        _dbSet = context.Set<AccountEntity>(); // _dbSet'i burada context üzerinden alıyoruz
    }

    public async Task<AccountEntity> GetUserByEmailPasswordAsync(string email, string password)
    {
        return await _dbSet.FirstOrDefaultAsync(account => account.Email == email && account.Password == password);
    }

    public async Task<AccountEntity> GetUserByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(account => account.Email == email);
    }
}