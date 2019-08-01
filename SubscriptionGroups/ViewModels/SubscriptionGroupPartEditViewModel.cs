using System.ComponentModel.DataAnnotations;

namespace Etch.OrchardCore.UserProfiles.SubscriptionGroups.ViewModels
{
    public class SubscriptionGroupPartEditViewModel
    {
        [Required(ErrorMessage = "Identifier is required")]
        public string Identifier { get; set; }
    }
}
