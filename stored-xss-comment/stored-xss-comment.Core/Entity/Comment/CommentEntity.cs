using stored_xss_input.Core.Entity.Base;

namespace stored_xss_input.Core.Entity.Comment;

public class CommentEntity : BaseEntity
{
    public string Name { get; set; }
    public string Comment { get; set; }
}