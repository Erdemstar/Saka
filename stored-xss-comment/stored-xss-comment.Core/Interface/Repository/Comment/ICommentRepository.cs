using stored_xss_input.Core.Entity.Comment;
using stored_xss_input.Core.Interface.Repository.Base;

namespace stored_xss_input.Core.Interface.Repository.Comment;

public interface ICommentRepository : IBaseRepository<CommentEntity>
{
}