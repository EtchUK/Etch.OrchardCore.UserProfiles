using OrchardCore.Security.Permissions;
using System.Collections.Generic;

namespace UKIE.OrchardCore.UserProfiles.SubscriptionAccessGrouping {
    public class Permissions : IPermissionProvider {
        public static readonly Permission ManageSubscription = new Permission("Subscription", "Manage subscription settings");

        public IEnumerable<Permission> GetPermissions() {
            return new[] { ManageSubscription };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes() {
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