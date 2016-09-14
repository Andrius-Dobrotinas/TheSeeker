using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TheSeeker.Forms.SearchResults
{
    public class DefaultFormSettingsProvider : IFormSettingsProvider
    {
        internal static Properties.Settings Settings = Properties.Settings.Default;

        public Point DesktopLocation
        {
            get
            {
                return Settings.ResultsFormLocation;
            }
            set
            {
                Settings.ResultsFormLocation = value;
            }
        }

        public int Width
        {
            get
            {
                return Settings.ResultsFormWidth;
            }
            set
            {
                Settings.ResultsFormWidth = value;
            }
        }

        public int Height
        {
            get
            {
                return Settings.ResultsFormHeight;
            }
            set
            {
                Settings.ResultsFormHeight = value;
            }
        }

        public void Save()
        {
            Settings.Save();
        }
    }
}
