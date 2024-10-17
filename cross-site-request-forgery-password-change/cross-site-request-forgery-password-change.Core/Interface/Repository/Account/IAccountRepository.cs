using cross_site_request_forgery_password_change.Core.Entity.Account;
using cross_site_request_forgery_password_change.Core.Interface.Repository.Base;

namespace cross_site_request_forgery_password_change.Core.Interface.Repository.Account;

public interface IAccountRepository : IBaseRepository<AccountEntity>
{
    public Task<AccountEntity> GetByEmailPasswordAsync(string email, string password);
}