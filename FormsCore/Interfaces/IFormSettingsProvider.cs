using System;
using System.Collections.Generic;
using System.Drawing;

namespace TheSeeker.Forms
{
    public interface IFormSettingsProvider
    {
        Point DesktopLocation { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        void Save();
    }
}
