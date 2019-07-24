using Etch.OrchardCore.UserProfiles.GroupField.Drivers;
using Etch.OrchardCore.UserProfiles.GroupField.Models;
using Fluid;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Modules;

namespace Etch.OrchardCore.UserProfiles.GroupField
{
    [Feature(Constants.Features.GroupField)]
    public class Startup : StartupBase
    {
        public Startup()
        {
            TemplateContext.GlobalMemberAccessStrategy.Register<ProfileGroupField>();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ContentField, ProfileGroupField>();

            services.AddScoped<IContentFieldDisplayDriver, ProfileGroupFieldDisplayDriver>();
            services.AddScoped<IContentPartFieldDefinitionDisplayDriver, ProfileGroupFieldSettingsDriver>();
        }
    }
}
