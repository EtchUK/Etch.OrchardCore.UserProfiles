using Etch.OrchardCore.UserProfiles.Grouping.Services;
using Etch.OrchardCore.UserProfiles.GroupOwnership.Models;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using System.Collections.Generic;
using System.Linq;

namespace Etch.OrchardCore.UserProfiles.GroupOwnership.Drivers
{
    public class ProfileGroupOwnershipPartDisplay : ContentPartDisplayDriver<ProfileGroupOwnershipPart>
    {
        #region Dependencies

        private readonly IProfileGroupsService _profileGroupsService;

        #endregion Dependencies

        #region Constructor

        public ProfileGroupOwnershipPartDisplay(IProfileGroupsService profileGroupsService)
        {
            _profileGroupsService = profileGroupsService;
        }

        #endregion Constructor

        #region ContentPartDisplayDriver

        #region Display



        #endregion Display

        #endregion ContentPartDisplayDriver

        #region Private

        private string JoinIds(IList<string> ids)
        {
            if (ids == null)
            {
                return string.Empty;
            }
            return string.Join(",", ids);
        }

        private IList<string> SplitIds(string ids)
        {
            if (ids == null)
            {
                return new List<string>();
            }
            return ids.Split(',')
                .Select(x => x.Trim())
                .ToList();
        }

        #endregion Private
    }
}
