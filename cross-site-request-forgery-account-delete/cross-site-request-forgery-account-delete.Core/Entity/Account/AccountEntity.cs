using cross_site_request_forgery_account_delete.Core.Entity.Base;

namespace cross_site_request_forgery_account_delete.Core.Entity.Account;

public class AccountEntity : BaseEntity
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Address { get; set; }
    public string Password { get; set; }
    public bool isDeleted { get; set; }
}