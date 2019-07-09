using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Etch.OrchardCore.UserProfiles.Subscriptions.Services;
using Etch.OrchardCore.UserProfiles.Subscriptions.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.Drivers
{

    public class SubscriptionAccessPartDisplay : ContentPartDisplayDriver<SubscriptionAccessPart>
    {

        #region Dependencies

        private readonly ISubscriptionsService _subscriptionsService;

        #endregion

        #region Constructor

        public SubscriptionAccessPartDisplay(ISubscriptionsService subscriptionsService)
        {
            _subscriptionsService = subscriptionsService;
        }

        #endregion

        #region Overrides

        public override IDisplayResult Display(SubscriptionAccessPart part, BuildPartDisplayContext context)
        {
            return null;
        }

        public override async Task<IDisplayResult> EditAsync(SubscriptionAccessPart part, BuildPartEditorContext context)
        {
            return Initialize<SubscriptionAccessPartViewModel>("SubscriptionAccessPart_Edit", async model => {
                var subscriptions = await _subscriptionsService.GetAllAsync();
                model.SubscriptionAccessSelection = SelectedSubscriptionParts(subscriptions, part);

                return;
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
