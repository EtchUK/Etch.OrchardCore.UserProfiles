using OrchardCore.ContentManagement;

namespace Etch.OrchardCore.UserProfiles.SubscriptionGroups.Models
{
    public class SubscriptionGroupPart : ContentPart
    {
        public string Identifier { get; set; }
        public bool IsSelected { get; set; }
    }
}
