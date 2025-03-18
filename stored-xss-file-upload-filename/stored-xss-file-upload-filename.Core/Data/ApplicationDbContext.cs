using Microsoft.EntityFrameworkCore;
using stored_xss_file_upload2.Core.Entity.Comment;

namespace stored_xss_file_upload_filename.Core.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<CommentEntity> Comment { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(
            "Data Source=/tmp/mydatabase.db");
    }
}