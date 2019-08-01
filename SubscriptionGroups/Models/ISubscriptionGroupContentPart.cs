using OrchardCore.ContentManagement;

namespace Etch.OrchardCore.UserProfiles.SubscriptionGroups.Models
{
    public class ISubscriptionGroupContentPart : ContentPart
    {
        public SubscriptionGroupPart[] SubscriptionGroupSelection { get; set; }
    }
}
