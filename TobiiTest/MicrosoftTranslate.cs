using System;
using System.Net.Http;
using System.Text;
// NOTE: Install the Newtonsoft.Json NuGet package.
using Newtonsoft.Json;

namespace TobiiTest
{
    class MicrosoftTranslator
    {
        static string host = "https://api.cognitive.microsofttranslator.com";
        static string path = "/translate?api-version=3.0";
        // Translate to German and Italian.
        static string params_ = "&to=de&to=it";

        static string uri = host + path + params_;

        // NOTE: Replace this example key with a valid subscription key.
        static string key = "0dcb155e46094a3a8be0ba81710e4f06";

        static string text = "Hello world!";

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

        public MicrosoftTranslator(string srclang, string targlang)
        {
            SourceLanguage = srclang;
            TargetLanguage = targlang;
        }

        // async version header saved, just in case
        // async static string Translate(string text)
        static string Translate(string text)
        {
            System.Object[] body = new System.Object[] { new { Text = text } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);
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
    }
}
