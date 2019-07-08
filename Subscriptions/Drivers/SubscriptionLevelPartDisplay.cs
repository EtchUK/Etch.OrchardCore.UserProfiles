using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Etch.OrchardCore.UserProfiles.Subscriptions.Services;
using Etch.OrchardCore.UserProfiles.Subscriptions.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.Drivers
{

    public class SubscriptionLevelPartDisplay : ContentPartDisplayDriver<SubscriptionLevelPart>
    {

        #region Constructor

        private readonly ISubscriptionsService _subscriptionsService;

        public SubscriptionLevelPartDisplay(ISubscriptionsService subscriptionsService)
        {
            _subscriptionsService = subscriptionsService;
        }

        #endregion

        #region Overrides

        public override IDisplayResult Edit(SubscriptionLevelPart part)
        {
            return Initialize<SubscriptionLevelPartViewModel>("SubscriptionLevelPart_Edit", model => {
                model.Subscription = part.Subscription;
                model.Subscriptions = _subscriptionsService.GetAllAsync().GetAwaiter().GetResult();

                return Task.CompletedTask;
            });
        }

        public async override Task<IDisplayResult> UpdateAsync(SubscriptionLevelPart part, IUpdateModel updater)
        {
            var model = new SubscriptionLevelPartViewModel();

            if (await updater.TryUpdateModelAsync(model, Prefix)) {
                part.Subscription = model.Subscription;
            }

            return Edit(part);
        }

        #endregion

    }
}
