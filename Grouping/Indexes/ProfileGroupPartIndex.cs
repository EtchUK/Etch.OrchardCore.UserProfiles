using Etch.OrchardCore.UserProfiles.Grouping.Models;
using OrchardCore.ContentManagement;
using YesSql.Indexes;

namespace Etch.OrchardCore.UserProfiles.Grouping.Indexes
{
    public class ProfileGroupPartIndex : MapIndex
    {

    }

    public class ProfileGroupPartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<ProfileGroupPartIndex>()
                .Map(contentItem =>
                {
                    var profileGroupPart = contentItem.As<ProfileGroupPart>();
                    if (profileGroupPart != null)
                    {
                        return new ProfileGroupPartIndex
                        {

                        };
                    }

                    return null;
                });
        }
    }
}
