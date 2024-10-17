using cross_site_request_forgery_comment.Core.Entity.Account;
using cross_site_request_forgery_comment.Core.Entity.Comment;

namespace cross_site_request_forgery_comment.Core.Entity.View.AccountComment;

public class AccountCommentViewEntity
{
    public AccountEntity AccountEntity { get; set; }
    public CommentEntity CommentEntity { get; set; }
}