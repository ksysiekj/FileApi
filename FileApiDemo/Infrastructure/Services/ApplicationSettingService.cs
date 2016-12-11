using System.Collections.Specialized;
using System.Configuration;

namespace Infrastructure.Services
{
    public sealed class ApplicationSettingService : IApplicationSettingService
    {
        private static readonly NameValueCollection _appSettings;

        static ApplicationSettingService()
        {
            _appSettings = ConfigurationManager.AppSettings;
        }

        public string this[string appKey]
        {
            get { return _appSettings[appKey]; }
        }
    }
}