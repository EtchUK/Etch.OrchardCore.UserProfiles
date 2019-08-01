using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.SubscriptionGroups.Models;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Etch.OrchardCore.UserProfiles.Subscriptions.Services;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using YesSql;

namespace Etch.OrchardCore.UserProfiles.SubscriptionGroups.Services
{
    public class SubscriptionGroupsService : ISubscriptionGroupsService
    {
        #region Dependencies

        private readonly ISession _session;
        private readonly ISubscriptionsService _subscriptionsService;

        #endregion

        #region Constructor

        public SubscriptionGroupsService(ISession session, ISubscriptionsService subscriptionsService)
        {
            _session = session;
            _subscriptionsService = subscriptionsService;
        }

        #endregion

        #region Implementation

        public async Task<List<SubscriptionGroupPart>> GetAllAsync()
        {
            var contentItems = await _session.Query<ContentItem>()
                                      .With<ContentItemIndex>(x => x.Published && x.Latest && x.ContentType == Constants.ContentSubscriptionGroupTypeName)
                                      .ListAsync();

            var results = new List<SubscriptionGroupPart>();

            foreach (var contentItem in contentItems) {
                results.Add(new SubscriptionGroupPart
                {
                    ContentItem = contentItem,
                    Identifier = contentItem.As<SubscriptionGroupPart>().Identifier,
                });
            }

            return results.OrderBy(x => x.ContentItem.DisplayText).ToList();
        }

        public async Task<List<SubscriptionGroupPart>> GetAsync(SubscriptionLevelPart group, bool allowMultiple)
        {
            var subscriptions = await _subscriptionsService.GetAllAsync();

            if (subscriptions == null) {
                return null;
            }

            var results = new List<SubscriptionGroupPart>();

            foreach (var subscription in subscriptions) {

                if(allowMultiple && !group.SubscriptionSelection.Any(x => x.Identifier == subscription.Identifier && x.IsSelected)) {
                    continue;
                }

                if (!allowMultiple && group.Subscription != subscription.Identifier) {
                    continue;
                }

                results.Add(new SubscriptionGroupPart
                {
                    ContentItem = subscription.ContentItem.As<SubscriptionGroupSelectPart>().ContentItem,
                    Identifier = subscription.ContentItem.As<SubscriptionGroupSelectPart>().SubscriptionGroup
                });
            }

            return results;
        }

        #endregion
    }
}
