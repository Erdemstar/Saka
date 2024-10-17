using sql_injection_profile.Core.Entity.Base;

namespace sql_injection_profile.Core.Entity.Profile;

public class ProfileEntity : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Address { get; set; }
}