using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LocalSettings
    {
        public void SaveLocalSettings(string key, string value)
        {
            DAL.LocalSettings localSettings = new DAL.LocalSettings();
            localSettings.SetLocalSettings(key, value);
        }

        public string GetLocalSettings(string key)
        {
            DAL.LocalSettings localSettings = new DAL.LocalSettings();
            return localSettings.GetLocalSettings(key);
        }

        public bool IsSetLocalSettings(string key)
        {
            DAL.LocalSettings localSettings = new DAL.LocalSettings();
            return localSettings.IsSetLocalSettings(key);
        }

        public void RemoveLocalSettings(string key)
        {
            DAL.LocalSettings localSettings = new DAL.LocalSettings();
            localSettings.ClearLocalSettings(key);
        }
    }
}