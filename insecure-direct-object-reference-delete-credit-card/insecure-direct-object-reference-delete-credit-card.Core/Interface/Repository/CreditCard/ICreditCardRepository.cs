using insecure_direct_object_reference_delete_credit_card.Core.Entity.CreditCard;
using insecure_direct_object_reference_delete_credit_card.Core.Interface.Repository.Base;

namespace insecure_direct_object_reference_delete_credit_card.Core.Interface.Repository.CreditCard;

public interface ICreditCardRepository : IBaseRepository<CreditCardEntity>
{
    public Task<List<CreditCardEntity>> GetByUserId(string userId);
}