using System.Collections.Generic;
using System.Linq;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.Services
{
    public class SubscriptionPartService : ISubscriptionPartService
    {
        #region Implementations

        public SubscriptionPart[] SelectedSubscriptionParts(List<SubscriptionPart> subscriptions, ISubscriptionContentPart part)
        {
            return subscriptions.Select(x => new SubscriptionPart
            {
                Identifier = x.Identifier,
                ContentItem = x.ContentItem,
                IsSelected = IsSelectedSubscriptionPart(x, part)
            }).ToArray();
        }

        private bool IsSelectedSubscriptionPart(SubscriptionPart subscriptionPart, ISubscriptionContentPart part)
        {
            if (part.SubscriptionSelection == null) {
                return false;
            }

            return part.SubscriptionSelection.Any(x => x.Identifier == subscriptionPart.Identifier && x.IsSelected);
        }

        #endregion

    }

    public interface ISubscriptionPartService
    {
        SubscriptionPart[] SelectedSubscriptionParts(List<SubscriptionPart> subscriptions, ISubscriptionContentPart part);
    }
}
