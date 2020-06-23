using Etch.OrchardCore.UserProfiles.GroupOwnership.Models;
using Etch.OrchardCore.UserProfiles.GroupOwnership.ViewModels;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using System;
using System.Threading.Tasks;

namespace Etch.OrchardCore.UserProfiles.GroupOwnership.Drivers
{
    public class ProfileGroupOwnershipPartSettingsDisplay : ContentTypePartDefinitionDisplayDriver
    {
        #region ContentPartFieldDefinitionDisplayDriver

        #region Edit

        public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition, IUpdateModel updater)
        {
            if (!IsProfileGroupOwnership(contentTypePartDefinition))
            {
                return null;
            }

            return Initialize<ProfileGroupOwnershipPartSettingsViewModel>("ProfileGroupOwnershipPartSettings_Edit", model =>
            {
                model.RestrictAccess = contentTypePartDefinition.GetSettings<ProfileGroupOwnershipPartSettings>().RestrictAccess;
            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
        {
            if (!IsProfileGroupOwnership(contentTypePartDefinition))
            {
                return null;
            }

            var model = new ProfileGroupOwnershipPartSettingsViewModel();

            await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.RestrictAccess);

            context.Builder.WithSettings(new ProfileGroupOwnershipPartSettings
            {
                RestrictAccess = model.RestrictAccess
            });

            return Edit(contentTypePartDefinition, context.Updater);
        }

        #endregion Edit

        #endregion ContentPartFieldDefinitionDisplayDriver

        #region Private

        private bool IsProfileGroupOwnership(ContentTypePartDefinition contentTypePartDefinition)
        {
            return string.Equals(nameof(ProfileGroupOwnershipPart), contentTypePartDefinition.Name, StringComparison.OrdinalIgnoreCase);
        }

        #endregion Private
    }
}
