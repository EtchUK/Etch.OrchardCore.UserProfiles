using Etch.OrchardCore.UserProfiles.Grouping.Indexes;
using Etch.OrchardCore.UserProfiles.Grouping.Models;
using Etch.OrchardCore.UserProfiles.Grouping.ViewModels;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.DisplayManagement.Views;
using System;
using System.Linq;
using System.Threading.Tasks;
using YesSql;

namespace Etch.OrchardCore.UserProfiles.Grouping.Drivers
{
    public class ProfileGroupPartDisplay : ContentPartDisplayDriver<ProfileGroupPart>
    {
        #region Dependencies

        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IContentManager _contentManager;
        private readonly ISession _session;

        #endregion

        #region Constructor

        public ProfileGroupPartDisplay(IContentDefinitionManager contentDefinitionManager, IContentManager contentManager, ISession session)
        {
            _contentDefinitionManager = contentDefinitionManager;
            _contentManager = contentManager;
            _session = session;
        }

        #endregion

        #region Driver Methods

        public override async Task<IDisplayResult> EditAsync(ProfileGroupPart part, BuildPartEditorContext context)
        {
            var query = await _session.Query<ContentItem>()
                    .With<ProfileGroupedPartIndex>(x => x.GroupContentItemId == part.ContentItem.ContentItemId)
                    .ListAsync();

            return Initialize<ProfileGroupPartViewModel>("ProfileGroupPart_Edit", model =>
            {
                model.Items = query.OrderBy(x => x.DisplayText);
                model.PartDefinition = context.TypePartDefinition;
                model.Settings = GetSettings(part);
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(ProfileGroupPart part, BuildPartEditorContext context)
        {
            var model = new ProfileGroupPartViewModel();

            if (!await context.Updater.TryUpdateModelAsync(model, Prefix))
            {
                return await EditAsync(part, context);
            }

            var contentItemIds = string.IsNullOrWhiteSpace(model.ContentItemIds) ? new string[0] : model.ContentItemIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            await AssignNullToOrphanedProfiles(contentItemIds, part.ContentItem.ContentItemId);
            await AssignGroupToProfiles(contentItemIds, part.ContentItem.ContentItemId);

            return await EditAsync(part, context);
        }

        #endregion

        #region Helper Methods

        private async Task AssignNullToOrphanedProfiles(string[] contentItemIds, string contentItemId)
        {
            var existingProfiles = await _session.Query<ContentItem>()
                .With<ProfileGroupedPartIndex>(x => x.GroupContentItemId == contentItemId)
                .ListAsync();

            foreach (var existingProfile in existingProfiles)
            {
                if (contentItemIds.Contains(existingProfile.ContentItemId))
                {
                    continue;
                }

                await AssignGroupToProfile(existingProfile, null);
            }
        }

        private async Task AssignGroupToProfiles(string[] contentItemIds, string groupContentItemId)
        {
            foreach (var contentItemId in contentItemIds)
            {
                await AssignGroupToProfile(await _contentManager.GetAsync(contentItemId), groupContentItemId);
            }
        }

        private async Task AssignGroupToProfile(ContentItem profile, string groupContentItemId)
        {
            if (profile == null || profile.As<ProfileGroupedPart>() == null)
            {
                return;
            }

            profile.Alter<ProfileGroupedPart>(x => x.GroupContentItemId = groupContentItemId);

            profile.Apply(nameof(ProfileGroupedPart), profile.As<ProfileGroupedPart>());
            ContentExtensions.Apply(profile, profile);

            await _contentManager.UpdateAsync(profile);
            await _contentManager.PublishAsync(profile);
        }

        private ProfileGroupPartSettings GetSettings(ProfileGroupPart part)
        {
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(part.ContentItem.ContentType);
            var contentTypePartDefinition = contentTypeDefinition.Parts.FirstOrDefault(x => string.Equals(x.PartDefinition.Name, nameof(ProfileGroupPart), StringComparison.Ordinal));
            return contentTypePartDefinition.Settings.ToObject<ProfileGroupPartSettings>();
        }

        #endregion
    }
}
