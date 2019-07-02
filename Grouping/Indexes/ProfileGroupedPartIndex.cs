using Etch.OrchardCore.UserProfiles.Grouping.Models;
using OrchardCore.ContentManagement;
using YesSql.Indexes;

namespace Etch.OrchardCore.UserProfiles.Grouping.Indexes
{
    public class ProfileGroupedPartIndex : MapIndex
    {
        public string GroupContentItemId { get; set; }
    }

    public class ProfileGroupedPartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<ProfileGroupedPartIndex>()
                .Map(contentItem =>
                {
                    var profileGroupedPart = contentItem.As<ProfileGroupedPart>();
                    if (profileGroupedPart != null)
                    {
                        return new ProfileGroupedPartIndex
                        {
                            GroupContentItemId = profileGroupedPart.GroupContentItemId
                        };
                    }

                    return null;
                });
        }
    }
}
