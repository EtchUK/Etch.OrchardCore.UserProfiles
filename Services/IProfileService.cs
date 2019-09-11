using OrchardCore.ContentManagement;
using OrchardCore.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Etch.OrchardCore.UserProfiles.Services
{
    public interface IProfileService
    {
        Task<ContentItem> CreateAsync(IUser user);
        Task<ContentItem> GetAsync(IUser user);
        Task<List<ContentItem>> GetAllAsync();
        Task<IList<ContentItem>> GetAllByGroupAsync(ContentItem contentItem);
    }
}
