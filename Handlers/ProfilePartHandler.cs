using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Models;
using Microsoft.AspNetCore.Identity;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.Users;
using OrchardCore.Users.Services;

namespace Etch.OrchardCore.UserProfiles.Handlers
{
    public class ProfilePartHandler : ContentPartHandler<ProfilePart>
    {

        #region Dependencies

        private readonly UserManager<IUser> _userManager;
        private readonly IUserService _userService;

        #endregion

        #region Constructor

        public ProfilePartHandler(UserManager<IUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        #endregion

        #region Implementation

        public override async Task RemovedAsync(RemoveContentContext context, ProfilePart part)
        {
            var user = await _userService.GetUserByUniqueIdAsync(part.UserIdentifier);

            if (user != null) {
                await _userManager.DeleteAsync(user);
            }
        }

        #endregion

    }
}
