using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Etch.OrchardCore.UserProfiles.GroupOwnership.Services
{
    public interface IOwnershipAuthorizationService
    {
        Task<bool> CanViewContentAsync(ClaimsPrincipal user, IList<string> requiredGroupIds);
    }
}
