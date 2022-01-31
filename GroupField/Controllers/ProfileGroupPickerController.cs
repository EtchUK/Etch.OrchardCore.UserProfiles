using Etch.OrchardCore.UserProfiles.Grouping.Models;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentFields.ViewModels;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Etch.OrchardCore.UserProfiles.GroupField.Controllers
{
    [Feature(Constants.Features.GroupField)]
    public class ProfileGroupPickerController : Controller
    {
        #region Dependencies

        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IEnumerable<IContentPickerResultProvider> _resultProviders;

        #endregion Dependencies

        #region Constructor

        public ProfileGroupPickerController(
            IContentDefinitionManager contentDefinitionManager,
            IEnumerable<IContentPickerResultProvider> resultProviders
            )
        {
            _contentDefinitionManager = contentDefinitionManager;
            _resultProviders = resultProviders;
        }

        #endregion Constructor

        #region Actions

        #region List

        public async Task<IActionResult> List(string query)
        {
            var resultProvider = _resultProviders.FirstOrDefault(p => p.Name == "ProfileGroupPicker");

            if (resultProvider == null)
            {
                return new ObjectResult(new List<ContentPickerResult>());
            }

            var results = await resultProvider.Search(new ContentPickerSearchContext
            {
                Query = query,
                ContentTypes = GetProfileGroupTypes()
            });

            return new ObjectResult(results.Select(r => new VueMultiselectItemViewModel() { Id = r.ContentItemId, DisplayText = r.DisplayText, HasPublished = r.HasPublished }));
        }

        #endregion List

        #endregion Actions

        #region Private Methods

        private IEnumerable<string> GetProfileGroupTypes()
        {
            return _contentDefinitionManager
                .ListTypeDefinitions()
                .Where(t =>
                    t.Parts.Any(p =>
                        nameof(ProfileGroupPart).Equals(p.Name, StringComparison.OrdinalIgnoreCase)
                    )
                ).Select(t => t.Name)
                .ToArray();
        }

        #endregion Private Methods
    }
}
