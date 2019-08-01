using System.Collections.Generic;
using System.Linq;
using Etch.OrchardCore.UserProfiles.SubscriptionGroups.Models;

namespace Etch.OrchardCore.UserProfiles.SubscriptionGroups.Services
{
    public class SubscriptionGroupPartService : ISubscriptionGroupPartService
    {
        #region Implementations

        public SubscriptionGroupPart[] SelectedSubscriptionGroupParts(List<SubscriptionGroupPart> subscriptionGroups, ISubscriptionGroupContentPart part)
        {
            return subscriptionGroups.Select(x => new SubscriptionGroupPart
            {
                Identifier = x.Identifier,
                ContentItem = x.ContentItem,
                IsSelected = IsSelectedSubscriptionGroupPart(x, part)
            }).ToArray();
        }

        private bool IsSelectedSubscriptionGroupPart(SubscriptionGroupPart subscriptionGroupPart, ISubscriptionGroupContentPart part)
        {
            if (part.SubscriptionGroupSelection == null) {
                return false;
            }

            return part.SubscriptionGroupSelection.Any(x => x.Identifier == subscriptionGroupPart.Identifier && x.IsSelected);
        }

        #endregion
    }

    public interface ISubscriptionGroupPartService
    {
        SubscriptionGroupPart[] SelectedSubscriptionGroupParts(List<SubscriptionGroupPart> subscriptions, ISubscriptionGroupContentPart part);
    }
}
