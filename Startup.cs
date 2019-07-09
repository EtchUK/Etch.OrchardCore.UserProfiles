using Etch.OrchardCore.UserProfiles.Drivers;
using Etch.OrchardCore.UserProfiles.Indexes;
using Etch.OrchardCore.UserProfiles.Models;
using Etch.OrchardCore.UserProfiles.Services;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using YesSql.Indexes;

namespace Etch.OrchardCore.UserProfiles
{
    [Feature(Constants.Features.Core)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // Profile Part
            services.AddScoped<IContentPartDisplayDriver, ProfilePartDisplay>();
            services.AddSingleton<ContentPart, ProfilePart>();

            services.AddScoped<IProfileService, ProfileService>();

            services.AddSingleton<IIndexProvider, ProfilePartIndexProvider>();

            services.AddScoped<IDataMigration, Migrations>();
        }
    }
}
