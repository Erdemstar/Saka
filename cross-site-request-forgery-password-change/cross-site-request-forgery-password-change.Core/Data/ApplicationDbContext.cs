using cross_site_request_forgery_password_change.Core.Entity.Account;
using Microsoft.EntityFrameworkCore;

namespace cross_site_request_forgery_password_change.Core.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<AccountEntity> Account { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(
            "Data Source=/tmp/mydatabase.db");
    }
}