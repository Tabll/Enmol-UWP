using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class HttpsConnector
    {
        private string SITE = "https://enmol.tabll.cn";

        private string userName;
        private string password;
        private string token;
        
        private List<KeyValuePair<string, string>> postValue = new List<KeyValuePair<string, string>>
        {
            //new KeyValuePair<string,string>("CLIENT", "UWP"),
            //new KeyValuePair<string,string>("VERSION", BLL.Tools.GetVersion())
        };

        HttpsConnector(string userName, string password)
        {
            postValue.Add(new KeyValuePair<string, string>("Operator-Name", "123"));

        }

        HttpsConnector()
        {
            postValue.Add(new KeyValuePair<string, string>("CLIENT", "UWP"));
            postValue.Add(new KeyValuePair<string, string>("VERSION", BLL.Tools.GetVersion()));
        }
    }
}
