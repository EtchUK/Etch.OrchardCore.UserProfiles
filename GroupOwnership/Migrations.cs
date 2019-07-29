using Etch.OrchardCore.UserProfiles.GroupField.Models;
using Etch.OrchardCore.UserProfiles.GroupOwnership.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;

namespace Etch.OrchardCore.UserProfiles.GroupOwnership
{
    public class Migrations : DataMigration
    {
        #region Dependencies

        private readonly IContentDefinitionManager _contentDefinitionManager;

        #endregion

        #region Constructor

        public Migrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        #endregion

        #region Migrations

        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinition(nameof(ProfileGroupOwnershipPart), builder => builder
                .Attachable()
                .WithDisplayName("Profile Group Ownership")
                .WithDescription("Add ability to assign ownership to a Profile Group, optionally restricting access.")
                .WithField(GroupOwnershipConstants.GroupFieldName, field => field
                    .OfType(nameof(ProfileGroupField))
                    .WithDisplayName("Owned by group")
                    .WithDescription("Specifies which group owns the current content")
                    .WithSetting("Hint", "Which group owns this content")
                    .WithSetting("Required", "false")
                    .WithSetting("DisplayMode", "Zero")
                ));

            return 1;
        }

        #endregion
    }
}
