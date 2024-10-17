using stored_xss_file_upload_filename.Core.Entity.Base;

namespace stored_xss_file_upload2.Core.Entity.Comment;

public class CommentEntity : BaseEntity
{
    public string Name { get; set; }
    public string Comment { get; set; }
    public string Filename { get; set; }
}