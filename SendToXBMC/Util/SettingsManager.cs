using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendToXBMC.Util
{
    class SettingsManager
    {
        private const String XBMC_HOST = "XBMCHost";
        private const String XBMC_PORT = "XBMCPort";
        private const String XBMC_USER = "XBMCUser";
        private const String XBMC_PASSWORD = "XBMCPassword";

        public static void saveXBMCSettings(String host, String port, String user, String password)
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values[XBMC_HOST] = host;
            Windows.Storage.ApplicationData.Current.LocalSettings.Values[XBMC_PORT] = port;
            Windows.Storage.ApplicationData.Current.LocalSettings.Values[XBMC_USER] = user;
            Windows.Storage.ApplicationData.Current.LocalSettings.Values[XBMC_PASSWORD] = password;
        }

        public static String XBMCHost()
        {
            return (String)Windows.Storage.ApplicationData.Current.LocalSettings.Values[XBMC_HOST];
        }

        public static String XBMCPort()
        {
            return (String)Windows.Storage.ApplicationData.Current.LocalSettings.Values[XBMC_PORT];
        }

        public static String XBMCUser()
        {
            return (String)Windows.Storage.ApplicationData.Current.LocalSettings.Values[XBMC_USER];
        }

        public static String XBMCPassword()
        {
            return (String)Windows.Storage.ApplicationData.Current.LocalSettings.Values[XBMC_PASSWORD];
        }

        public static Boolean validateSettings()
        {
            if (XBMCHost() == null || XBMCHost().Equals(String.Empty)) return false;
            if (XBMCPort() == null || XBMCPort().Equals(String.Empty)) return false;
            
            return true;
        }
    }
}
