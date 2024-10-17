using cross_site_request_forgery_money_transfer.Core.Entity.Account;
using cross_site_request_forgery_money_transfer.Core.Entity.Financial;
using Microsoft.EntityFrameworkCore;

namespace cross_site_request_forgery_money_transfer.Core.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<AccountEntity> Account { get; set; }
    public DbSet<FinancialEntity> Financial { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(
            "Data Source=/tmp/mydatabase.db");
    }
}