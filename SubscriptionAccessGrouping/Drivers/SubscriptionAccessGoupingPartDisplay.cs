using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.Services;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Microsoft.AspNetCore.Http;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Environment.Shell;

namespace Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.Drivers
{
    public class SubscriptionAccessGoupingPartDisplay : ContentPartDisplayDriver<SubscriptionAccessPart>
    {

        #region Dependencies

        private readonly IAccessAuthorizationService _accessAuthorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ShellSettings _shellSettings;
        private readonly ISubscriptionAccessSettingsService _subscriptionAccessSettingsService;

        #endregion

        #region Constructor

        public SubscriptionAccessGoupingPartDisplay(IAccessAuthorizationService accessAuthorizationService, IHttpContextAccessor httpContextAccessor, ShellSettings shellSettings, ISubscriptionAccessSettingsService subscriptionAccessSettingsService)
        {
            _accessAuthorizationService = accessAuthorizationService;
            _httpContextAccessor = httpContextAccessor;
            _subscriptionAccessSettingsService = subscriptionAccessSettingsService;
            _shellSettings = shellSettings;
        }

        #endregion

        #region Overrides

        public override async Task<IDisplayResult> DisplayAsync(SubscriptionAccessPart part, BuildPartDisplayContext context)
        {

            // If the request is not a detail page then we allow users to view the content
            if (context.DisplayType != "Detail") {
                return null;
            }

            var settings = await _subscriptionAccessSettingsService.GetSettingsAsync();

            if (await _accessAuthorizationService.CanViewContent(_httpContextAccessor.HttpContext?.User, part.SubscriptionSelection)) {
                return null;
            }

            // If there is no redirect URL has been specified
            // then we redirect users to the root of the website.
            _httpContextAccessor.HttpContext.Response.Redirect(string.IsNullOrEmpty(settings.UnauthorisedRedirectPath) ? GetTenantUrl() : settings.UnauthorisedRedirectPath);

            return null;
        }

        #endregion

        #region Helpers

        private string GetTenantUrl()
        {
            return "/" + _shellSettings.RequestUrlPrefix;
        }


        #endregion
    }
}
