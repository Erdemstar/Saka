using sql_injection_profile.Core.Data;
using sql_injection_profile.Core.Entity.Profile;
using sql_injection_profile.Core.Interface.Repository.Profile;
using sql_injection_profile.Infrastructure.Repository.Base;

namespace sql_injection_profile.Infrastructure.Repository.Profile;

public class ProfileRepository : BaseRepository<ProfileEntity>, IProfileRepository
{
    public ProfileRepository(ApplicationDbContext context) : base(context)
    {
    }
}