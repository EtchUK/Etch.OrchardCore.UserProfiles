using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.Modules;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Etch.OrchardCore.UserProfiles.Controllers
{
    [Feature(Constants.Features.Grouping)]
    public class ProfilePickerController : Controller
    {
        #region Dependencies

        private readonly IEnumerable<IContentPickerResultProvider> _resultProviders;

        #endregion

        #region Constructor

        public ProfilePickerController(IEnumerable<IContentPickerResultProvider> resultProviders)
        {
            _resultProviders = resultProviders;
        }

        #endregion

        public async Task<IActionResult> List(string query)
        {
            var resultProvider = _resultProviders.FirstOrDefault(p => p.Name == "ProfilePicker");

            if (resultProvider == null)
            {
                return new ObjectResult(new List<ContentPickerResult>());
            }

            var results = await resultProvider.Search(new ContentPickerSearchContext
            {
                Query = query,
                ContentTypes = new List<string> { Constants.ContentTypeName }
            });

            return new ObjectResult(results);
        }
    }
}
