using System.ComponentModel.DataAnnotations;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.ViewModels
{
    public class SubscriptionPartEditViewModel
    {
        [Required(ErrorMessage = "Identifier is required")]
        public string Identifier { get; set; }
    }
}
