using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.Models;
using Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Settings;

using Permissions = UKIE.OrchardCore.UserProfiles.SubscriptionAccessGrouping.Permissions;

namespace Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.Drivers
{
    public class SubscriptionAccessSettingsDisplay : SectionDisplayDriver<ISite, SubscriptionAccessSettings>
    {
        #region Constants

        public const string GroupId = "SubscriptionAccessGrouping";

        #endregion

        #region Dependencies

        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructor

        public SubscriptionAccessSettingsDisplay(IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Overrides

        public override async Task<IDisplayResult> EditAsync(SubscriptionAccessSettings settings, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageSubscription)) {
                return null;
            }

            return Initialize<SubscriptionAccessSettingsViewModel>("SubscriptionAccessSettings_Edit", async model =>
            {
                model.UnauthorisedRedirectPath = settings.UnauthorisedRedirectPath;

            }).Location("Content:3").OnGroup(GroupId);
        }

        public override async Task<IDisplayResult> UpdateAsync(SubscriptionAccessSettings settings, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageSubscription)) {
                return null;
            }

            if (context.GroupId == GroupId) {
                var model = new SubscriptionAccessSettingsViewModel();

                await context.Updater.TryUpdateModelAsync(model, Prefix);

                settings.UnauthorisedRedirectPath = model.UnauthorisedRedirectPath;
            }

            return await EditAsync(settings, context);
        }

        #endregion
    }
}
