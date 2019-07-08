using Etch.OrchardCore.UserProfiles.Subscriptions.Drivers;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Etch.OrchardCore.UserProfiles.Subscriptions.Services;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;

namespace Etch.OrchardCore.UserProfiles.Subscriptions
{
    [Feature(Constants.Features.Subscriptions)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IContentPartDisplayDriver, SubscriptionPartDisplay>();
            services.AddScoped<IContentPartDisplayDriver, SubscriptionLevelPartDisplay>();
            services.AddScoped<ISubscriptionsService, SubscriptionsService>();
            services.AddScoped<IDataMigration, Migrations>();

            services.AddSingleton<ContentPart, SubscriptionPart>();
            services.AddSingleton<ContentPart, SubscriptionLevelPart>();

        }
    }
}
