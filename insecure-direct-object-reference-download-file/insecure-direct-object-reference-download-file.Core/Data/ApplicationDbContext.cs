using insecure_direct_object_reference_download_file.Core.Entity.Account;
using Microsoft.EntityFrameworkCore;

namespace insecure_direct_object_reference_download_file.Core.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<AccountEntity> Account { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(
            "Data Source=/tmp/mydatabase.db");
    }
}