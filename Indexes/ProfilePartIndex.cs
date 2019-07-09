using Etch.OrchardCore.UserProfiles.Models;
using OrchardCore.ContentManagement;
using YesSql.Indexes;

namespace Etch.OrchardCore.UserProfiles.Indexes
{
    public class ProfilePartIndex : MapIndex
    {
        public string UserIdentifier { get; set; }
    }

    public class ProfilePartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<ProfilePartIndex>()
                .Map(contentItem =>
                {
                    var profilePart = contentItem.As<ProfilePart>();
                    if (profilePart != null)
                    {
                        return new ProfilePartIndex
                        {
                            UserIdentifier = profilePart.UserIdentifier
                        };
                    }

                    return null;
                });
        }
    }
}
