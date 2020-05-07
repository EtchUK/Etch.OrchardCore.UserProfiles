using Etch.OrchardCore.UserProfiles.Grouping.Models;
using Etch.OrchardCore.UserProfiles.Grouping.ViewModels;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using System;
using System.Threading.Tasks;

namespace Etch.OrchardCore.UserProfiles.Grouping.Settings
{
    public class ProfileGroupPartSettingsDisplayDriver : ContentTypePartDefinitionDisplayDriver
    {

        public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition, IUpdateModel updater)
        {
            if (!string.Equals(nameof(ProfileGroupPart), contentTypePartDefinition.PartDefinition.Name, StringComparison.Ordinal))
            {
                return null;
            }

            return Initialize<ProfileGroupPartSettingsViewModel>("ProfileGroupPartSettings_Edit", model =>
            {
                var settings = contentTypePartDefinition.Settings.ToObject<ProfileGroupPartSettings>();

                model.Hint = settings.Hint;
                model.Label = settings.Label;
                model.ProfileGroupPartSettings = settings;
            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
        {
            if (!string.Equals(nameof(ProfileGroupPart), contentTypePartDefinition.PartDefinition.Name, StringComparison.Ordinal))
            {
                return null;
            }

            var model = new ProfileGroupPartSettings();

            await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.Hint, m => m.Label);

            context.Builder.WithSettings(model);

            return Edit(contentTypePartDefinition, context.Updater);
        }
    }
}
