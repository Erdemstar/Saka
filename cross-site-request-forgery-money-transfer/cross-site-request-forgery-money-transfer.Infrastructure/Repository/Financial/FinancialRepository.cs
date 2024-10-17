using cross_site_request_forgery_money_transfer.Core.Data;
using cross_site_request_forgery_money_transfer.Core.Entity.Financial;
using cross_site_request_forgery_money_transfer.Core.Interface.Repository.Financial;
using cross_site_request_forgery_money_transfer.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace cross_site_request_forgery_money_transfer.Infrastructure.Repository.Financial;

public class FinancialRepository : BaseRepository<FinancialEntity>, IFinancialRepository
{
    private readonly DbSet<FinancialEntity> _dbSet;

    public FinancialRepository(ApplicationDbContext context) : base(context)
    {
        _dbSet = context.Set<FinancialEntity>();
    }

    public async Task<FinancialEntity> GetByUserId(int userId)
    {
        return await _dbSet.FirstOrDefaultAsync(financial => financial.UserId == userId);
    }
}