using OrchardCore.ContentManagement;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.Models
{
    public class SubscriptionPart : ContentPart
    {
        public string Identifier { get; set; }
        public bool IsSelected { get; set; }
    }
}
