using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.SubscriptionGroups.Models;
using Etch.OrchardCore.UserProfiles.SubscriptionGroups.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.Drivers
{
    public class SubscriptionGroupPartDisplay : ContentPartDisplayDriver<SubscriptionGroupPart>
    {

        #region Overrides

        public override IDisplayResult Edit(SubscriptionGroupPart part)
        {
            return Initialize<SubscriptionGroupPartEditViewModel>("SubscriptionGroupPart_Edit", model =>
            {
                model.Identifier = part.Identifier;

                return Task.CompletedTask;
            });
        }

        public async override Task<IDisplayResult> UpdateAsync(SubscriptionGroupPart part, IUpdateModel updater)
        {
            var model = new SubscriptionGroupPartEditViewModel();

            if (await updater.TryUpdateModelAsync(model, Prefix)) {
                part.Identifier = model.Identifier;
            }

            return Edit(part);
        }

        #endregion

    }
}
