using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PostMessages
    {
        private List<KeyValuePair<string, string>> postList = new List<KeyValuePair<string, string>>();
        private KeyValuePair<string, string> theHeader;
        private string sendMessageUriBefore = "https://www.tabll.cn/enmolapi/";
        private Uri sendMessageUri;


        public PostMessages()
        {

        }

        public void SetUri(string uri)
        {
            sendMessageUri = new Uri(sendMessageUriBefore + uri);
        }

        public void AddPostInfomation(string key, string information)
        {
            postList.Add(new KeyValuePair<string, string>(key, information));
        }

        public void AddPostHeader(string key, string information)
        {
            theHeader = new KeyValuePair<string, string>(key, information);
        }
        
        public Task<string> SendMessages()
        {
            return DAL.HttpsConnect.HttpsConnector.SendOneMessageAsync(sendMessageUri,theHeader, postList);
        }
    }
}
