using System;
using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Etch.OrchardCore.UserProfiles.Subscriptions.ViewModels;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.Settings
{
    public class SubscriptionLevelPartSettingsDriver : ContentTypePartDefinitionDisplayDriver
    {
        #region Driver Methods

        #region Edit

        public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition, IUpdateModel updater)
        {
            // Only show this setting on SubscriptionLevelPart
            if (!string.Equals(nameof(SubscriptionLevelPart), contentTypePartDefinition.PartDefinition.Name, StringComparison.Ordinal)) {
                return null;
            }

            return Initialize<EditSubscriptionLevelPartSettingsViewModel>("SubscriptionLevelPartSettings_Edit", model =>
            {
                var settings = contentTypePartDefinition.Settings.ToObject<SubscriptionLevelPartSettings>();

                model.Hint = settings.Hint;
                model.Multiple = settings.Multiple;
            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
        {
            var model = new EditSubscriptionLevelPartSettingsViewModel();

            await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.Hint, m => m.Multiple);

            context.Builder.WithSetting(nameof(SubscriptionLevelPartSettings.Hint), model.Hint);
            context.Builder.WithSetting(nameof(SubscriptionLevelPartSettings.Multiple), model.Multiple.ToString());

            return Edit(contentTypePartDefinition, context.Updater);
        }


        #endregion

        #endregion
    }
}

