using OrchardCore.Security.Permissions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Etch.OrchardCore.UserProfiles.Profile
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageProfileMigration = new Permission("ManageProfileMigration", "Manage Profile Migration");

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[] { ManageProfileMigration }.AsEnumerable());
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