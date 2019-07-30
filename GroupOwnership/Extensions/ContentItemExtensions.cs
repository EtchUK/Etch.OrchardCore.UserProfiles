using Etch.OrchardCore.UserProfiles.GroupField.Models;
using Etch.OrchardCore.UserProfiles.GroupOwnership.Models;
using OrchardCore.ContentManagement;
using System.Collections.Generic;

namespace Etch.OrchardCore.UserProfiles.GroupOwnership.Extensions
{
    public static class ContentItemExtensions
    {
        public static IList<string> GetProfileGroupOwnershipContentItemIds(this ContentItem contentItem)
        {
            if (contentItem == null)
            {
                return null;
            }
            var part = contentItem.As<ProfileGroupOwnershipPart>();
            if (part == null)
            {
                return null;
            }
            var field = part.Get<ProfileGroupField>(GroupOwnershipConstants.GroupFieldName);
            if (field == null)
            {
                return null;
            }
            return field.ProfileGroupContentItemIds;
        }
    }
}
