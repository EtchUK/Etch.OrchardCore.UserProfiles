using OrchardCore.Environment.Shell;

namespace Etch.OrchardCore.UserProfiles.Services
{
    public class URLService : IURLService
    {
        #region Dependencies

        private readonly ShellSettings _shellSettings;

        #endregion

        #region Constructor

        public URLService(ShellSettings shellSettings)
        {
            _shellSettings = shellSettings;
        }

        #endregion

        #region Implementation

        public string GetTenantUrl()
        {
            return "/" + _shellSettings.RequestUrlPrefix;
        }

        #endregion
    }
}
