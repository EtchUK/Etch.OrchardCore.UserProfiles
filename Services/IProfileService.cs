using OrchardCore.Users;
using System.Threading.Tasks;

namespace Etch.OrchardCore.UserProfiles.Services
{
    public interface IProfileService
    {
        Task CreateProfileAsync(IUser user);
    }
}
