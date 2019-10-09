using OrchardCore.Security.Permissions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UKIE.OrchardCore.UserProfiles.SubscriptionAccessGrouping
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageSubscription = new Permission("Subscription", "Manage subscription settings");

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[] { ManageSubscription }.AsEnumerable());
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new[] { ManageSubscription }
                }
            };
        }
    }
}