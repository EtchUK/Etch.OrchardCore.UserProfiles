using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.SubscriptionGroups.Models;
using Etch.OrchardCore.UserProfiles.SubscriptionGroups.Services;
using Etch.OrchardCore.UserProfiles.SubscriptionGroups.ViewModels;
using Etch.OrchardCore.UserProfiles.Subscriptions.Services;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.Drivers
{

    public class SubscriptionGroupSelectPartDisplay : ContentPartDisplayDriver<SubscriptionGroupSelectPart>
    {

        #region Dependencies

        private readonly ISubscriptionGroupsService _subscriptionGroupsService;

        #endregion

        #region Constructor

        public SubscriptionGroupSelectPartDisplay(ISubscriptionGroupsService subscriptionGroupsService)
        {
            _subscriptionGroupsService = subscriptionGroupsService;
        }

        #endregion

        #region Overrides

        public override IDisplayResult Display(SubscriptionGroupSelectPart part, BuildPartDisplayContext context)
        {
            return null;
        }

        public override async Task<IDisplayResult> EditAsync(SubscriptionGroupSelectPart part, BuildPartEditorContext context)
        {
            return Initialize<SubscriptionGroupSelectPartViewModel>("SubscriptionGroupSelectPart_Edit", async model => {
                model.SubscriptionGroups = await _subscriptionGroupsService.GetAllAsync();
                model.SubscriptionGroup = part.SubscriptionGroup;
                return;
            });
        }

        public async override Task<IDisplayResult> UpdateAsync(SubscriptionGroupSelectPart part, IUpdateModel updater)
        {
            var model = new SubscriptionGroupSelectPartViewModel();

            if (await updater.TryUpdateModelAsync(model, Prefix)) {
                part.SubscriptionGroup = model.SubscriptionGroup;
            }

            return Edit(part);
        }

        #endregion

    }
}
