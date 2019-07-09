using System.Collections.Generic;
using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.Services
{
    public interface ISubscriptionsService
    {
        Task<List<SubscriptionPart>> GetAllAsync();
    }
}
