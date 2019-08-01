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
            services.AddScoped<IContentPartDisplayDriver, SubscriptionGroupPartDisplay>();
            services.AddScoped<IContentPartDisplayDriver, SubscriptionGroupSelectPartDisplay>();
            services.AddScoped<IContentPartDisplayDriver, SubscriptionGroupAccessPartDisplay>();

            services.AddScoped<ISubscriptionGroupsService, SubscriptionGroupsService>();
            services.AddScoped<ISubscriptionGroupPartService, SubscriptionGroupPartService>();

            services.AddSingleton<ContentPart, SubscriptionGroupPart>();
            services.AddSingleton<ContentPart, SubscriptionGroupSelectPart>();
            services.AddSingleton<ContentPart, SubscriptionGroupAccessPart>();

            services.AddScoped<IDataMigration, Migrations>();
        }
    }
}
