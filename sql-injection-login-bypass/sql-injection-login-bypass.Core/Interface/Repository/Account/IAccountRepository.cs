using sql_injection_login_bypass.Core.Entity.Account;
using sql_injection_login_bypass.Core.Interface.Repository.Base;

namespace sql_injection_login_bypass.Core.Interface.Repository.Account;

public interface IAccountRepository: IBaseRepository<AccountEntity>
{
    
}