using System;
using System.Collections.Generic;

namespace TheSeeker.Forms
{
    public interface IHidable
    {
        bool Visible { get; }
        void Show();
        void Hide();
    }
}
