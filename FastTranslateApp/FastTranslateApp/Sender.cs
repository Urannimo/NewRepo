using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FastTranslateApp
{
    class Sender
    {
        public static string sendRequest(HttpWebRequest req)
        {
            req.Method = "GET";

            IWebProxy proxy = WebRequest.GetSystemWebProxy();
            proxy.Credentials = new NetworkCredential("grinkoym", "gfm015ze0");
            req.Proxy = proxy;
            req.PreAuthenticate = true;

            var response = (HttpWebResponse)req.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }
    }
}
