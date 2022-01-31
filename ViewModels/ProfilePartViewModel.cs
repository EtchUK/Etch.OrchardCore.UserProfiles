using System.ComponentModel.DataAnnotations;

namespace Etch.OrchardCore.UserProfiles.ViewModels
{
    public class ProfilePartViewModel
    {
        [Required]
        public string UserName { get; set; }

        public string UserId { get; set; }

        public string SiteURL { get; set; }
    }
}
