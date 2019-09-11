using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Models;
using Etch.OrchardCore.UserProfiles.Profile;
using Etch.OrchardCore.UserProfiles.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using OrchardCore.ContentManagement;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Modules;
using OrchardCore.Users;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;
using Permissions = Etch.OrchardCore.UserProfiles.Profile.Permissions;

namespace Etch.OrchardCore.UserProfiles.Controllers
{
    [Feature(Constants.Features.Core)]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IContentManager _contentManager;
        private readonly IProfileService _profileService;
        private readonly INotifier _notifier;
        private readonly IHtmlLocalizer TH;
        private readonly UserManager<IUser> _userManager;
        private readonly IUserService _userService;

        public AdminController(IAuthorizationService authorizationService, IContentManager contentManager, IProfileService profileService, INotifier notifier, IHtmlLocalizer<AdminController> htmlLocalizer, UserManager<IUser> userManager, IUserService userService)
        {
            _authorizationService = authorizationService;
            _contentManager = contentManager;
            _profileService = profileService;
            _notifier = notifier;
            TH = htmlLocalizer;
            _userManager = userManager;
            _userService = userService;
        }

        [Route("Etch.OrchardCore.Profiles/Admin/Index")]
        public async Task<ActionResult> Index()
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.ManageProfileMigration))
            {
                return Unauthorized();
            }

            return View();
        }

        [Route("Etch.OrchardCore.Profiles/Admin/CreateUsersFromProfile")]
        public async Task<ActionResult> CreateUsersFromProfile()
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.ManageProfileMigration))
            {
                return Unauthorized();
            }

            var profiles = await _profileService.GetAllAsync();

            var count = 0;

            foreach (ContentItem profile in profiles)
            {
                var email = profile.DisplayText;

                var user = await _userService.GetUserAsync(email);

                if (user != null)
                {
                    continue;
                }

                user = await _userService.CreateUserAsync(new User { UserName = email, Email = email }, null, (key, message) =>
                {
                    _notifier.Error(TH[$"{message}"]);
                    return;
                }) as User;

                await UpdateUserIdentifier(profile, user);

                count++;
            }

            if (count == 0)
            {
                _notifier.Success(TH[$"All profiles have associated users."]);
                return RedirectToAction("Index");
            }

            _notifier.Success(TH[$"{(count)} users successfully created."]);

            return RedirectToAction("Index");
        }

        #region Private

        public async Task UpdateUserIdentifier(ContentItem contentItem, IUser user)
        {
            var profilePart = contentItem.Get<ProfilePart>("ProfilePart");
            profilePart.UserIdentifier = await _userManager.GetUserIdAsync(user);
            contentItem.Apply("ProfilePart", profilePart);

            ContentExtensions.Apply(contentItem, contentItem);

            await _contentManager.UpdateAsync(contentItem);
        }

        #endregion
    }
}
