using insecure_direct_object_reference_edit_profile.Core.Data;
using insecure_direct_object_reference_edit_profile.Core.Entity.Account;
using insecure_direct_object_reference_edit_profile.Core.Interface.Repository.Account;
using insecure_direct_object_reference_edit_profile.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace insecure_direct_object_reference_edit_profile.Infrastructure.Repository.Account;

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