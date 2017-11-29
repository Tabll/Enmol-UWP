using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace DAL.HttpsConnect
{
    public class HttpsConnector
    {
        public static async Task<string> SendOneMessageAsync(Uri sendMessageUri, KeyValuePair<string, string> theHeader, List<KeyValuePair<string, string>> needPostMessages)
        {
            HttpClient httpClient = new HttpClient();//初始化Http请求
            var header = httpClient.DefaultRequestHeaders;//初始化请求头
            header.Add("key", "QFE1WEG3ER448984WEF7W4849WEF");
            header.Add(theHeader);
            string httpResponseBody = "";//初始化返回值
            if (!header.UserAgent.TryParseAdd("UWP"))
            {
                httpResponseBody = "error:3001";
                throw new Exception("ERROR: 3001");
            }
            HttpResponseMessage httpResponse = new HttpResponseMessage();//初始化返回值
            try
            {
                httpResponse = await httpClient.PostAsync(sendMessageUri, new HttpFormUrlEncodedContent(needPostMessages));
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                httpResponseBody = "error:3002";
                throw new Exception("ERROR: 3002");
            }
            return httpResponseBody;
        }
    }
}
