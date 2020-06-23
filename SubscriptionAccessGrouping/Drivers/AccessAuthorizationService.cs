using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Grouping.Services;
using Etch.OrchardCore.UserProfiles.Services;
using Etch.OrchardCore.UserProfiles.SubscriptionGroups.Models;
using Etch.OrchardCore.UserProfiles.SubscriptionGroups.Services;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Etch.OrchardCore.UserProfiles.Subscriptions.Services;
using Microsoft.AspNetCore.Authorization;
using OrchardCore.ContentManagement;
using OrchardCore.Contents;
using OrchardCore.Users.Services;
using Permissions = OrchardCore.Contents.CommonPermissions;

namespace Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.Drivers
{
    public class AccessAuthorizationService : IAccessAuthorizationService
    {
        #region Dependencies

        private readonly IAuthorizationService _authorizationService;
        private readonly IProfileGroupsService _profileGroupsService;
        private readonly IProfileService _profileService;
        private readonly ISubscriptionLevelService _subscriptionLevelService;
        private readonly ISubscriptionGroupsService _subscriptionGroupsService;
        private readonly IUserService _userService;

        #endregion

        #region Constructor

        public AccessAuthorizationService(IAuthorizationService authorizationService, IProfileGroupsService profileGroupsService, IProfileService profileService, ISubscriptionLevelService subscriptionLevelService, ISubscriptionGroupsService subscriptionGroupsService, IUserService userService)
        {
            _authorizationService = authorizationService;
            _profileGroupsService = profileGroupsService;
            _profileService = profileService;
            _subscriptionLevelService = subscriptionLevelService;
            _subscriptionGroupsService = subscriptionGroupsService;
            _userService = userService;
        }

        #endregion

        #region Implantation

        public async Task<bool> CanViewContent(ClaimsPrincipal userPrincipal, SubscriptionPart[] subscriptionAccessSelection)
        {
            // The content item doesn't have any subscription access so it's viewable to everyone.
            if (subscriptionAccessSelection == null || !subscriptionAccessSelection.Any(x => x.IsSelected)) {
                return true;
            }

            subscriptionAccessSelection = subscriptionAccessSelection.Where(x => x.IsSelected).ToArray();

            // If logged in user is an admin of CMS can view the content
            if (await _authorizationService.AuthorizeAsync(userPrincipal, CommonPermissions.EditContent)) {
                return true;
            }

            // Get current user
            var user = await _userService.GetAuthenticatedUserAsync(userPrincipal);

            if (user == null) {
                return false;
            }

            // Get user profile
            var profile = await _profileService.GetAsync(user);

            if (profile == null) {
                return false;
            }

            // Get subscription level on the group
            var group = await _profileGroupsService.GetSubscriptionAccessAsync(profile);

            if (group == null) {
                return false;
            }

            var allowMultiple = _subscriptionLevelService.GetSettings(group).Multiple;

            // Check if we use Subscription Group Access
            var profileSubscriptionGroupAccess = profile.ContentItem.As<SubscriptionGroupAccessPart>();
            if (profileSubscriptionGroupAccess != null) {
                var subscriptionGroups = await _subscriptionGroupsService.GetAsync(group, allowMultiple);

                // If use has access to group
                if(profileSubscriptionGroupAccess.SubscriptionGroupSelection.Any(x => x.IsSelected && subscriptionGroups.Any(y => y.Identifier == x.Identifier))) {

                    return HasAccessToSubscriptionLevel(subscriptionAccessSelection, group, allowMultiple);
                }

                return false;
            }

            return HasAccessToSubscriptionLevel(subscriptionAccessSelection, group, allowMultiple);
        }

        #endregion

        #region Private Methods

        private bool HasAccessToSubscriptionLevel(SubscriptionPart[] subscriptionAccessSelection, SubscriptionLevelPart group, bool allowMultiple)
        {
            // Check subsciption on single subscription level
            if (!allowMultiple && !subscriptionAccessSelection.Any(x => x.Identifier == group.Subscription)) {
                return false;
            }

            // Check subsciption on multiple subscription level
            if (allowMultiple && !subscriptionAccessSelection.Any(x => group.SubscriptionSelection.Any(y => y.Identifier == x.Identifier && y.IsSelected))) {
                return false;
            }

            return true;
        }

        #endregion
    }

    public interface IAccessAuthorizationService
    {
        Task<bool> CanViewContent(ClaimsPrincipal user, SubscriptionPart[] SubscriptionAccessSelection);
    }
}
