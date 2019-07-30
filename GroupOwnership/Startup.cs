using Etch.OrchardCore.UserProfiles.GroupOwnership.Drivers;
using Etch.OrchardCore.UserProfiles.GroupOwnership.Indexes;
using Etch.OrchardCore.UserProfiles.GroupOwnership.Models;
using Etch.OrchardCore.UserProfiles.GroupOwnership.Services;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using YesSql.Indexes;

namespace Etch.OrchardCore.UserProfiles.GroupOwnership
{
    [Feature(Constants.Features.GroupOwnership)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IIndexProvider, GroupOwnershipIndexProvider>();

            services.AddScoped<IDataMigration, Migrations>();

            services.AddScoped<IContentPartDisplayDriver, ProfileGroupOwnershipPartDisplay>();
            services.AddScoped<IContentTypePartDefinitionDisplayDriver, ProfileGroupOwnershipPartSettingsDisplay>();

            services.AddScoped<IOwnershipAuthorizationService, OwnershipAuthorizationService>();

            services.AddSingleton<ContentPart, ProfileGroupOwnershipPart>();
        }
    }
}
