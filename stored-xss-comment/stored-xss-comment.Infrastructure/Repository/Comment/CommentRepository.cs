using stored_xss_input.Core.Entity.Comment;
using stored_xss_input.Core.Interface.Repository.Comment;
using stored_xss_input.Data;
using stored_xss_input.Infrastructure.Repository.Base;

namespace stored_xss_input.Infrastructure.Repository.Comment;

public class CommentRepository : BaseRepository<CommentEntity>, ICommentRepository
{
    public CommentRepository(ApplicationDbContext context) : base(context)
    {
    }
}