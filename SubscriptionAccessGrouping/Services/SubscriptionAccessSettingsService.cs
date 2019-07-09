using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.Models;
using OrchardCore.Entities;
using OrchardCore.Settings;

namespace Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.Services
{
    public class SubscriptionAccessSettingsService : ISubscriptionAccessSettingsService
    {
        private readonly ISiteService _siteService;

        public SubscriptionAccessSettingsService(ISiteService siteService) {
            _siteService = siteService;
        }

        public async Task<SubscriptionAccessSettings> GetSettingsAsync() {
            var siteSettings = await _siteService.GetSiteSettingsAsync();

            var settings = siteSettings.As<SubscriptionAccessSettings>();
            return settings;
        }
    }
}
