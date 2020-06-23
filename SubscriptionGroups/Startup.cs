using Etch.OrchardCore.UserProfiles.SubscriptionGroups.Drivers;
using Etch.OrchardCore.UserProfiles.SubscriptionGroups.Models;
using Etch.OrchardCore.UserProfiles.SubscriptionGroups.Services;
using Etch.OrchardCore.UserProfiles.Subscriptions.Drivers;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;

namespace Etch.OrchardCore.UserProfiles.SubscriptionGroups
{
    [Feature(Constants.Features.SubscriptionGroups)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddContentPart<SubscriptionGroupPart>()
                .UseDisplayDriver<SubscriptionGroupPartDisplay>();

            services.AddContentPart<SubscriptionGroupSelectPart>()
                .UseDisplayDriver<SubscriptionGroupSelectPartDisplay>();

            services.AddContentPart<SubscriptionGroupAccessPart>()
                .UseDisplayDriver<SubscriptionGroupAccessPartDisplay>();

            services.AddScoped<ISubscriptionGroupsService, SubscriptionGroupsService>();
            services.AddScoped<ISubscriptionGroupPartService, SubscriptionGroupPartService>();

            services.AddScoped<IDataMigration, Migrations>();
        }
    }
}
