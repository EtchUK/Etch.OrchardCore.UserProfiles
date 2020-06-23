using Etch.OrchardCore.UserProfiles.GroupField.Models;
using Etch.OrchardCore.UserProfiles.Grouping.Services;
using Etch.OrchardCore.UserProfiles.GroupOwnership.Models;
using Etch.OrchardCore.UserProfiles.GroupOwnership.Services;
using Etch.OrchardCore.UserProfiles.GroupOwnership.ViewModels;
using Etch.OrchardCore.UserProfiles.Services;
using Microsoft.AspNetCore.Http;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using System.Threading.Tasks;

namespace Etch.OrchardCore.UserProfiles.GroupOwnership.Drivers
{
    public class ProfileGroupOwnershipPartDisplay : ContentPartDisplayDriver<ProfileGroupOwnershipPart>
    {
        #region Dependencies

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOwnershipAuthorizationService _ownershipAuthorizationService;
        private readonly IProfileGroupsService _profileGroupsService;
        private readonly IURLService _urlService;

        #endregion Dependencies

        #region Constructor

        public ProfileGroupOwnershipPartDisplay(IHttpContextAccessor httpContextAccessor,
            IOwnershipAuthorizationService ownershipAuthorizationService,
            IProfileGroupsService profileGroupsService,
            IURLService urlService)
        {
            _httpContextAccessor = httpContextAccessor;
            _ownershipAuthorizationService = ownershipAuthorizationService;
            _profileGroupsService = profileGroupsService;
            _urlService = urlService;
        }

        #endregion Constructor

        #region ContentPartDisplayDriver

        #region Display

        public override IDisplayResult Display(ProfileGroupOwnershipPart part, BuildPartDisplayContext context)
        {
            // If the request is not a detail page then we allow users to view the content
            if (context.DisplayType != "Detail")
            {
                return null;
            }

            // If the settings for the part don't restrict access allow viewing the content
            if (!ShouldRestrictAccess(part, context))
            {
                return null;
            }

            var ids = part.Get<ProfileGroupField>(GroupOwnershipConstants.GroupFieldName)?.ProfileGroupContentItemIds;

            // If the user has the required profile group allow viewing
            if (_ownershipAuthorizationService.CanViewContentAsync(_httpContextAccessor.HttpContext.User, ids).Result)
            {
                return null;
            }

            // Otherwise 401 and redirect
            _httpContextAccessor.HttpContext.Response.StatusCode = 401;
            _httpContextAccessor.HttpContext.Response.Redirect(_urlService.GetTenantUrl(), false);

            return null;
        }

        #endregion Display

        #region Edit

        public override IDisplayResult Edit(ProfileGroupOwnershipPart part, BuildPartEditorContext context)
        {
            return Initialize<EditProfileGroupOwnershipPartViewModel>(GetEditorShapeType(context), model =>
            {
                model.Part = part;
                model.PartDefinition = context.TypePartDefinition;
                model.Settings = context.TypePartDefinition.Settings.ToObject<ProfileGroupOwnershipPartSettings>();

                model.RestrictAccess = part.RestrictAccess;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(ProfileGroupOwnershipPart part, IUpdateModel updater, UpdatePartEditorContext context)
        {
            var model = new EditProfileGroupOwnershipPartViewModel();

            if (await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.RestrictAccess))
            {
                part.RestrictAccess = model.RestrictAccess;
            }

            return Edit(part, context);
        }

        #endregion Edit

        #endregion ContentPartDisplayDriver

        #region Private

        private bool ShouldRestrictAccess(ProfileGroupOwnershipPart part, BuildPartDisplayContext context)
        {
            var settings = context.TypePartDefinition.GetSettings<ProfileGroupOwnershipPartSettings>();

            if (settings == null)
            {
                return false;
            }

            if (settings.RestrictAccess == RestrictAccess.None)
            {
                return false;
            }

            if (settings.RestrictAccess == RestrictAccess.ForType)
            {
                return true;
            }

            if (settings.RestrictAccess == RestrictAccess.SetPerItem)
            {
                return part.RestrictAccess;
            }

            return false;
        }

        #endregion Private
    }
}
