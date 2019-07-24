using OrchardCore.ContentManagement;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.Models
{
    public class ISubscriptionContentPart : ContentPart
    {
        public SubscriptionPart[] SubscriptionSelection { get; set; }
    }
}
