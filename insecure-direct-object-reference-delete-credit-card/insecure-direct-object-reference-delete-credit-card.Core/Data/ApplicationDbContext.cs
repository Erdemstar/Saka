using insecure_direct_object_reference_delete_credit_card.Core.Entity.Account;
using insecure_direct_object_reference_delete_credit_card.Core.Entity.CreditCard;
using Microsoft.EntityFrameworkCore;

namespace insecure_direct_object_reference_delete_credit_card.Core.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<AccountEntity> Account { get; set; }
    public DbSet<CreditCardEntity> CreditCard { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(
            "Data Source=/tmp/mydatabase.db");
    }
}