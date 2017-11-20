using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DAL
{
    public class LocalSettings
    {
        private static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;//设置容器

        public string GetLocalSettings(string key){
            return "";
        }
    }
}
