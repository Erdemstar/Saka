using Microsoft.EntityFrameworkCore;
using sql_injection_profile.Core.Entity.Profile;

namespace sql_injection_profile.Core.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<ProfileEntity> Profile { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(
            "Data Source=/tmp/mydatabase.db");
    }
}