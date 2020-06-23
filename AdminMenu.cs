using Microsoft.Extensions.Localization;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using System;
using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.Profile;

namespace Etch.OrchardCore.UserProfiles
{
    [Feature(Constants.Features.Core)]
    public class AdminMenu : INavigationProvider
    {
        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            T = localizer;
        }

        public IStringLocalizer T { get; set; }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            builder
                .Add(T["Configuration"], configuration => configuration
                .Add(T["Security"], "5", security => security
                .Add(T["User/Profile Migration"], "11", installed => installed
                    .Action("Index", "Admin", "Etch.OrchardCore.UserProfiles")
                    .Permission(Permissions.ManageProfileMigration)
                    .LocalNav()
            )));

            return Task.CompletedTask;
        }
    }
}
