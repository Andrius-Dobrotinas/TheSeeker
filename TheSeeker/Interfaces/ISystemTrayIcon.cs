using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TheSeeker.Forms
{
    /// <summary>
    /// Implementing classes must wrap and manage NofityIcon and ContextMenuStrip objects
    /// </summary>
    public interface ISystemTrayIcon : IDisposableOnce
    {
        /// <summary>
        /// System tray text
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// System tray icon
        /// </summary>
        Icon Icon { get; set; }

        /// <summary>
        /// Indicates whether this system tray icon is visible
        /// </summary>
        bool Visible { get; set; }

        /// <summary>
        /// Underlying system tray icon object
        /// </summary>
        NotifyIcon TrayIcon { get; }

        /// <summary>
        /// Context menu for the system tray icon
        /// </summary>
        ContextMenuStrip Menu { get; }

        /// <summary>
        /// Shorthand method for adding menu items to the underlying context menu
        /// </summary>
        /// <param name="text"></param>
        /// <param name="onClick"></param>
        ToolStripItem AddMenuItem(string text, EventHandler onClick);
    }
}
