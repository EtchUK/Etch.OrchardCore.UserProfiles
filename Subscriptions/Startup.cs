using System;
using Etch.OrchardCore.UserProfiles.Subscriptions.Drivers;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
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
            services.AddSingleton<ContentPart, SubscriptionPart>();
            services.AddScoped<IDataMigration, Migrations>();
        }

        public override void Configure(IApplicationBuilder builder, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
        }
    }
}
