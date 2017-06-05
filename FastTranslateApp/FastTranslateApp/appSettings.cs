using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTranslateApp
{
    class appSettings
    {
        public appSettings()
        {
        }

        public static string apiBaseDetect = "https://translate.yandex.net/api/v1.5/tr.json/detect?key=";
        public static string apiBaseAllLang = "https://translate.yandex.net/api/v1.5/tr.json/getLangs?key=";
        public static string apiBaseTranslate = "https://translate.yandex.net/api/v1.5/tr.json/translate?key=";

        public static string getApikey()
        {
            return "trnsl.1.1.20170524T102337Z.54954085e39b55f2.1c6b67eb938adad2946929792a2ff5f87f189c28";
        }
    }
}
