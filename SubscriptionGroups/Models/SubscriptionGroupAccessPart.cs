using OrchardCore.ContentManagement;

namespace Etch.OrchardCore.UserProfiles.SubscriptionGroups.Models
{
    public class SubscriptionGroupAccessPart : ContentPart
    {
        public SubscriptionGroupPart[] SubscriptionGroupSelection { get; set; }
    }
}
