using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Grouping.Services;
using Etch.OrchardCore.UserProfiles.Services;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Microsoft.AspNetCore.Authorization;
using OrchardCore.Users;
using OrchardCore.Users.Services;
using Permissions = OrchardCore.Contents.Permissions;

namespace Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.Drivers
{
    public class AccessAuthorizationService : IAccessAuthorizationService
    {

        #region Dependencies

        private readonly IProfileService _profileService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserService _userService;
        private readonly IProfileGroupsService _profileGroupsService;

        #endregion

        #region Constructor

        public AccessAuthorizationService(IProfileService profileService, IAuthorizationService authorizationService, IUserService userService, IProfileGroupsService profileGroupsService)
        {
            _profileService = profileService;
            _authorizationService = authorizationService;
            _userService = userService;
            _profileGroupsService = profileGroupsService;
        }

        #endregion

        #region Implantation

        public async Task<bool> CanViewContent(ClaimsPrincipal userPrincipal, SubscriptionPart[] subscriptionAccessSelection)
        {
            // The content item doesn't have any subscription access so it's viewable to everyone.
            if(subscriptionAccessSelection == null || !subscriptionAccessSelection.Any(x => x.IsSelected)) {
                return true;
            }

            subscriptionAccessSelection = subscriptionAccessSelection.Where(x => x.IsSelected).ToArray();

            // If logged in user is an admin of CMS can view the content
            if (await _authorizationService.AuthorizeAsync(userPrincipal, Permissions.ViewContent)) {
                return true;
            }

            // Get current user
            var user = await _userService.GetAuthenticatedUserAsync(userPrincipal);

            if (user == null) {
                return false;
            }

            // Get user profile
            var profile = await _profileService.GetAsync(user);

            if(profile == null) {
                return false;
            }

            // Get subscription level on the group
            var group = await _profileGroupsService.GetSubscriptionAccessAsync(profile);

            if(group == null) {
                return false;
            }

            if(!subscriptionAccessSelection.Any(x => x.Identifier == group.Subscription)) {
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
