using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.Models;

namespace Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.Services
{
    public interface ISubscriptionAccessSettingsService
    {
        Task<SubscriptionAccessSettings> GetSettingsAsync();
    }
}
