using Etch.OrchardCore.UserProfiles.Models;
using Etch.OrchardCore.UserProfiles.Services;
using Etch.OrchardCore.UserProfiles.ViewModels;
using Microsoft.AspNetCore.Identity;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Users;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;
using System.Threading.Tasks;

namespace Etch.OrchardCore.UserProfiles.Drivers
{
    public class ProfilePartDisplay : ContentPartDisplayDriver<ProfilePart>
    {
        #region Dependencies

        private readonly IURLService _urlService;
        private readonly UserManager<IUser> _userManager;
        private readonly IUserService _userService;

        #endregion

        #region Constructor

        public ProfilePartDisplay(IURLService urlService, UserManager<IUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
            _urlService = urlService;
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
            var user = (User)await _userService.GetUserByUniqueIdAsync(part.UserIdentifier);

            return Initialize<ProfilePartViewModel>("ProfilePart_Edit", model =>
            {
                model.UserName = (user == null ? string.Empty : user.UserName);
                model.Id = (user == null ? 0 : user.Id);
                model.SiteURL = _urlService.GetTenantUrl();
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(ProfilePart part, IUpdateModel updater, UpdatePartEditorContext context)
        {
            var model = new ProfilePartViewModel();

            if (!await context.Updater.TryUpdateModelAsync(model, Prefix))
            {
                return await EditAsync(part, context);
            }

            var user = await _userService.GetUserAsync(model.UserName);

            if (user == null)
            {
                user = await _userService.CreateUserAsync(new User { UserName = model.UserName, Email = model.UserName }, null, (key, message) =>
                {
                    context.Updater.ModelState.AddModelError("UserName", $"{message}");
                }) as User;
            }

            part.UserIdentifier = await _userManager.GetUserIdAsync(user);

            return await EditAsync(part, context);
        }

        #endregion

    }
}
