using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Models;
using Microsoft.AspNetCore.Identity;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Environment.Shell;
using OrchardCore.Users;
using YesSql;
using Etch.OrchardCore.UserProfiles.Indexes;
using Microsoft.Extensions.DependencyInjection;

namespace Etch.OrchardCore.UserProfiles.Services
{
    public class ProfileService : IProfileService
    {
        #region Dependencies

        private readonly ISession _session;
        private readonly IShellHost _shellHost;
        private readonly ShellSettings _shellSettings;
        private readonly UserManager<IUser> _userManager;

        #endregion

        #region Constructor

        public ProfileService(ISession session, IShellHost shellHost, ShellSettings shellSettings, UserManager<IUser> userManager)
        {
            _session = session;
            _shellSettings = shellSettings;
            _shellHost = shellHost;
            _userManager = userManager;
        }

        #endregion

        #region Implementation

        public async Task<ContentItem> CreateAsync(IUser user)
        {
            using (var scope = await _shellHost.GetScopeAsync(_shellSettings))
            {
                var contentManager = scope.ServiceProvider.GetRequiredService<IContentManager>();
                var contentItem = await contentManager.NewAsync(Constants.ContentTypeName);

                var profile = contentItem.As<ProfilePart>();
                profile.UserIdentifier = await _userManager.GetUserIdAsync(user);
                profile.Apply();

                contentItem.DisplayText = user.UserName;

                contentItem.Apply(nameof(ProfilePart), profile);
                ContentExtensions.Apply(contentItem, contentItem);

                await contentManager.CreateAsync(contentItem);
                await contentManager.PublishAsync(contentItem);

                return contentItem;
            }
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
