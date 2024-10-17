using sql_injection_profile.Core.Entity.Profile;
using sql_injection_profile.Core.Interface.Repository.Base;

namespace sql_injection_profile.Core.Interface.Repository.Profile;

public interface IProfileRepository : IBaseRepository<ProfileEntity>
{
}