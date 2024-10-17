using insecure_direct_object_reference_delete_credit_card.Core.Entity.Base;

namespace insecure_direct_object_reference_delete_credit_card.Core.Entity.CreditCard;

public class CreditCardEntity : BaseEntity
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public string CVE { get; set; }
    public string Password { get; set; }
}