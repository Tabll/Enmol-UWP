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
            if (localSettings.Values.ContainsKey(key))
            {
                return localSettings.Values[key].ToString();
            }
            else
            {
                return "NULL";
            }
        }

        public void SetLocalSettings(string key, string value)
        {
            localSettings.Values[key] = value;
        }

        public void ClearLocalSettings(string key)
        {
            if (localSettings.Values.ContainsKey(key))
            {
                localSettings.Values.Remove(key);
            }
        }

        public bool IsSetLocalSettings(string key)
        {
            return localSettings.Values.ContainsKey(key);
        }
    }
}
