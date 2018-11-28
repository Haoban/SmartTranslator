using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TobiiTest
{
    class Preferences
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
            prefs.Add("translator", "google");
        }

        public string Get(string key)
        {
            prefs.TryGetValue(key, out string pref);
            if (pref == null)
                throw new ArgumentException("No such preference: " + key);
            return pref;
        }
    }
}
