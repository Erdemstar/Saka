using cross_site_request_forgery_money_transfer.Core.Entity.Account;
using cross_site_request_forgery_money_transfer.Core.Interface.Repository.Base;

namespace cross_site_request_forgery_money_transfer.Core.Interface.Repository.Account;

public interface IAccountRepository : IBaseRepository<AccountEntity>
{
    public Task<AccountEntity> GetByEmailPasswordAsync(string email, string password);
}