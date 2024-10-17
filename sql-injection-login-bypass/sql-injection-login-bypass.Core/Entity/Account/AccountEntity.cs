using sql_injection_login_bypass.Core.Entity.Base;

namespace sql_injection_login_bypass.Core.Entity.Account;

public class AccountEntity: BaseEntity
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}