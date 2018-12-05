using System;

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
    }
}