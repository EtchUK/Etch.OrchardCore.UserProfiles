using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesSql;
using YesSql.Services;

namespace Etch.OrchardCore.UserProfiles.GroupField.Services
{
    public class ProfileGroupPickerResultProvider : IContentPickerResultProvider
    {
        private readonly IContentManager _contentManager;
        private readonly ISession _session;

        public ProfileGroupPickerResultProvider(IContentManager contentManager, ISession session)
        {
            _contentManager = contentManager;
            _session = session;
        }

        public string Name => "ProfileGroupPicker";

        public async Task<IEnumerable<ContentPickerResult>> Search(ContentPickerSearchContext searchContext)
        {
            var query = _session.Query<ContentItem, ContentItemIndex>()
                .With<ContentItemIndex>(x => x.ContentType.IsIn(searchContext.ContentTypes) && (x.Published || x.Latest));

            if (!string.IsNullOrEmpty(searchContext.Query))
            {
                query.With<ContentItemIndex>(x => x.DisplayText.Contains(searchContext.Query));
            }

            var contentItems = await query.Take(20).ListAsync();

            var results = new List<ContentPickerResult>();

            foreach (var contentItem in contentItems)
            {
                results.Add(new ContentPickerResult
                {
                    ContentItemId = contentItem.ContentItemId,
                    DisplayText = contentItem.ToString(),
                    HasPublished = await _contentManager.HasPublishedVersionAsync(contentItem)
                });
            }

            return results.OrderBy(x => x.DisplayText);
        }
    }
}
