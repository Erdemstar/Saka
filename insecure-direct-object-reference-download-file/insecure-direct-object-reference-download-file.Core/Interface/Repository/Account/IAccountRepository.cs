using insecure_direct_object_reference_download_file.Core.Entity.Account;
using insecure_direct_object_reference_download_file.Core.Interface.Repository.Base;

namespace insecure_direct_object_reference_download_file.Core.Interface.Repository.Account;

public interface IAccountRepository : IBaseRepository<AccountEntity>
{
    public Task<AccountEntity> GetUserByEmailPasswordAsync(string email, string password);
    public Task<AccountEntity> GetUserByEmailAsync(string email);
}