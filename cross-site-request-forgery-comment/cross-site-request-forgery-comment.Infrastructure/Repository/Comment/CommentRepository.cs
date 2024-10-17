using cross_site_request_forgery_comment.Core.Data;
using cross_site_request_forgery_comment.Core.Entity.Comment;
using cross_site_request_forgery_comment.Core.Interface.Repository.Comment;
using cross_site_request_forgery_comment.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace cross_site_request_forgery_comment.Infrastructure.Repository.Comment;

public class CommentRepository : BaseRepository<CommentEntity>, ICommentRepository
{
    private readonly DbSet<CommentEntity> _dbSet;

    public CommentRepository(ApplicationDbContext context) : base(context)
    {
        _dbSet = context.Set<CommentEntity>(); // _dbSet'i burada context üzerinden alıyoruz
    }

    public async Task<List<CommentEntity>> GetCommentByUserId(string userId)
    {
        return await _dbSet.Where(comment => comment.UserId == userId).ToListAsync();
    }
}