using sql_injection_login_bypass.Core.Interface.Repository.Account;
using sql_injection_search.Core.Data;
using sql_injection_search.Core.Entity.Account;
using sql_injection_search.Infrastructure.Repository.Base;

namespace sql_injection_search.Infrastructure.Repository.Account;

public class AccountRepository : BaseRepository<AccountEntity>, IAccountRepository
{
    public AccountRepository(ApplicationDbContext context) : base(context)
    {
    }
}