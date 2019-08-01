using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.SubscriptionGroups.Models;
using Etch.OrchardCore.UserProfiles.SubscriptionGroups.Services;
using Etch.OrchardCore.UserProfiles.SubscriptionGroups.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;

namespace Etch.OrchardCore.UserProfiles.SubscriptionGroups.Drivers
{

    public class SubscriptionGroupAccessPartDisplay : ContentPartDisplayDriver<SubscriptionGroupAccessPart>
    {

        #region Dependencies

        private readonly ISubscriptionGroupPartService _subscriptionGroupPartService;
        private readonly ISubscriptionGroupsService _subscriptionGroupsService;

        #endregion

        #region Constructor

        public SubscriptionGroupAccessPartDisplay(ISubscriptionGroupPartService subscriptionGroupPartService, ISubscriptionGroupsService subscriptionGroupsService)
        {
            _subscriptionGroupPartService = subscriptionGroupPartService;
            _subscriptionGroupsService = subscriptionGroupsService;
        }

        #endregion

        #region Overrides

        public override IDisplayResult Display(SubscriptionGroupAccessPart part, BuildPartDisplayContext context)
        {
            return null;
        }

        public override async Task<IDisplayResult> EditAsync(SubscriptionGroupAccessPart part, BuildPartEditorContext context)
        {
            return Initialize<SubscriptionGroupAccessPartViewModel>("SubscriptionGroupAccessPart_Edit", async model => {
                var subscriptions = await _subscriptionGroupsService.GetAllAsync();
                model.SubscriptionGroupSelection = _subscriptionGroupPartService.SelectedSubscriptionGroupParts(subscriptions, part);

                return;
            });
        }

        public async override Task<IDisplayResult> UpdateAsync(SubscriptionGroupAccessPart part, IUpdateModel updater)
        {
            var model = new SubscriptionGroupAccessPartViewModel();

            if (await updater.TryUpdateModelAsync(model, Prefix)) {
                part.SubscriptionGroupSelection = model.SubscriptionGroupSelection;
            }

            return Edit(part);
        }

        #endregion

    }
}
