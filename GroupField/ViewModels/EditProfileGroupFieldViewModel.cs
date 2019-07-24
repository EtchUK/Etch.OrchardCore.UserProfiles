using Etch.OrchardCore.UserProfiles.GroupField.Models;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata.Models;
using System.Collections.Generic;

namespace Etch.OrchardCore.UserProfiles.GroupField.ViewModels
{
    public class EditProfileGroupFieldViewModel
    {
        public ProfileGroupField Field { get; set; }
        public ContentPart Part { get; set; }
        public ContentPartFieldDefinition PartFieldDefinition { get; set; }

        public string ProfileGroupContentItemIds { get; set; }
        public IList<ProfileGroupOption> PossibleProfileGroupOptions { get; set; }
    }

    public class ProfileGroupOption
    {
        public string ContentItemId { get; set; }
        public string DisplayText { get; set; }
    }
}
