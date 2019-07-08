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

    public class SubscriptionLevelPartDisplay : ContentPartDisplayDriver<SubscriptionLevelPart>
    {

        #region Dependencies

        private readonly ISubscriptionsService _subscriptionsService;

        #endregion

        #region Constructor

        public SubscriptionLevelPartDisplay(ISubscriptionsService subscriptionsService)
        {
            _subscriptionsService = subscriptionsService;
        }

        #endregion

        #region Overrides

        public override async Task<IDisplayResult> EditAsync(SubscriptionLevelPart part, BuildPartEditorContext context)
        {
            return Initialize<SubscriptionLevelPartViewModel>("SubscriptionLevelPart_Edit", async model => {
                model.Subscription = part.Subscription;
                model.Subscriptions = await _subscriptionsService.GetAllAsync();

                return;
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
