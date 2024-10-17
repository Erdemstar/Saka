using cross_site_request_forgery_comment.Core.Entity.Comment;
using cross_site_request_forgery_comment.Core.Interface.Repository.Base;

namespace cross_site_request_forgery_comment.Core.Interface.Repository.Comment;

public interface ICommentRepository : IBaseRepository<CommentEntity>
{
    public Task<List<CommentEntity>> GetCommentByUserId(string userId);
}