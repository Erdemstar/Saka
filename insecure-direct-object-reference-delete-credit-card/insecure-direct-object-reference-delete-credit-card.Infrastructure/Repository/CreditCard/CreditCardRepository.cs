using insecure_direct_object_reference_delete_credit_card.Core.Data;
using insecure_direct_object_reference_delete_credit_card.Core.Entity.CreditCard;
using insecure_direct_object_reference_delete_credit_card.Core.Interface.Repository.CreditCard;
using insecure_direct_object_reference_delete_credit_card.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace insecure_direct_object_reference_delete_credit_card.Infrastructure.Repository.CreditCard;

public class CreditCardRepository : BaseRepository<CreditCardEntity>, ICreditCardRepository
{
    private readonly DbSet<CreditCardEntity> _dbSet;

    public CreditCardRepository(ApplicationDbContext context) : base(context)
    {
        _dbSet = context.Set<CreditCardEntity>(); // _dbSet'i burada context üzerinden alıyoruz
    }


    public async Task<List<CreditCardEntity>> GetByUserId(string userId)
    {
        return _dbSet.Where(card => card.UserId == userId).ToList();
    }
}