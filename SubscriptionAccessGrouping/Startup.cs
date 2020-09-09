using Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.Drivers;
using Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.Services;
using Etch.OrchardCore.UserProfiles.SubscriptionGroups.Services;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;

namespace Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping
{
    [Feature(Constants.Features.SubscriptionAccessGrouping)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<INavigationProvider, UKIE.OrchardCore.UserProfiles.SubscriptionAccessGrouping.AdminMenu>();
            services.AddScoped<IDisplayDriver<ISite>, SubscriptionAccessSettingsDisplay>();
            services.AddScoped<IPermissionProvider, UKIE.OrchardCore.UserProfiles.SubscriptionAccessGrouping.Permissions>();
            services.AddScoped<ISubscriptionAccessSettingsService, SubscriptionAccessSettingsService>();
            services.AddScoped<IAccessAuthorizationService, AccessAuthorizationService>();
            services.AddScoped<IContentPartDisplayDriver, SubscriptionAccessGroupingPartDisplay>();
            services.AddScoped<ISubscriptionGroupsService, SubscriptionGroupsService>();
        }
    }
}