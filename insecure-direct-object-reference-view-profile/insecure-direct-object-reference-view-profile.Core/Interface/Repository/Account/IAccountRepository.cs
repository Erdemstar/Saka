using insecure_direct_object_reference_view_profile.Core.Entity.Account;
using insecure_direct_object_reference_view_profile.Core.Interface.Repository.Base;

namespace insecure_direct_object_reference_view_profile.Core.Interface.Repository.Account;

public interface IAccountRepository : IBaseRepository<AccountEntity>
{
    public Task<AccountEntity> GetByEmailPasswordAsync(string email, string password);
}