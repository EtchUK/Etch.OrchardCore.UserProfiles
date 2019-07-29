using Etch.OrchardCore.UserProfiles.GroupOwnership.Drivers;
using Etch.OrchardCore.UserProfiles.GroupOwnership.Models;
using Etch.OrchardCore.UserProfiles.GroupOwnership.Services;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;

namespace Etch.OrchardCore.UserProfiles.GroupOwnership
{
    [Feature(Constants.Features.GroupOwnership)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDataMigration, Migrations>();

            services.AddScoped<IContentPartDisplayDriver, ProfileGroupOwnershipPartDisplay>();
            services.AddScoped<IContentTypePartDefinitionDisplayDriver, ProfileGroupOwnershipPartSettingsDisplay>();

            services.AddScoped<IOwnershipAuthorizationService, OwnershipAuthorizationService>();

            services.AddSingleton<ContentPart, ProfileGroupOwnershipPart>();
        }
    }
}
