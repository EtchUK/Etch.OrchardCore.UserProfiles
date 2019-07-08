using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;

namespace Etch.OrchardCore.UserProfiles.Subscriptions
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
            _contentDefinitionManager.AlterPartDefinition("SubscriptionPart", builder => builder
                .WithDescription("Properties for subscription."));

            _contentDefinitionManager.AlterTypeDefinition(Constants.ContentSubscriptionTypeName, type => type
                .WithPart("TitlePart")
                .WithPart("SubscriptionPart")
                .Creatable()
                .Listable()
            );

            return 1;
        }

        #endregion
    }
}
