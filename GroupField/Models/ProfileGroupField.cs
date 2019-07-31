using OrchardCore.ContentManagement;
using System.Collections.Generic;

namespace Etch.OrchardCore.UserProfiles.GroupField.Models
{
    public class ProfileGroupField : ContentField
    {
        public IList<string> ProfileGroupContentItemIds { get; set; }
        public string ProfileGroupNames { get; set; }
    }
}
