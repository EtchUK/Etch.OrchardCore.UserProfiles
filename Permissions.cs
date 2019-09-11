using OrchardCore.Security.Permissions;
using System.Collections.Generic;

namespace Etch.OrchardCore.UserProfiles.Profile
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageProfileMigration = new Permission("ManageProfileMigration", "Manage Profile Migration");

        public IEnumerable<Permission> GetPermissions()
        {
            return new[] { ManageProfileMigration };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new[] { ManageProfileMigration }
                }
            };
        }
    }
}