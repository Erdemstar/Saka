using cross_site_request_forgery_comment.Core.Entity.Account;
using cross_site_request_forgery_comment.Core.Entity.Comment;
using Microsoft.EntityFrameworkCore;

namespace cross_site_request_forgery_comment.Core.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<AccountEntity> Account { get; set; }
    public DbSet<CommentEntity> Comment { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(
            "Data Source=/tmp/mydatabase.db");
    }
}