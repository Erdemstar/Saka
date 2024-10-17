using insecure_direct_object_reference_delete_credit_card.Core.Entity.Base;

namespace insecure_direct_object_reference_delete_credit_card.Core.Entity.Account;

public class AccountEntity : BaseEntity
{
    // Login Info
    public string Email { get; set; }
    public string Password { get; set; }

    // General Info
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Address { get; set; }
    public string Hobby { get; set; }
}