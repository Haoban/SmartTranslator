using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using MSScriptControl;

/// <summary>
/// google translate
/// </summary>
/// <param name="text">text need to be translated</param>
/// <param name="fromLanguage">automatic detection：auto</param>
/// <param name="toLanguage">chinese：zh-CN，english：en</param>
/// <returns>text has been translated</returns>
/// 

namespace TobiiTest
{
    internal class googletranslate
    {

        internal static string Translate(string text, string fromLanguage, string toLanguage)
        {
            CookieContainer cc = new CookieContainer();

            string GoogleTransBaseUrl = "https://translate.google.cn/";

            var BaseResultHtml = GetResultHtml(GoogleTransBaseUrl, cc, "");

            Regex re = new Regex(@"(?<=TKK=)(.*?)(?=\);)");

            var TKKStr = re.Match(BaseResultHtml).ToString() + ")";//get the matched TTK code of javascript in the HTML which has been return

            var TKK = ExecuteScript(TKKStr, TKKStr);//run the code of TKK，get the value of TKK

            var GetTkkJS = File.ReadAllText("./gettk.js");

            var tk = ExecuteScript("tk(\"" + text + "\",\"" + TKK + "\")", GetTkkJS);

            string googleTransUrl = "https://translate.google.com/translate_a/single?client=t&sl=" + fromLanguage + "&tl=" + toLanguage + "&hl=en&dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&ie=UTF-8&oe=UTF-8&otf=1&ssel=0&tsel=0&kc=1&tk=" + tk + "&q=" + HttpUtility.UrlEncode(text);

            var ResultHtml = GetResultHtml(googleTransUrl, cc, "https://translate.google.cn/");

            dynamic TempResult = Newtonsoft.Json.JsonConvert.DeserializeObject(ResultHtml);

            string ResultText = Convert.ToString(TempResult[0][0][0]);

            return ResultText;
        }

        
/// <summary>
/// run JS
/// </summary>S
/// <param name="sExpression">parameter</param>
/// <param name="sCode">the string in the JavaScript code</param>
/// <returns></returns>

        internal static string GetResultHtml(string url, CookieContainer cc, string refer)
        {
            var html = "";

            var webRequest = WebRequest.Create(url) as HttpWebRequest;

            webRequest.Method = "GET";

            webRequest.CookieContainer = cc;//modify

            webRequest.Referer = "https://translate.google.cn/";//Grabed from google translate

            webRequest.Timeout = 20000;

            webRequest.Headers.Add("X-Requested-With:XMLHttpRequest");

            webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";

            webRequest.UserAgent = "Chrome/70.0.3538.77";//Grabed from google translate

            using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                using (var reader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                {

                    html = reader.ReadToEnd();
                    reader.Close();
                    webResponse.Close();
                }
            }
            return html;
        }

/// <summary>
/// run JS
/// </summary>
/// <param name="sExpression">parameter</param>
/// <param name="sCode">the string in the JavaScript code</param>
/// <returns></returns>

        internal static string ExecuteScript(string sExpression, string sCode)
        {
            ScriptControl scriptControl = new ScriptControl();
            scriptControl.UseSafeSubset = true;
            scriptControl.Language = "JScript";
            scriptControl.AddCode(sCode);
            try
            {
                string str = scriptControl.Eval(sExpression).ToString();
                return str;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            return null;
        }

    }
}
