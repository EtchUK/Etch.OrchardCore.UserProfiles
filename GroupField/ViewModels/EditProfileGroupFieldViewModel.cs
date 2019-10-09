using Etch.OrchardCore.UserProfiles.GroupField.Models;
using OrchardCore.ContentFields.ViewModels;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata.Models;
using System.Collections.Generic;
using System.Linq;

namespace Etch.OrchardCore.UserProfiles.GroupField.ViewModels
{
    public class EditProfileGroupFieldViewModel
    {
        public IList<ContentItem> Items { get; set; }
        public ProfileGroupField Field { get; set; }
        public ContentPart Part { get; set; }
        public ContentPartFieldDefinition PartFieldDefinition { get; set; }

        public string ProfileGroupContentItemIds { get; set; }
        public IList<VueMultiselectItemViewModel> SelectedItems
        {
            get
            {
                if (Items == null)
                {
                    return new List<VueMultiselectItemViewModel>();
                }

                return Items.Select(x => new VueMultiselectItemViewModel
                {
                    Id = x.ContentItemId,
                    DisplayText = x.DisplayText,
                    HasPublished = true
                }).ToList();
            }
        }
    }
}
