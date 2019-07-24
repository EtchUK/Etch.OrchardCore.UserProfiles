using Etch.OrchardCore.UserProfiles.GroupField.Models;
using Etch.OrchardCore.UserProfiles.GroupField.ViewModels;
using Etch.OrchardCore.UserProfiles.Grouping.Services;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Etch.OrchardCore.UserProfiles.GroupField.Drivers
{
    public class ProfileGroupFieldDisplayDriver : ContentFieldDisplayDriver<ProfileGroupField>
    {
        #region Dependencies

        private readonly IProfileGroupsService _profileGroupsService;

        #endregion Dependencies

        #region Constructor

        public ProfileGroupFieldDisplayDriver(IProfileGroupsService profileGroupsService)
        {
            _profileGroupsService = profileGroupsService;
        }

        #endregion Constructor

        #region ContentFieldDisplayDriver

        #region Edit

        public override IDisplayResult Edit(ProfileGroupField field, BuildFieldEditorContext context)
        {
            var groups = _profileGroupsService.GetAllGroupsAsync().Result;

            return Initialize<EditProfileGroupFieldViewModel>(GetEditorShapeType(context), model =>
            {
                model.Field = field;
                model.Part = context.ContentPart;
                model.PartFieldDefinition = context.PartFieldDefinition;

                model.ProfileGroupContentItemIds = JoinIds(field.ProfileGroupContentItemIds);
                model.PossibleProfileGroupOptions = groups.Select(g => new ProfileGroupOption
                {
                    ContentItemId = g.ContentItemId,
                    DisplayText = g.DisplayText
                })
                .OrderBy(x => x.DisplayText)
                .ToList();
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(ProfileGroupField field, IUpdateModel updater, UpdateFieldEditorContext context)
        {
            var model = new EditProfileGroupFieldViewModel();

            if (await updater.TryUpdateModelAsync(model, Prefix, m => m.ProfileGroupContentItemIds))
            {
                field.ProfileGroupContentItemIds = SplitIds(model.ProfileGroupContentItemIds);
            }

            return Edit(field, context);
        }

        #endregion Edit

        #endregion ContentFieldDisplayDriver

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
            if (string.IsNullOrWhiteSpace(ids))
            {
                return new List<string>();
            }
            return ids
                .Split(',')
                .Select(x => x.Trim())
                .ToList();
        }

        #endregion Private
    }
}
