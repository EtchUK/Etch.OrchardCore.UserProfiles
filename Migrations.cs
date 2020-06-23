using Etch.OrchardCore.UserProfiles.Indexes;
using Etch.OrchardCore.UserProfiles.Services;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Users.Indexes;
using OrchardCore.Users.Models;
using System.Threading.Tasks;
using YesSql;

namespace Etch.OrchardCore.UserProfiles
{
    public class Migrations : DataMigration
    {
        #region Constants

        public const int UserIdenityMaxLength = 40;

        #endregion

        #region Dependencies

        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IProfileService _profileService;
        private readonly ISession _session;

        #endregion

        #region Constructor

        public Migrations(IContentDefinitionManager contentDefinitionManager, IProfileService profileService, ISession session)
        {
            _contentDefinitionManager = contentDefinitionManager;
            _profileService = profileService;
            _session = session;
        }

        #endregion

        #region Migrations

        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinition("ProfilePart", builder => builder
                .WithDescription("Links content item to user.")
                .WithDefaultPosition("0")
            );

            _contentDefinitionManager.AlterTypeDefinition(Constants.ContentTypeName, type => type
                .WithPart("ProfilePart")
            );

            SchemaBuilder.CreateMapIndexTable(nameof(ProfilePartIndex), table => table
                .Column<string>("UserIdentifier", c => c.WithLength(UserIdenityMaxLength))
            );

            SchemaBuilder.AlterTable(nameof(ProfilePartIndex), table => table
                .CreateIndex("IDX_ProfilePartIndex_UserIdentifier", "UserIdentifier")
            );

            /**
             * Note:
             * Caused an issue on initial orchard recipe setup so commented this out
             */

            // await CreateProfilesForExistingUsersAsync();

            return 2;
        }

        public int UpdateFrom1()
        {
            SchemaBuilder.CreateMapIndexTable(nameof(ProfilePartIndex), table => table
                .Column<string>("UserIdentifier", c => c.WithLength(UserIdenityMaxLength))
            );

            SchemaBuilder.AlterTable(nameof(ProfilePartIndex), table => table
                .CreateIndex("IDX_ProfilePartIndex_UserIdentifier", "UserIdentifier")
            );

            return 2;
        }

        #endregion

        #region Helper Methods

        private async Task CreateProfilesForExistingUsersAsync()
        {
            var users = await _session.Query<User, UserIndex>().ListAsync();

            foreach (var user in users)
            {
                await _profileService.CreateAsync(user);
            }
        }

        #endregion
    }
}
