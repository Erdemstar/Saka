using sql_injection_search.Core.Entity.Base;

namespace sql_injection_search.Core.Entity.Account;

public class AccountEntity : BaseEntity
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}