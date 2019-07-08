using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Etch.OrchardCore.UserProfiles.Subscriptions.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.Drivers
{
    public class SubscriptionPartDisplay : ContentPartDisplayDriver<SubscriptionPart>
    {

        #region Overrides

        public override IDisplayResult Edit(SubscriptionPart part)
        {
            return Initialize<SubscriptionPartEditViewModel>("SubscriptionPart_Edit", model =>
            {
                model.Identifier = part.Identifier;

                return Task.CompletedTask;
            });
        }

        public async override Task<IDisplayResult> UpdateAsync(SubscriptionPart part, IUpdateModel updater)
        {
            var model = new SubscriptionPartEditViewModel();

            if (await updater.TryUpdateModelAsync(model, Prefix)) {
                part.Identifier = model.Identifier;
            }

            return Edit(part);
        }

        #endregion

    }
}
