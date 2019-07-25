using Etch.OrchardCore.UserProfiles.GroupField.Models;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata.Models;
using System.Collections.Generic;

namespace Etch.OrchardCore.UserProfiles.GroupField.ViewModels
{
    public class DisplayProfileGroupFieldViewModel
    {
        public ProfileGroupField Field { get; set; }
        public ContentPart Part { get; set; }
        public ContentPartFieldDefinition PartFieldDefinition { get; set; }

        public IList<ProfileGroup> ProfileGroups { get; set; }
    }

    public class ProfileGroup
    {
        public string ContentItemId { get; set; }
        public string DisplayText { get; set; }
    }
}
