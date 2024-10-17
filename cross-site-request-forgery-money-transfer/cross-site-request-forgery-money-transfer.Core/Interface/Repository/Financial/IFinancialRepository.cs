using cross_site_request_forgery_money_transfer.Core.Entity.Financial;
using cross_site_request_forgery_money_transfer.Core.Interface.Repository.Base;

namespace cross_site_request_forgery_money_transfer.Core.Interface.Repository.Financial;

public interface IFinancialRepository : IBaseRepository<FinancialEntity>
{
    public Task<FinancialEntity> GetByUserId(int userId);
}