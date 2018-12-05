using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
// NOTE: Install the Newtonsoft.Json NuGet package.
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TobiiTest
{
    public class MicrosoftTranslator : Translator
    {
        private static string host = "https://api.cognitive.microsofttranslator.com";
        private static string path = "/translate?api-version=3.0";

        private string GetURI()
        {
            Languages.TryGetValue(SourceLanguage, out string from);
            Languages.TryGetValue(TargetLanguage, out string to);
            return host + path + "&from=" + from + "&to=" + to;
        }

        private static readonly string key = Encoding.UTF8.GetString(System.Convert.FromBase64String("MGRjYjE1NWU0NjA5NGEzYThiZTBiYTgxNzEwZTRmMDY="));

        // Language names like in Dictionary below
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
        public override string Translate(string text)
        {
            Console.WriteLine("MST: got text: " + text);
            System.Object[] body = new System.Object[] { new { Text = text } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                Console.WriteLine("MST: URI: " + GetURI());
                request.RequestUri = new Uri(GetURI());
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                Console.WriteLine("MST: key: " + key);
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);

                Console.WriteLine("MST: request: " + request);

                var response = client.SendAsync(request).Result;
                var responseBody = response.Content.ReadAsStringAsync().Result;

                Console.WriteLine("MST: responce: " + responseBody);
                
                var jt = JToken.Parse(responseBody);
                if (jt is JArray)
                {
                    return jt[0]["translations"][0]["text"].ToString();
                }
                else //if (jt is JObject)
                {
                    throw new Exception("MS API Error.\nCode: " + jt["error"]["code"] + "\nMessage: " + jt["error"]["message"]);
                }
                //return jarr[0]["translations"][0]["text"].ToString();

            }
        }

        private static Dictionary<string, string> _languages;
    }
}
