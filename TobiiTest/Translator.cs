using System;
using System.Collections.Generic;

namespace TobiiTest
{
    public abstract class Translator
    {
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

        public abstract string Translate(string text);

        public static Translator Create(string translator, string src, string targ)
        {
            switch (translator)
            {
                case "Google":
                    return new GoogleTranslator(src, targ);
                case "Microsoft":
                    return new MicrosoftTranslator(src, targ);
                case "Yandex":
                    return new YandexTranslator(src, targ);
                default:
                    throw new ArgumentException("unknown translator: " + translator);
            }
        }

        public static Dictionary<string, string> Languages(string translator)
        {
            switch(translator)
            {
                case "Google":
                    return new Dictionary<string, string>
                    {
                        { "Afrikaans", "af" },
                        { "Albanian", "sq" },
                        { "Arabic", "ar" },
                        { "Armenian", "hy" },
                        { "Azerbaijani", "az" },
                        { "Basque", "eu" },
                        { "Belarusian", "be" },
                        { "Bengali", "bn" },
                        { "Bulgarian", "bg" },
                        { "Catalan", "ca" },
                        { "Chinese", "zh-CN" },
                        { "Croatian", "hr" },
                        { "Czech", "cs" },
                        { "Danish", "da" },
                        { "Dutch", "nl" },
                        { "English", "en" },
                        { "Esperanto", "eo" },
                        { "Estonian", "et" },
                        { "Filipino", "tl" },
                        { "Finnish", "fi" },
                        { "French", "fr" },
                        { "Galician", "gl" },
                        { "German", "de" },
                        { "Georgian", "ka" },
                        { "Greek", "el" },
                        { "Haitian Creole", "ht" },
                        { "Hebrew", "iw" },
                        { "Hindi", "hi" },
                        { "Hungarian", "hu" },
                        { "Icelandic", "is" },
                        { "Indonesian", "id" },
                        { "Irish", "ga" },
                        { "Italian", "it" },
                        { "Japanese", "ja" },
                        { "Korean", "ko" },
                        { "Lao", "lo" },
                        { "Latin", "la" },
                        { "Latvian", "lv" },
                        { "Lithuanian", "lt" },
                        { "Macedonian", "mk" },
                        { "Malay", "ms" },
                        { "Maltese", "mt" },
                        { "Norwegian", "no" },
                        { "Persian", "fa" },
                        { "Polish", "pl" },
                        { "Portuguese", "pt" },
                        { "Romanian", "ro" },
                        { "Russian", "ru" },
                        { "Serbian", "sr" },
                        { "Slovak", "sk" },
                        { "Slovenian", "sl" },
                        { "Spanish", "es" },
                        { "Swahili", "sw" },
                        { "Swedish", "sv" },
                        { "Tamil", "ta" },
                        { "Telugu", "te" },
                        { "Thai", "th" },
                        { "Turkish", "tr" },
                        { "Ukrainian", "uk" },
                        { "Urdu", "ur" },
                        { "Vietnamese", "vi" },
                        { "Welsh", "cy" },
                        { "Yiddish", "yi" }
                    };
                case "Microsoft":
                    return new Dictionary<string, string>
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
                case "Yandex":
                    return new Dictionary<string, string>
                    {
                        { "Afrikaans", "af" },
                        { "Albanian", "sq" },
                        { "Amharic", "am" },
                        { "Arabic", "ar" },
                        { "Armenian", "hy" },
                        { "Azerbaijan", "az" },
                        { "Bashkir", "ba" },
                        { "Basque", "eu" },
                        { "Belarusian", "be" },
                        { "Bengali", "bn" },
                        { "Bosnian", "bs" },
                        { "Bulgarian", "bg" },
                        { "Burmese", "my" },
                        { "Catalan", "ca" },
                        { "Cebuano", "ceb" },
                        { "Chinese", "zh" },
                        { "Croatian", "hr" },
                        { "Czech", "cs" },
                        { "Danish", "da" },
                        { "Dutch", "nl" },
                        { "English", "en" },
                        { "Esperanto", "eo" },
                        { "Estonian", "et" },
                        { "Finnish", "fi" },
                        { "French", "fr" },
                        { "Galician", "gl" },
                        { "Georgian", "ka" },
                        { "German", "de" },
                        { "Greek", "el" },
                        { "Gujarati", "gu" },
                        { "Haitian (Creole)", "ht" },
                        { "Hebrew", "he" },
                        { "Hill Mari", "mr" },
                        { "Hindi", "hi" },
                        { "Hungarian", "hu" },
                        { "Icelandic", "is" },
                        { "Indonesian", "id" },
                        { "Irish", "ga" },
                        { "Italian", "it" },
                        { "Japanese",  "ja" },
                        { "Javanese",  "jv" },
                        { "Kannada", "kn" },
                        { "Kazakh", "kk" },
                        { "Khmer", "km" },
                        { "Korean", "ko" },
                        { "Kyrgyz", "ky" },
                        { "Laotian", "lo" },
                        { "Latin", "la" },
                        { "Latvian", "lv" },
                        { "Lithuanian", "lt" },
                        { "Luxembourgish", "lb" },
                        { "Macedonian", "mk" },
                        { "Malagasy", "mg" },
                        { "Malay", "ms" },
                        { "Malayalam", "ml" },
                        { "Maltese", "mt" },
                        { "Maori", "mi" },
                        { "Marathi", "mr" },
                        { "Mari", "mhr" },
                        { "Mongolian", "mn" },
                        { "Nepali", "ne" },
                        { "Norwegian", "no" },
                        { "Papiamento", "pap" },
                        { "Persian", "fa" },
                        { "Polish", "pl" },
                        { "Portuguese", "pt" },
                        { "Romanian", "ro" },
                        { "Russian", "ru" },
                        { "Punjabi", "pa" },
                        { "Scottish", "gd" },
                        { "Serbian", "sr" },
                        { "Sinhala", "si" },
                        { "Slovakian", "sk" },
                        { "Slovenian", "sl" },
                        { "Spanish", "es" },
                        { "Sundanese", "su" },
                        { "Swahili", "sw" },
                        { "Swedish", "sv" },
                        { "Tagalog", "tl" },
                        { "Tajik", "tg" },
                        { "Tamil", "ta" },
                        { "Tatar", "tt" },
                        { "Telugu", "te" },
                        { "Thai", "th" },
                        { "Turkish", "tr" },
                        { "Udmurt", "udm" },
                        { "Ukrainian", "uk" },
                        { "Urdu", "ur" },
                        { "Uzbek", "uz" },
                        { "Vietnamese", "vi" },
                        { "Welsh", "cy" },
                        { "Xhosa", "xh" },
                        { "Yiddish", "yi" },
                    };
                default: throw new ArgumentException("Unknown translator: " + translator);
            }
        }
    }
}