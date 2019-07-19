using System;
using System.Linq;
using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Etch.OrchardCore.UserProfiles.Subscriptions.Services;
using Etch.OrchardCore.UserProfiles.Subscriptions.Settings;
using Etch.OrchardCore.UserProfiles.Subscriptions.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.Drivers
{

    public class SubscriptionLevelPartDisplay : ContentPartDisplayDriver<SubscriptionLevelPart>
    {

        #region Dependencies

        private readonly ISubscriptionLevelService _subscriptionLevelService;
        private readonly ISubscriptionPartService _subscriptionPartService;
        private readonly ISubscriptionsService _subscriptionsService;

        #endregion

        #region Constructor

        public SubscriptionLevelPartDisplay(ISubscriptionLevelService subscriptionLevelService, ISubscriptionPartService subscriptionPartService, ISubscriptionsService subscriptionsService)
        {
            _subscriptionLevelService = subscriptionLevelService;
            _subscriptionPartService = subscriptionPartService;
            _subscriptionsService = subscriptionsService;
        }

        #endregion

        #region Overrides

        public override async Task<IDisplayResult> EditAsync(SubscriptionLevelPart part, BuildPartEditorContext context)
        {
            return Initialize<SubscriptionLevelPartViewModel>("SubscriptionLevelPart_Edit", async model => {
                model.Subscription = part.Subscription;
                model.Subscriptions = await _subscriptionsService.GetAllAsync();
                model.SubscriptionSelection = _subscriptionPartService.SelectedSubscriptionParts(model.Subscriptions, part);

                model.Settings = _subscriptionLevelService.GetSettings(part);

                return;
            });
        }

        public async override Task<IDisplayResult> UpdateAsync(SubscriptionLevelPart part, IUpdateModel updater)
        {
            var model = new SubscriptionLevelPartViewModel();

            if (await updater.TryUpdateModelAsync(model, Prefix)) {
                part.Subscription = model.Subscription;
                part.SubscriptionSelection = model.SubscriptionSelection;
            }

            return Edit(part);
        }

        #endregion

    }
}
