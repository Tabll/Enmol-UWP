using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace BLL
{
    public class JsonTools
    {
        private JsonObject jsonObject;

        public JsonTools(string json)
        {
            try
            {
                jsonObject = JsonObject.Parse(json);
            }
            catch
            {
                jsonObject = null;
            }
        }
        public string GetJson(string key)
        {
            try
            {
                return jsonObject[key].GetString();
            }
            catch
            {
                return "服务器连接错误";
            }
            
        }
    }
}
