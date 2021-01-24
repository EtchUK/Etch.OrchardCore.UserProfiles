using Etch.OrchardCore.UserProfiles.Grouping.Indexes;
using Etch.OrchardCore.UserProfiles.Grouping.Models;
using Etch.OrchardCore.UserProfiles.Grouping.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using YesSql.Sql;

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

           SchemaBuilder.CreateMapIndexTable<ProfileGroupPartIndex>( table => { });

            SchemaBuilder.CreateMapIndexTable<ProfileGroupedPartIndex>( table => table
                .Column<string>("GroupContentItemId", c => c.WithLength(26))
            );

            SchemaBuilder.AlterIndexTable<ProfileGroupedPartIndex>( table => table
                .CreateIndex("IDX_ProfileGroupedPartIndex_GroupContentItemId", "GroupContentItemId")
            );

            _contentDefinitionManager.AlterTypeDefinition(Constants.ContentTypeName, type => type
                .WithPart("ProfileGroupedPart")
            );

            return 2;
        }

        public int UpdateFrom1()
        {
            SchemaBuilder.CreateMapIndexTable<ProfileGroupPartIndex>( table => { });
            return 2;
        }

        public int UpdateFrom2()
        {
            _contentDefinitionManager.MigratePartSettings<ProfileGroupPart, ProfileGroupPartSettings>();
            return 3;
        }

        #endregion
    }
}
