using OrchardCore.ContentManagement;

namespace Etch.OrchardCore.UserProfiles.Models
{
    public class ProfilePart : ContentPart
    {
        public string UserIdentifier { get; set; }

        public string FullName { get; set; }
    }
}
