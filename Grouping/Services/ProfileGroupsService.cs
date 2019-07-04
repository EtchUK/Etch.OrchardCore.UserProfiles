
using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Grouping.Models;
using OrchardCore.ContentManagement;

namespace Etch.OrchardCore.UserProfiles.Grouping.Services {
    public class ProfileGroupsService : IProfileGroupsService {

        #region Dependencies

        private readonly IContentManager _contentManager;

        #endregion

        #region Constructor

        public ProfileGroupsService(IContentManager contentManager) {
            _contentManager = contentManager;
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

        #endregion
    }

    public interface IProfileGroupsService {
        Task<ContentItem> AssignGroupAsync(ContentItem profile, string groupContentItemId);
    }
}
