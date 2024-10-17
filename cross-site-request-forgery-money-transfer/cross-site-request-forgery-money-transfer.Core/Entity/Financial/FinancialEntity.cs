using cross_site_request_forgery_money_transfer.Core.Entity.Base;

namespace cross_site_request_forgery_money_transfer.Core.Entity.Financial;

public class FinancialEntity : BaseEntity
{
    public int UserId { get; set; }
    public int Money { get; set; }
}