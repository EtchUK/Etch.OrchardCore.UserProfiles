using Etch.OrchardCore.UserProfiles.Subscriptions.Drivers;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Etch.OrchardCore.UserProfiles.Subscriptions.Services;
using Etch.OrchardCore.UserProfiles.Subscriptions.Settings;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;

namespace Etch.OrchardCore.UserProfiles.Subscriptions
{
    [Feature(Constants.Features.Subscriptions)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddContentPart<SubscriptionPart>()
                .UseDisplayDriver<SubscriptionPartDisplay>();

            services.AddContentPart<SubscriptionLevelPart>()
                .UseDisplayDriver<SubscriptionLevelPartDisplay>();

            services.AddContentPart<SubscriptionAccessPart>()
                .UseDisplayDriver<SubscriptionAccessPartDisplay>();

            services.AddScoped<ISubscriptionsService, SubscriptionsService>();
            services.AddScoped<ISubscriptionPartService, SubscriptionPartService>();
            services.AddScoped<ISubscriptionLevelService, SubscriptionLevelService>();

            services.AddScoped<IDataMigration, Migrations>();

            services.AddScoped<IContentTypePartDefinitionDisplayDriver, SubscriptionLevelPartSettingsDriver>();
        }
    }
}
