using stored_xss_file_upload_filename.Core.Data;
using stored_xss_file_upload_filename.Core.Interface.Repository.Comment;
using stored_xss_file_upload_filename.Infrastructure.Repository.Base;
using stored_xss_file_upload2.Core.Entity.Comment;

namespace stored_xss_file_upload_filename.Infrastructure.Repository.Comment;

public class CommentRepository : BaseRepository<CommentEntity>, ICommentRepository
{
    public CommentRepository(ApplicationDbContext context) : base(context)
    {
    }
}