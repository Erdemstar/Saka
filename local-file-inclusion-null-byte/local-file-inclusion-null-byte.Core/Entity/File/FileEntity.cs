using local_file_inclusion_filename.Core.Entity.Base;

namespace local_file_inclusion_filename.Core.Entity.File;

public class FileEntity : BaseEntity
{
    public string Name { get; set; }
    public string Content { get; set; }
}