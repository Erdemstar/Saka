using cross_site_request_forgery_comment.Core.Entity.Account;
using cross_site_request_forgery_comment.Core.Interface.Repository.Base;

namespace cross_site_request_forgery_comment.Core.Interface.Repository.Account;

public interface IAccountRepository : IBaseRepository<AccountEntity>
{
    public Task<AccountEntity> GetByEmailPasswordAsync(string email, string password);
}