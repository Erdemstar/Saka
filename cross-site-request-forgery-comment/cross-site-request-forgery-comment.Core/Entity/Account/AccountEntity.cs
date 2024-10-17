using cross_site_request_forgery_comment.Core.Entity.Base;

namespace cross_site_request_forgery_comment.Core.Entity.Account;

public class AccountEntity : BaseEntity
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Address { get; set; }
    public string Password { get; set; }
}