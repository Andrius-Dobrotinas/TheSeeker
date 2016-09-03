using System;
using System.Collections.Generic;
using System.Linq;

namespace TheSeeker.Forms
{
    /// <summary>
    /// Interface for components that can be hidden/shown
    /// </summary>
    public interface ICanHide
    {
        bool Visible { get; set; }
        void Show();
        void Hide();
    }
}
