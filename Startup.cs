using Etch.OrchardCore.UserProfiles.Drivers;
using Etch.OrchardCore.UserProfiles.Handlers;
using Etch.OrchardCore.UserProfiles.Indexes;
using Etch.OrchardCore.UserProfiles.Models;
using Etch.OrchardCore.UserProfiles.Services;
using Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.ViewModels;
using Fluid;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using YesSql.Indexes;

namespace Etch.OrchardCore.UserProfiles
{
    [Feature(Constants.Features.Core)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // Profile Part
            services.AddContentPart<ProfilePart>()
                .UseDisplayDriver<ProfilePartDisplay>()
                .AddHandler<ProfilePartHandler>();

            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IURLService, URLService>();

            services.AddSingleton<IIndexProvider, ProfilePartIndexProvider>();

            services.AddScoped<INavigationProvider, AdminMenu>();
            services.AddScoped<IDataMigration, Migrations>();

            services.Configure<TemplateOptions>(o =>
            {
                o.MemberAccessStrategy.Register<SubscriptionAccessViewModel>();
            });
        }
    }
}
