using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Models;
using Microsoft.AspNetCore.Identity;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Users;
using YesSql;
using Etch.OrchardCore.UserProfiles.Indexes;

namespace Etch.OrchardCore.UserProfiles.Services
{
    public class ProfileService : IProfileService
    {
        #region Dependencies

        private readonly IContentManager _contentManager;
        private readonly UserManager<IUser> _userManager;
        private readonly ISession _session;

        #endregion

        #region Constructor

        public ProfileService(IContentManager contentManager, UserManager<IUser> userManager, ISession session)
        {
            _contentManager = contentManager;
            _userManager = userManager;
            _session = session;
        }

        #endregion

        #region Implementation

        public async Task<ContentItem> CreateAsync(IUser user)
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

        public async Task<ContentItem> GetAsync(IUser user)
        {
            var userIdentifier = await _userManager.GetUserIdAsync(user);

            var contentItems = await _session.Query<ContentItem>()
                                      .With<ContentItemIndex>(x => x.Published && x.Latest && x.ContentType == Constants.ContentTypeName)
                                      .With<ProfilePartIndex>(x => x.UserIdentifier == userIdentifier)
                                      .FirstOrDefaultAsync();

            return contentItems;
        }

        #endregion
    }
}
