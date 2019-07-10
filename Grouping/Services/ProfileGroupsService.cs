
using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Grouping.Models;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using YesSql;

namespace Etch.OrchardCore.UserProfiles.Grouping.Services
{
    public class ProfileGroupsService : IProfileGroupsService {

        #region Dependencies

        private readonly IContentManager _contentManager;
        private readonly ISession _session;

        #endregion

        #region Constructor

        public ProfileGroupsService(IContentManager contentManager, ISession session) {
            _contentManager = contentManager;
            _session = session;
        }

        #endregion

        #region Implementation

        public async Task<ContentItem> AssignGroupAsync(ContentItem profile, string groupContentItemId) {

            profile.Alter<ProfileGroupedPart>(x => x.GroupContentItemId = groupContentItemId);

            profile.Apply(nameof(ProfileGroupedPart), profile.As<ProfileGroupedPart>());
            ContentExtensions.Apply(profile, profile);

            await _contentManager.UpdateAsync(profile);
            await _contentManager.PublishAsync(profile);

            return profile;
        }

        public async Task<ContentItem> GetAsync(ContentItem contentItem)
        {
            var profileGroupedPart = contentItem.As<ProfileGroupedPart>();

            if(profileGroupedPart == null) {
                return null;
            }

            var contentItems = await _session.Query<ContentItem>()
                                      .With<ContentItemIndex>(x => x.Published && x.Latest && x.ContentItemId == profileGroupedPart.GroupContentItemId)
                                      .FirstOrDefaultAsync();

            return contentItems;
        }

        public async Task<SubscriptionLevelPart> GetSubscriptionAccessAsync(ContentItem contentItem)
        {
            return (await GetAsync(contentItem)).As<SubscriptionLevelPart>();
        }
 
        #endregion
    }
}
