using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TobiiTest
{
    public class YandexTranslator : Translator
    {
        private static readonly string key = "trnsl.1.1.20181202T130022Z.e346b952c881526f.7cf20535988bdcee6ba84908bad44cd290d8bbc4";

        private static string host = "https://translate.yandex.net";
        private static string path = "/api/v1.5/tr.json/translate?key=" + key;

        //public static Dictionary<string, string> Languages
        //{
        //    get
        //    {
        //        if (_languages == null || _languages.Count == 0)
        //        {
        //            _languages = new Dictionary<string, string>
        //            {
        //                { "Afrikaans", "af" },
        //                { "Albanian", "sq" },
        //                { "Amharic", "am" },
        //                { "Arabic", "ar" },
        //                { "Armenian", "hy" },
        //                { "Azerbaijan", "az" },
        //                { "Bashkir", "ba" },
        //                { "Basque", "eu" },
        //                { "Belarusian", "be" },
        //                { "Bengali", "bn" },
        //                { "Bosnian", "bs" },
        //                { "Bulgarian", "bg" },
        //                { "Burmese", "my" },
        //                { "Catalan", "ca" },
        //                { "Cebuano", "ceb" },
        //                { "Chinese", "zh" },
        //                { "Croatian", "hr" },
        //                { "Czech", "cs" },
        //                { "Danish", "da" },
        //                { "Dutch", "nl" },
        //                { "English", "en" },
        //                { "Esperanto", "eo" },
        //                { "Estonian", "et" },
        //                { "Finnish", "fi" },
        //                { "French", "fr" },
        //                { "Galician", "gl" },
        //                { "Georgian", "ka" },
        //                { "German", "de" },
        //                { "Greek", "el" },
        //                { "Gujarati", "gu" },
        //                { "Haitian (Creole)", "ht" },
        //                { "Hebrew", "he" },
        //                { "Hill Mari", "mr" },
        //                { "Hindi", "hi" },
        //                { "Hungarian", "hu" },
        //                { "Icelandic", "is" },
        //                { "Indonesian", "id" },
        //                { "Irish", "ga" },
        //                { "Italian", "it" },
        //                { "Japanese",  "ja" },
        //                { "Javanese",  "jv" },
        //                { "Kannada", "kn" },
        //                { "Kazakh", "kk" },
        //                { "Khmer", "km" },
        //                { "Korean", "ko" },
        //                { "Kyrgyz", "ky" },
        //                { "Laotian", "lo" },
        //                { "Latin", "la" },
        //                { "Latvian", "lv" },
        //                { "Lithuanian", "lt" },
        //                { "Luxembourgish", "lb" },
        //                { "Macedonian", "mk" },
        //                { "Malagasy", "mg" },
        //                { "Malay", "ms" },
        //                { "Malayalam", "ml" },
        //                { "Maltese", "mt" },
        //                { "Maori", "mi" },
        //                { "Marathi", "mr" },
        //                { "Mari", "mhr" },
        //                { "Mongolian", "mn" },
        //                { "Nepali", "ne" },
        //                { "Norwegian", "no" },
        //                { "Papiamento", "pap" },
        //                { "Persian", "fa" },
        //                { "Polish", "pl" },
        //                { "Portuguese", "pt" },
        //                { "Romanian", "ro" },
        //                { "Russian", "ru" },
        //                { "Punjabi", "pa" },
        //                { "Scottish", "gd" },
        //                { "Serbian", "sr" },
        //                { "Sinhala", "si" },
        //                { "Slovakian", "sk" },
        //                { "Slovenian", "sl" },
        //                { "Spanish", "es" },
        //                { "Sundanese", "su" },
        //                { "Swahili", "sw" },
        //                { "Swedish", "sv" },
        //                { "Tagalog", "tl" },
        //                { "Tajik", "tg" },
        //                { "Tamil", "ta" },
        //                { "Tatar", "tt" },
        //                { "Telugu", "te" },
        //                { "Thai", "th" },
        //                { "Turkish", "tr" },
        //                { "Udmurt", "udm" },
        //                { "Ukrainian", "uk" },
        //                { "Urdu", "ur" },
        //                { "Uzbek", "uz" },
        //                { "Vietnamese", "vi" },
        //                { "Welsh", "cy" },
        //                { "Xhosa", "xh" },
        //                { "Yiddish", "yi" },
        //            };
        //        }
        //        return _languages;
        //    }
        //}

        public YandexTranslator(string srclang, string targlang)
        {
            SourceLanguage = srclang;
            TargetLanguage = targlang;
        }

        public override string Translate(string text)
        {
            //System.Object[] body = new System.Object[] { new { text = Uri.EscapeUriString(text) } };
            //var requestBody = JsonConvert.SerializeObject(body);
            var langs = Translator.Languages("Yandex");
            langs.TryGetValue(SourceLanguage, out string from);
            langs.TryGetValue(TargetLanguage, out string to);
            string lang = from + "-" + to;
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(host + path + "&lang=" + lang);
                request.Content = new StringContent("text=" + Uri.EscapeUriString(text), Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = client.SendAsync(request).Result;
                var responseBody = response.Content.ReadAsStringAsync().Result;

                Console.WriteLine("YT: got response: " + responseBody);
                var jobj = JObject.Parse(responseBody);
                if (jobj["code"].ToObject<int>() != 200)
                {
                    throw new Exception("Yandex API Error.\nCode: " + jobj["code"] + "\nMessage: " + jobj["message"]);
                    //Console.WriteLine("YT: error in responce, code: " + jobj["code"].ToObject<int>());
                    //Console.WriteLine("YT: error in responce, message: " + jobj["message"].ToString());
                    //return "";
                }
                else
                {
                    return jobj["text"][0].ToString();
                }
            }
        }
    }
}
