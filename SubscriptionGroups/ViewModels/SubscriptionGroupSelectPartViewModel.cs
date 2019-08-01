using System.Collections.Generic;
using Etch.OrchardCore.UserProfiles.SubscriptionGroups.Models;

namespace Etch.OrchardCore.UserProfiles.SubscriptionGroups.ViewModels
{
    public class SubscriptionGroupSelectPartViewModel
    {
        public List<SubscriptionGroupPart> SubscriptionGroups { get; set; } = new List<SubscriptionGroupPart>();
        public string SubscriptionGroup { get; set; }
    }
}
