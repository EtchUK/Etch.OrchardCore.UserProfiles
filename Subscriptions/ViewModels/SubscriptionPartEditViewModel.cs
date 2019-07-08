using System.ComponentModel.DataAnnotations;

namespace Etch.OrchardCore.UserProfiles.Subscriptions.ViewModels
{
    public class SubscriptionPartEditViewModel
    {
        [Required(ErrorMessage = "The From Identifier field is required")]
        public string Identifier { get; set; }
    }
}
