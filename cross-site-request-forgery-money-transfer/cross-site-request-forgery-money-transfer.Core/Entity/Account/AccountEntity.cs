using cross_site_request_forgery_money_transfer.Core.Entity.Base;

namespace cross_site_request_forgery_money_transfer.Core.Entity.Account;

public class AccountEntity : BaseEntity
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}