using Etch.OrchardCore.UserProfiles.GroupField.Models;
using Etch.OrchardCore.UserProfiles.GroupOwnership.Models;
using OrchardCore.ContentManagement;
using System.Linq;
using YesSql.Indexes;

namespace Etch.OrchardCore.UserProfiles.GroupOwnership.Indexes
{
    public class GroupOwnershipIndex : MapIndex
    {
        public string GroupContentItemId { get; set; }
    }

    public class GroupOwnershipIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<GroupOwnershipIndex>()
                .Map(contentItem =>
                {
                    var profileGroupPart = contentItem.As<ProfileGroupOwnershipPart>();
                    if (profileGroupPart == null)
                    {
                        return null;
                    }

                    var field = profileGroupPart.Get<ProfileGroupField>(GroupOwnershipConstants.GroupFieldName);

                    if (field == null || field.ProfileGroupContentItemIds == null || field.ProfileGroupContentItemIds.Count < 1)
                    {
                        return null;
                    }

                    return field.ProfileGroupContentItemIds.Select(x => new GroupOwnershipIndex
                    {
                        GroupContentItemId = x
                    });
                });
        }
    }
}
