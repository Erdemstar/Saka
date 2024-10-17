using cross_site_request_forgery_money_transfer.Core.Entity.Account;
using cross_site_request_forgery_money_transfer.Core.Entity.Financial;

namespace cross_site_request_forgery_money_transfer.Core.Entity.View.AccountFinancial;

public class AccountFinancialViewEntity
{
    public AccountEntity Account { get; set; }
    public FinancialEntity Financial { get; set; }
}