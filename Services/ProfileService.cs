using System;
using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Models;
using Microsoft.AspNetCore.Identity;
using OrchardCore.ContentManagement;
using OrchardCore.Users;

namespace Etch.OrchardCore.UserProfiles.Services
{
    public class ProfileService : IProfileService
    {
        #region Dependencies

        private readonly IContentManager _contentManager;
        private readonly UserManager<IUser> _userManager;

        #endregion

        #region Constructor

        public ProfileService(IContentManager contentManager, UserManager<IUser> userManager)
        {
            _contentManager = contentManager;
            _userManager = userManager;
        }

        #endregion

        #region Implementation

        public async Task<ContentItem> CreateProfileAsync(IUser user)
        {
            var contentItem = await _contentManager.NewAsync(Constants.ContentTypeName);

            var profile = contentItem.As<ProfilePart>();
            profile.UserIdentifier = await _userManager.GetUserIdAsync(user);
            profile.Apply();

            contentItem.DisplayText = user.UserName;

            contentItem.Apply(nameof(ProfilePart), profile);
            ContentExtensions.Apply(contentItem, contentItem);

            await _contentManager.CreateAsync(contentItem);
            await _contentManager.PublishAsync(contentItem);

            return contentItem;
        }

        #endregion
    }
}
