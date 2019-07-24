using Etch.OrchardCore.UserProfiles.GroupField.Models;
using Etch.OrchardCore.UserProfiles.GroupField.ViewModels;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Views;
using System.Threading.Tasks;

namespace Etch.OrchardCore.UserProfiles.GroupField.Drivers
{
    public class ProfileGroupFieldSettingsDriver : ContentPartFieldDefinitionDisplayDriver<ProfileGroupField>
    {
        #region ContentPartFieldDefinitionDisplayDriver

        #region Edit

        public override IDisplayResult Edit(ContentPartFieldDefinition partFieldDefinition)
        {
            return Initialize<EditProfileGroupFieldSettingsViewModel>("ProfileGroupFieldSettings_Edit", model =>
            {
                partFieldDefinition.Settings.Populate(model);
            })
            .Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentPartFieldDefinition partFieldDefinition, UpdatePartFieldEditorContext context)
        {
            var model = new EditProfileGroupFieldSettingsViewModel();

            if (await context.Updater.TryUpdateModelAsync(model, Prefix))
            {
                context.Builder.MergeSettings(model);
            }

            return Edit(partFieldDefinition);
        }

        #endregion Edit

        #endregion ContentPartFieldDefinitionDisplayDriver
    }
}
