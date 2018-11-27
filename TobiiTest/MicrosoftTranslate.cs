using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
// NOTE: Install the Newtonsoft.Json NuGet package.
using Newtonsoft.Json;

namespace TobiiTest
{
    class MicrosoftTranslator
    {
        private static string host = "https://api.cognitive.microsofttranslator.com";
        private static string path = "/translate?api-version=3.0";

        private string GetURI()
        {
            Languages.TryGetValue(SourceLanguage, out string from);
            Languages.TryGetValue(TargetLanguage, out string to);
            return host + path + "&from=" + from + "&to=" + to;
        }

        private static string key = Encoding.UTF8.GetString(System.Convert.FromBase64String("MGRjYjE1NWU0NjA5NGEzYThiZTBiYTgxNzEwZTRmMDY="));

        // Language names like in Dictionary below
        public string SourceLanguage
        {
            get;
            set;
        }

        public string TargetLanguage
        {
            get;
            set;
        }

        public static Dictionary<string,string> Languages
        {
            get
            {
                if (_languages == null || _languages.Count == 0)
                {
                    _languages = new Dictionary<string, string>
                    {
                        { "Afrikaans", "af" },
                        { "Arabic", "ar" },
                        { "Arabic, Levantine", "apc" },
                        { "Bangla", "bn" },
                        { "Bosnian (Latin)", "bs" },
                        { "Bulgarian", "bg" },
                        { "Cantonese (Traditional)", "yue" },
                        { "Catalan", "ca" },
                        { "Chinese Simplified", "zh-Hans" },
                        { "Chinese Traditional", "zh-Hant" },
                        { "Croatian", "hr" },
                        { "Czech", "cs" },
                        { "Danish", "da" },
                        { "Dutch", "nl" },
                        { "English", "en" },
                        { "Estonian", "et" },
                        { "Fijian", "fj" },
                        { "Filipino", "fil" },
                        { "Finnish", "fi" },
                        { "French", "fr" },
                        { "German", "de" },
                        { "Greek", "el" },
                        { "Haitian Creole", "ht" },
                        { "Hebrew", "he" },
                        { "Hindi", "hi" },
                        { "Hmong Daw", "mww" },
                        { "Hungarian", "hu" },
                        { "Icelandic", "is" },
                        { "Indonesian", "id" },
                        { "Italian", "it" },
                        { "Japanese", "ja" },
                        { "Kiswahili", "sw" },
                        { "Klingon", "tlh" },
                        { "Klingon (plqaD)", "tlh-Qaak" },
                        { "Korean", "ko" },
                        { "Latvian", "lv" },
                        { "Lithuanian", "lt" },
                        { "Malagasy", "mg" },
                        { "Malay", "ms" },
                        { "Maltese", "mt" },
                        { "Norwegian", "nb" },
                        { "Persian", "fa" },
                        { "Polish", "pl" },
                        { "Portuguese", "pt" },
                        { "Queretaro Otomi", "otq" },
                        { "Romanian", "ro" },
                        { "Russian", "ru" },
                        { "Samoan", "sm" },
                        { "Serbian (Cyrillic)", "sr-Cyrl" },
                        { "Serbian (Latin)", "sr-Latn" },
                        { "Slovak", "sk" },
                        { "Slovenian", "sl" },
                        { "Spanish", "es" },
                        { "Swedish", "sv" },
                        { "Tahitian", "ty" },
                        { "Tamil", "ta" },
                        { "Telugu", "te" },
                        { "Thai", "th" },
                        { "Tongan", "to" },
                        { "Turkish", "tr" },
                        { "Ukrainian", "uk" },
                        { "Urdu", "ur" },
                        { "Vietnamese", "vi" },
                        { "Welsh", "cy" },
                        { "Yucatec Maya", "yua" }
                    };
                }
                return _languages;
            }
        }

        public MicrosoftTranslator(string srclang, string targlang)
        {
            SourceLanguage = srclang;
            TargetLanguage = targlang;
        }

        // async version header saved, just in case
        // async static string Translate(string text)
        string Translate(string text)
        {
            System.Object[] body = new System.Object[] { new { Text = text } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(GetURI());
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);

                var response = client.SendAsync(request).Result;
                var responseBody = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(responseBody), Formatting.Indented);

                //Console.OutputEncoding = UnicodeEncoding.UTF8;
                //Console.WriteLine(result);
                return result;
            }
        }

        /*
        static void Main(string[] args)
        {
            Translate();
            Console.ReadLine();
        }
        */

        private static Dictionary<string, string> _languages;
    }
}
