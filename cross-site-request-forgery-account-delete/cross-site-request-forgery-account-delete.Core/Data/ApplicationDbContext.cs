using cross_site_request_forgery_account_delete.Core.Entity.Account;
using Microsoft.EntityFrameworkCore;

namespace cross_site_request_forgery_account_delete.Core.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<AccountEntity> Account { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(
            "Data Source=/tmp/mydatabase.db");
    }
}