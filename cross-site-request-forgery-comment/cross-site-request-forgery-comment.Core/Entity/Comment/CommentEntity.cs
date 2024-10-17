using cross_site_request_forgery_comment.Core.Entity.Base;

namespace cross_site_request_forgery_comment.Core.Entity.Comment;

public class CommentEntity : BaseEntity
{
    public string UserId { get; set; }
    public string Comment { get; set; }
}