namespace Etch.OrchardCore.UserProfiles.GroupOwnership.Models
{
    public class ProfileGroupOwnershipPartSettings
    {
        public RestrictAccess RestrictAccess { get; set; }
    }

    public enum RestrictAccess
    {
        None = 0,
        ForType = 1,
        SetPerItem = 2
    }
}
