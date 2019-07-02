using Etch.OrchardCore.UserProfiles.Grouping.Models;

namespace Etch.OrchardCore.UserProfiles.Grouping.ViewModels
{
    public class ProfileGroupPartSettingsViewModel
    {
        public string Hint { get; set; }
        public string Label { get; set; }

        public ProfileGroupPartSettings ProfileGroupPartSettings { get; set; }
    }
}
