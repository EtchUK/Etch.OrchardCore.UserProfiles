using Etch.OrchardCore.UserProfiles.GroupOwnership.Models;
using OrchardCore.ContentManagement.Metadata.Models;

namespace Etch.OrchardCore.UserProfiles.GroupOwnership.ViewModels
{
    public class EditProfileGroupOwnershipPartViewModel
    {
        public ProfileGroupOwnershipPart Part { get; set; }
        public ContentTypePartDefinition PartDefinition { get; set; }
        public ProfileGroupOwnershipPartSettings Settings { get; set; }
        public bool RestrictAccess { get; set; }
    }
}
