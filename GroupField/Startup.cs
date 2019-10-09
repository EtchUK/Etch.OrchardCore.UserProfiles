using Etch.OrchardCore.UserProfiles.GroupField.Drivers;
using Etch.OrchardCore.UserProfiles.GroupField.Models;
using Etch.OrchardCore.UserProfiles.GroupField.Services;
using Fluid;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Modules;
using System;

namespace Etch.OrchardCore.UserProfiles.GroupField
{
    [Feature(Constants.Features.GroupField)]
    public class Startup : StartupBase
    {
        public Startup()
        {
            TemplateContext.GlobalMemberAccessStrategy.Register<ProfileGroupField>();
        }

        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "ProfileGroupPicker",
                areaName: "Etch.OrchardCore.UserProfiles",
                pattern: "ProfileGroupPicker",
                defaults: new { controller = "ProfileGroupPicker", action = "List" }
            );
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ContentField, ProfileGroupField>();

            services.AddScoped<IContentFieldDisplayDriver, ProfileGroupFieldDisplayDriver>();
            services.AddScoped<IContentPartFieldDefinitionDisplayDriver, ProfileGroupFieldSettingsDriver>();

            services.AddScoped<IContentPickerResultProvider, ProfileGroupPickerResultProvider>();
        }
    }
}
