using insecure_direct_object_reference_view_profile.Core.Entity.Account;
using Microsoft.EntityFrameworkCore;

namespace insecure_direct_object_reference_view_profile.Core.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<AccountEntity> Account { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(
            "Data Source=/tmp/mydatabase.db");
    }
}