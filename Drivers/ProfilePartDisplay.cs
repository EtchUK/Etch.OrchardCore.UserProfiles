using Etch.OrchardCore.UserProfiles.Models;
using Etch.OrchardCore.UserProfiles.ViewModels;
using Microsoft.AspNetCore.Identity;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Users;
using OrchardCore.Users.Services;
using System.Threading.Tasks;

namespace Etch.OrchardCore.UserProfiles.Drivers
{
    public class ProfilePartDisplay : ContentPartDisplayDriver<ProfilePart>
    {
        #region Dependencies

        private readonly UserManager<IUser> _userManager;
        private readonly IUserService _userService;

        #endregion

        #region Constructor

        public ProfilePartDisplay(UserManager<IUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        #endregion

        #region Display Driver

        public override async Task<IDisplayResult> DisplayAsync(ProfilePart part, BuildPartDisplayContext context)
        {
            var user = await _userService.GetUserByUniqueIdAsync(part.UserIdentifier);

            return Initialize<ProfilePartViewModel>("ProfilePart", model =>
            {
                model.UserName = user.UserName;
            });
        }

        public override async Task<IDisplayResult> EditAsync(ProfilePart part, BuildPartEditorContext context)
        {
            var user = await _userService.GetUserByUniqueIdAsync(part.UserIdentifier);

            return Initialize<ProfilePartViewModel>("ProfilePart_Edit", model =>
            {
                model.UserName = user.UserName;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(ProfilePart part, BuildPartEditorContext context)
        {
            var model = new ProfilePartViewModel();

            if (!await context.Updater.TryUpdateModelAsync(model, Prefix))
            {
                return Edit(part);
            }

            var user = await _userService.GetUserAsync(model.UserName);

            if (user != null)
            {
                part.UserIdentifier = await _userManager.GetUserIdAsync(user);
            }
            else
            {
                context.Updater.ModelState.AddModelError("UserName", $"Unable to find user with matching username: {model.UserName}");
            }

            return await EditAsync(part, context);
        }

        #endregion
    }
}
