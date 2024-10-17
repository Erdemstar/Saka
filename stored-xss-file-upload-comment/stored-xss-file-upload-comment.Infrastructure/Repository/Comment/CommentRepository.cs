using stored_xss_file_upload.Core.Data;
using stored_xss_file_upload.Core.Entity.Comment;
using stored_xss_file_upload.Infrastructure.Repository.Base;
using stored_xss_input.Core.Interface.Repository.Comment;

namespace stored_xss_file_upload.Infrastructure.Repository.Comment;

public class CommentRepository : BaseRepository<CommentEntity>, ICommentRepository
{
    public CommentRepository(ApplicationDbContext context) : base(context)
    {
    }
}