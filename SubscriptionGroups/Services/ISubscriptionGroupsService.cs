using System.Collections.Generic;
using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.SubscriptionGroups.Models;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;

namespace Etch.OrchardCore.UserProfiles.SubscriptionGroups.Services
{
    public interface ISubscriptionGroupsService
    {
        Task<List<SubscriptionGroupPart>> GetAllAsync();
        Task<List<SubscriptionGroupPart>> GetAsync(SubscriptionLevelPart group, bool allowMultiple);
    }
}
