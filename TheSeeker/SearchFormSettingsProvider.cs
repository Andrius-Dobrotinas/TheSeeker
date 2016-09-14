using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TheSeeker.Forms
{
    public class SearchFormSettingsProvider : IFormSettingsProvider
    {
        internal static Properties.Settings Settings = Properties.Settings.Default;
        private const string notImplementedMessage = "Not used for this form";

        public Point DesktopLocation
        {
            get
            {
                return Settings.WindowLocation;
            }
            set
            {
                Settings.WindowLocation = value;
            }
        }

        public int Width
        {
            get
            {
                throw new NotImplementedException(notImplementedMessage);
            }
            set
            {
                throw new NotImplementedException(notImplementedMessage);
            }
        }

        public int Height
        {
            get
            {
                throw new NotImplementedException(notImplementedMessage);
            }
            set
            {
                throw new NotImplementedException(notImplementedMessage);
            }
        }

        public void Save()
        {
            Settings.Save();
        }
    }
}
