using Etch.OrchardCore.UserProfiles.Grouping.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        public IList<VueMultiselectItemViewModel> SelectedItems
        {
            get {
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

        [BindNever]
        public ProfileGroupPartSettings Settings { get; set; }
    }
}
