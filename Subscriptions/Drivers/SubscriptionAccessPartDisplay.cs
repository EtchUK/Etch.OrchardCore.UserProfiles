using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Etch.OrchardCore.UserProfiles.Subscriptions.Services;
using Etch.OrchardCore.UserProfiles.Subscriptions.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.Drivers
{

    public class SubscriptionAccessPartDisplay : ContentPartDisplayDriver<SubscriptionAccessPart>
    {

        #region Constructor

        private readonly ISubscriptionsService _subscriptionsService;

        public SubscriptionAccessPartDisplay(ISubscriptionsService subscriptionsService)
        {
            _subscriptionsService = subscriptionsService;
        }

        #endregion

        #region Overrides

        public override IDisplayResult Edit(SubscriptionAccessPart part)
        {
            return Initialize<SubscriptionAccessPartViewModel>("SubscriptionAccessPart_Edit", model => {
                var subscriptions = _subscriptionsService.GetAllAsync().GetAwaiter().GetResult();
                model.SubscriptionAccessSelection = SelectedSubscriptionParts(subscriptions, part);

                return Task.CompletedTask;
            });
        }

        public async override Task<IDisplayResult> UpdateAsync(SubscriptionAccessPart part, IUpdateModel updater)
        {
            var model = new SubscriptionAccessPartViewModel();

            if (await updater.TryUpdateModelAsync(model, Prefix)) {
                part.SubscriptionAccessSelection = model.SubscriptionAccessSelection;
            }

            return Edit(part);
        }

        #endregion

        #region Helpers

        private SubscriptionPart[] SelectedSubscriptionParts(List<SubscriptionPart> subscriptions, SubscriptionAccessPart part)
        {

            return subscriptions.Select(x => new SubscriptionPart
            {
                Identifier = x.Identifier,
                ContentItem = x.ContentItem,
                IsSelected = IsSelectedSubscriptionPart(x, part)
            }).ToArray();
        }

        private bool IsSelectedSubscriptionPart(SubscriptionPart subscriptionPart, SubscriptionAccessPart part)
        {

            if(part.SubscriptionAccessSelection == null) {
                return false;
            }

            return part.SubscriptionAccessSelection.Any(x => x.Identifier == subscriptionPart.Identifier && x.IsSelected);
        }

        #endregion


    }
}
