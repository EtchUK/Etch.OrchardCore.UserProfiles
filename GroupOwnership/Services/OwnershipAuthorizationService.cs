using Etch.OrchardCore.UserProfiles.Grouping.Models;
using Etch.OrchardCore.UserProfiles.Grouping.Services;
using Etch.OrchardCore.UserProfiles.Services;
using Microsoft.AspNetCore.Authorization;
using OrchardCore.ContentManagement;
using OrchardCore.Contents;
using OrchardCore.Users.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Etch.OrchardCore.UserProfiles.GroupOwnership.Services
{
    public class OwnershipAuthorizationService : IOwnershipAuthorizationService
    {
        #region Dependencies

        private readonly IAuthorizationService _authorizationService;
        private readonly IProfileGroupsService _profileGroupsService;
        private readonly IProfileService _profileService;
        private readonly IUserService _userService;

        #endregion Dependencies

        #region Constructor

        public OwnershipAuthorizationService(IAuthorizationService authorizationService,
            IProfileGroupsService profileGroupsService,
            IProfileService profileService,
            IUserService userService)
        {
            _authorizationService = authorizationService;
            _profileGroupsService = profileGroupsService;
            _profileService = profileService;
            _userService = userService;
        }

        #endregion Constructor

        #region IOwnershipAuthorizationService

        public async Task<bool> CanViewContentAsync(ClaimsPrincipal userPrincipal, IList<string> requiredGroupIds)
        {
            // Return false if no user
            if (userPrincipal == null)
            {
                return false;
            }

            // The content item doesn't have any group ownership so it's viewable to everyone.
            if (requiredGroupIds == null || !requiredGroupIds.Any())
            {
                return true;
            }

            // If logged in user is an admin of CMS can view the content
            if (await _authorizationService.AuthorizeAsync(userPrincipal, CommonPermissions.EditContent))
            {
                return true;
            }

            // Get current user
            var user = await _userService.GetAuthenticatedUserAsync(userPrincipal);

            if (user == null)
            {
                return false;
            }

            // Get user profile
            var profile = await _profileService.GetAsync(user);

            if (profile == null)
            {
                return false;
            }

            // Get profile group id
            var profileGroupedPart = profile.As<ProfileGroupedPart>();

            if (profileGroupedPart == null)
            {
                return false;
            }

            // If the group id is contained within the required list return true otherwise false
            return requiredGroupIds.Any(reqId => string.Equals(reqId, profileGroupedPart.GroupContentItemId, StringComparison.OrdinalIgnoreCase));
        }

        #endregion IOwnershipAuthorizationService
    }
}
