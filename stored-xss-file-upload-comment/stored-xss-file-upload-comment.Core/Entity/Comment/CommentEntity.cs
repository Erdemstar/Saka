using stored_xss_file_upload.Core.Entity.Base;

namespace stored_xss_file_upload.Core.Entity.Comment;

public class CommentEntity : BaseEntity
{
    public string Name { get; set; }
    public string Comment { get; set; }
}