using Etch.OrchardCore.UserProfiles.Grouping.Drivers;
using Etch.OrchardCore.UserProfiles.Grouping.Indexes;
using Etch.OrchardCore.UserProfiles.Grouping.Models;
using Etch.OrchardCore.UserProfiles.Grouping.Services;
using Etch.OrchardCore.UserProfiles.Grouping.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using System;
using YesSql.Indexes;

namespace Etch.OrchardCore.UserProfiles.Grouping
{
    [Feature(Constants.Features.Grouping)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IIndexProvider, ProfileGroupedPartIndexProvider>();

            services.AddScoped<IContentPartDisplayDriver, ProfileGroupPartDisplay>();
            services.AddScoped<IContentTypePartDefinitionDisplayDriver, ProfileGroupPartSettingsDisplayDriver>();
            services.AddSingleton<ContentPart, ProfileGroupedPart>();
            services.AddSingleton<ContentPart, ProfileGroupPart>();

            services.AddScoped<IContentPickerResultProvider, ProfilePickerResultProvider>();

            services.AddScoped<IDataMigration, Migrations>();
        }

        public override void Configure(IApplicationBuilder builder, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaRoute(
                name: "ProfilePicker",
                areaName: "Etch.OrchardCore.UserProfiles",
                template: "ProfilePicker",
                defaults: new { controller = "ProfilePicker", action = "List" }
            );
        }
    }
}
