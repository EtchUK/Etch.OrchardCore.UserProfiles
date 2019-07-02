using Etch.OrchardCore.UserProfiles.Grouping.Indexes;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;

namespace Etch.OrchardCore.UserProfiles.Grouping
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
            _contentDefinitionManager.AlterPartDefinition("ProfileGroupPart", builder => builder
                .Attachable()
                .WithDescription("Add ability to group user profiles."));

            SchemaBuilder.CreateMapIndexTable(nameof(ProfileGroupedPartIndex), table => table
                .Column<string>("GroupContentItemId", c => c.WithLength(26))
            );

            SchemaBuilder.AlterTable(nameof(ProfileGroupedPartIndex), table => table
                .CreateIndex("IDX_ProfileGroupedPartIndex_GroupContentItemId", "GroupContentItemId")
            );

            _contentDefinitionManager.AlterTypeDefinition(Constants.ContentTypeName, type => type
                .WithPart("ProfileGroupedPart")
            );

            return 1;
        }

        #endregion
    }
}
