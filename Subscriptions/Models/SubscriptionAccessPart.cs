using OrchardCore.ContentManagement;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.Models
{
    public class SubscriptionAccessPart : ContentPart
    {
        public SubscriptionPart[] SubscriptionAccessSelection { get; set; }
    }
}
