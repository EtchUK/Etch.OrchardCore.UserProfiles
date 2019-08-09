using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Grouping.Services;
using Etch.OrchardCore.UserProfiles.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.Users;
using OrchardCore.Users.Services;

namespace Etch.OrchardCore.UserProfiles.Handlers
{
    public class ProfilePartHandler : ContentPartHandler<ProfilePart>
    {

        #region Dependencies

        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProfileGroupsService _profileGroupsService;
        private readonly UserManager<IUser> _userManager;
        private readonly IUserService _userService;

        #endregion

        #region Constructor

        public ProfilePartHandler(IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor, IProfileGroupsService profileGroupsService, UserManager<IUser> userManager, IUserService userService)
        {
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _profileGroupsService = profileGroupsService;
            _userManager = userManager;
            _userService = userService;
        }

        #endregion

        #region Implementation

        public override async Task RemovedAsync(RemoveContentContext context, ProfilePart part)
        {
            // Check if the user has permission to delete orchard user
            if (!await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext?.User, Permissions.ManageUsers)) {
                return;
            }

            await _profileGroupsService.AssignGroupAsync(part.ContentItem, null);

            var user = await _userService.GetUserByUniqueIdAsync(part.UserIdentifier);

            if (user != null) {
                await _userManager.DeleteAsync(user);
            }
        }

        #endregion

    }
}
