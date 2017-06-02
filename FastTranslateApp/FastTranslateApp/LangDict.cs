using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace FastTranslateApp
{
    class LangDict
    {
        public Dictionary<string, string> langList;

        public LangDict()
        {
            getAllLanguages();
        }

        private void getAllLanguages()
        {
            HttpWebRequest newReq = WebRequest.CreateHttp(appSettings.apiBaseAllLang + appSettings.getApikey() + "&ui=ru");

            var buf = Sender.sendRequest(newReq);
            buf = buf.Substring(buf.IndexOf("langs") + 7); //убираем первую часть ответа            
            buf = buf.Remove(buf.LastIndexOf(buf.Last())); //убираем последний символ }

            langList = JsonConvert.DeserializeObject<Dictionary<string, string>>(buf);
        }

        public Dictionary<string, string> GetLangChoices()
        {
            return langList;
        }

        public string getLanguageByCode(string code)
        {
            string result;
            langList.TryGetValue(code, out result);
            var vv = langList.Fi;
            return result;
        }
    }
}
