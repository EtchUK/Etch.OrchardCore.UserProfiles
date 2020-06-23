using System;
using System.Threading.Tasks;
using Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping.Drivers;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;

namespace Etch.OrchardCore.UserProfiles.SubscriptionAccessGrouping
{
    public class AdminMenu : INavigationProvider 
    {
        public AdminMenu(IStringLocalizer<AdminMenu> localizer) 
        {
            T = localizer;
        }

        public IStringLocalizer T { get; set; }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder) {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase)) {
                return Task.CompletedTask;
            }

            builder
                .Add(T["Configuration"], configuration => configuration
                    .Add(T["Subscriptions"], settings => settings
                        .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = SubscriptionAccessSettingsDisplay.GroupId })
                        .Permission(Permissions.ManageSubscription)
                        .LocalNav()
                    ));

            return Task.CompletedTask;
        }
    }
}
