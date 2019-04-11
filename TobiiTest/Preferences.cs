using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TobiiTest
{
    public class Preferences
    {
        // Preferences of the application
        private Dictionary<string, string> prefs;

        public Preferences()
        {
            prefs = new Dictionary<string, string>();
            InitPrefsDefaults();
        }

        private void InitPrefsDefaults()
        {
            // Default preferences should go here
            prefs.Add("translator", "Google");
            prefs.Add("key", Key.LeftCtrl.ToString());
            prefs.Add("sssize", "Small");
            prefs.Add("screenx", "0");
            prefs.Add("screeny", "0");
            prefs.Add("magnifyFactor", "5");
        }

        public string Get(string key)
        {
            prefs.TryGetValue(key, out string pref);
            if (pref == null)
                throw new ArgumentException("No such preference: " + key);
            return pref;
        }

        public void Update(string key, string value)
        {
            prefs.TryGetValue(key, out string pref);
            if (pref == null)
            {
                prefs.Add(key, value);
            }
            else
            {
                prefs[key] = value;
            }
        }
    }
}
