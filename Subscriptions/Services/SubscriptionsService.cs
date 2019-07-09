using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Users;
using YesSql;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.Services
{
    public class SubscriptionsService : ISubscriptionsService
    {
        #region Dependencies

        private readonly ISession _session;

        #endregion

        #region Constructor

        public SubscriptionsService(ISession session)
        {
            _session = session;
        }

        #endregion

        #region Implementation

        public async Task<List<SubscriptionPart>> GetAllAsync()
        {
            var contentItems = await _session.Query<ContentItem>()
                                      .With<ContentItemIndex>(x => x.Published && x.Latest && x.ContentType == Constants.ContentSubscriptionTypeName)
                                      .ListAsync();

            var results = new List<SubscriptionPart>();

            foreach (var contentItem in contentItems) {
                results.Add(new SubscriptionPart
                {
                    ContentItem = contentItem,
                    Identifier = contentItem.As<SubscriptionPart>().Identifier,
                });
            }

            return results.OrderBy(x => x.ContentItem.DisplayText).ToList();
        }

        #endregion
    }
}
