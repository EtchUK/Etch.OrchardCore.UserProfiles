﻿using Etch.OrchardCore.UserProfiles.Grouping.Indexes;
using Etch.OrchardCore.UserProfiles.Grouping.Models;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Environment.Shell;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesSql;
using YesSql.Services;

namespace Etch.OrchardCore.UserProfiles.Grouping.Services
{
    public class ProfileGroupsService : IProfileGroupsService {

        #region Dependencies

        private readonly IContentManager _contentManager;
        private readonly ISession _session;

        #endregion

        #region Constructor

        public ProfileGroupsService(IContentManager contentManager, ISession session) 
        {
            _contentManager = contentManager;
            _session = session;
        }

        #endregion

        #region Implementation

        public async Task<ContentItem> AssignGroupAsync(ContentItem profile, string groupContentItemId) 
        {
            if (profile == null) {
                return null;
            }

            profile.Alter<ProfileGroupedPart>(x => x.GroupContentItemId = groupContentItemId);

            profile.Apply(nameof(ProfileGroupedPart), profile.As<ProfileGroupedPart>());
            ContentExtensions.Apply(profile, profile);

            await _contentManager.UpdateAsync(profile);
            await _contentManager.PublishAsync(profile);

            return profile;
        }

        public async Task<IList<ContentItem>> GetAllGroupsAsync() {
            return (await _session.Query<ContentItem>()
                .With<ContentItemIndex>(x => x.Published)
                .With<ProfileGroupPartIndex>(x => x.Id == x.Id)
                .ListAsync())
                .ToList();
        }

        public async Task<IList<ContentItem>> GetGroupsByIdsAsync(ICollection<string> ids) {
            return (await _session.Query<ContentItem>()
                .With<ContentItemIndex>(x => x.Published && x.ContentItemId.IsIn(ids))
                .With<ProfileGroupPartIndex>(x => x.Id == x.Id)
                .ListAsync())
                .ToList();
        }

        public async Task<ContentItem> GetAsync(ContentItem contentItem)
        {
            var profileGroupedPart = contentItem.As<ProfileGroupedPart>();

            if (profileGroupedPart == null) {
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
