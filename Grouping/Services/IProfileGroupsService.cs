using System.Collections.Generic;
using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Subscriptions.Models;
using OrchardCore.ContentManagement;

namespace Etch.OrchardCore.UserProfiles.Grouping.Services
{
    public interface IProfileGroupsService
    {
        Task<ContentItem> AssignGroupAsync(ContentItem profile, string groupContentItemId);
        Task<IList<ContentItem>> GetAllGroupsAsync();
        Task<ContentItem> GetAsync(ContentItem contentItem);
        Task<SubscriptionLevelPart> GetSubscriptionAccessAsync(ContentItem contentItem);
    }
}
