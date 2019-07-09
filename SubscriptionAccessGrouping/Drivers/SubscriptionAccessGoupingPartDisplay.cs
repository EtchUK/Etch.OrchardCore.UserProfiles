using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.Services;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Microsoft.AspNetCore.Http;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;

namespace Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.Drivers
{
    public class SubscriptionAccessGoupingPartDisplay : ContentPartDisplayDriver<SubscriptionAccessPart>
    {

        #region Dependencies

        private readonly ISubscriptionAccessSettingsService _subscriptionAccessSettingsService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccessAuthorizationService _accessAuthorizationService;

        #endregion

        #region Constructor

        public SubscriptionAccessGoupingPartDisplay(ISubscriptionAccessSettingsService subscriptionAccessSettingsService, IHttpContextAccessor httpContextAccessor, IAccessAuthorizationService accessAuthorizationService)
        {
            _subscriptionAccessSettingsService = subscriptionAccessSettingsService;
            _httpContextAccessor = httpContextAccessor;
            _accessAuthorizationService = accessAuthorizationService;
        }

        #endregion

        #region Overrides

        public override async Task<IDisplayResult> DisplayAsync(SubscriptionAccessPart part, BuildPartDisplayContext context)
        {

            // If the request is not a detail page then we allow users to view the content
            if(context.DisplayType != "Detail") {
                return null;
            }

            var settings = await _subscriptionAccessSettingsService.GetSettingsAsync();

            if (await _accessAuthorizationService.CanViewContent(_httpContextAccessor.HttpContext?.User, part.SubscriptionAccessSelection)) {
                return null;
            }

            _httpContextAccessor.HttpContext.Response.Redirect(settings.RedirectPath);

            return null;
        } 

        #endregion
    }
}
