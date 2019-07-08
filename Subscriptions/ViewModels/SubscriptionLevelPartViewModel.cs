using System.Collections.Generic;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.ViewModels
{
    public class SubscriptionLevelPartViewModel
    {
        public string Subscription { get; set; }

        public List<SubscriptionPart> Subscriptions { get; set; } = new List<SubscriptionPart>();
    }
}
