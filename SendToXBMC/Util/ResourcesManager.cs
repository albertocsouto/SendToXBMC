using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace SendToXBMC.Util
{
    class ResourcesManager
    {
        static ResourceLoader resourceLoader = new ResourceLoader();
        
        public static string LocalizedString(string key)
        {
            return resourceLoader.GetString(key);
        }
    }
}
