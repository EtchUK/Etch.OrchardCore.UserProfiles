using System.ComponentModel.DataAnnotations;

namespace Etch.OrchardCore.UserProfiles.ViewModels
{
    public class ProfilePartViewModel
    {
        [Required]
        public string UserName { get; set; }
    }
}
