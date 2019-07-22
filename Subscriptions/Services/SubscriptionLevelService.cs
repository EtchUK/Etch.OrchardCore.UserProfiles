using System;
using System.Linq;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using Etch.OrchardCore.UserProfiles.Subscriptions.Settings;
using OrchardCore.ContentManagement.Metadata;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.Services
{
    public class SubscriptionLevelService : ISubscriptionLevelService
    {

        #region Dependencies

        private readonly IContentDefinitionManager _contentDefinitionManager;

        #endregion

        #region Constructor

        public SubscriptionLevelService(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        #endregion

        #region Implementations

        public SubscriptionLevelPartSettings GetSettings(SubscriptionLevelPart subscriptionLevelPart)
        {
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(subscriptionLevelPart.ContentItem.ContentType);
            var contentTypePartDefinition = contentTypeDefinition.Parts.FirstOrDefault(x => string.Equals(x.PartDefinition.Name, nameof(SubscriptionLevelPart), StringComparison.Ordinal));
            return contentTypePartDefinition.Settings.ToObject<SubscriptionLevelPartSettings>();
        }

        #endregion
    }

    public interface ISubscriptionLevelService
    {
        SubscriptionLevelPartSettings GetSettings(SubscriptionLevelPart subscriptionLevelPart);
    }
}
