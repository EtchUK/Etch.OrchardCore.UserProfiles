using OrchardCore.ContentFields.ViewModels;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata.Models;
using System.Collections.Generic;
using System.Linq;

namespace Etch.OrchardCore.UserProfiles.Grouping.ViewModels
{
    public class ProfileGroupPartViewModel
    {
        public IEnumerable<ContentItem> Items { get; set; }

        public ContentTypePartDefinition PartDefinition { get; set; }

        public string ContentItemIds { get; set; }

        public IList<ContentPickerItemViewModel> SelectedItems
        {
            get {
                if (Items == null)
                {
                    return new List<ContentPickerItemViewModel>();
                }

                return Items.Select(x => new ContentPickerItemViewModel
                {
                    ContentItemId = x.ContentItemId,
                    DisplayText = x.DisplayText,
                    HasPublished = true
                }).ToList();
            }
        }
    }
}
