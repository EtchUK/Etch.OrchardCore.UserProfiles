using Etch.OrchardCore.UserProfiles.GroupOwnership.Drivers;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddScoped<IContentTypePartDefinitionDisplayDriver, ProfileGroupOwnershipPartSettingsDisplay>();
        }
    }
}
