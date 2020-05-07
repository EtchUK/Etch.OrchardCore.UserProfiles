using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Services;
using Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.Services;
using Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.ViewModels;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Microsoft.AspNetCore.Http;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;

namespace Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.Drivers
{
    public class SubscriptionAccessGroupingPartDisplay : ContentPartDisplayDriver<SubscriptionAccessPart>
    {
        #region Dependencies

        private readonly IAccessAuthorizationService _accessAuthorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISubscriptionAccessSettingsService _subscriptionAccessSettingsService;
        private readonly IURLService _urlService;

        #endregion

        #region Constructor

        public SubscriptionAccessGroupingPartDisplay(IAccessAuthorizationService accessAuthorizationService, IHttpContextAccessor httpContextAccessor, ISubscriptionAccessSettingsService subscriptionAccessSettingsService, IURLService urlService)
        {
            _accessAuthorizationService = accessAuthorizationService;
            _httpContextAccessor = httpContextAccessor;
            _subscriptionAccessSettingsService = subscriptionAccessSettingsService;
            _urlService = urlService;
        }

        #endregion

        #region Overrides

        public override async Task<IDisplayResult> DisplayAsync(SubscriptionAccessPart part, BuildPartDisplayContext context)
        {
            var canViewContent = await _accessAuthorizationService.CanViewContent(_httpContextAccessor.HttpContext?.User, part.SubscriptionSelection);

            // If the request is not a detail page then we allow users to view the content
            if (context.DisplayType != "Detail")
            {
                return Initialize<SubscriptionAccessViewModel>("SubscriptionAccessPart", model =>
                {
                    model.HasAccess = canViewContent;
                })
                .Location("Detail", "")
                .Location("Summary", "AfterContent")
                .Location("SummaryAdmin", "");
            }

            if (canViewContent)
            {
                return null;
            }

            var settings = await _subscriptionAccessSettingsService.GetSettingsAsync();

            // If there is no redirect URL has been specified
            // then we redirect users to the root of the website.
            _httpContextAccessor.HttpContext.Response.StatusCode = 401;
            _httpContextAccessor.HttpContext.Response.Redirect(string.IsNullOrEmpty(settings.UnauthorisedRedirectPath) ? _urlService.GetTenantUrl() : settings.UnauthorisedRedirectPath, false);

            return null;
        }

        #endregion

    }
}
