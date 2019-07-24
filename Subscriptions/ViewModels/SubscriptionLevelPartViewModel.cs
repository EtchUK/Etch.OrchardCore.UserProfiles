using System.Collections.Generic;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Etch.OrchardCore.UserProfiles.Subscriptions.Settings;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.ViewModels
{
    public class SubscriptionLevelPartViewModel
    {
        public string Subscription { get; set; }

        public List<SubscriptionPart> Subscriptions { get; set; } = new List<SubscriptionPart>();

        public SubscriptionPart[] SubscriptionSelection { get; set; }

        [BindNever]
        public SubscriptionLevelPartSettings Settings { get; set; }
    }
}
