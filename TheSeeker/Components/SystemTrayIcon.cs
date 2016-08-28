using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TheSeeker.Forms
{
    /// <summary>
    /// System tray icon with context menu
    /// </summary>
    public class SystemTrayIcon : ISystemTrayIcon
    {
        public NotifyIcon TrayIcon { get; }
        public ContextMenuStrip Menu { get; }
        protected ToolStripItem showMenuItem;

        public string Text
        {
            get
            {
                return TrayIcon.Text;
            }
            set
            {
                TrayIcon.Text = value;
            }
        }
        public Icon Icon
        {
            get
            {
                return TrayIcon.Icon;
            }
            set
            {
                TrayIcon.Icon = value;
            }
        }

        public bool Visible
        {
            get
            {
                return TrayIcon.Visible;
            }
            set
            {
                TrayIcon.Visible = value;
            }
        }

        /// <summary>
        /// Instantiates System Tray Icon with an owner that can be hidden and show
        /// </summary>
        public SystemTrayIcon()
        {
            TrayIcon = new NotifyIcon();

            Menu = new ContextMenuStrip();
            TrayIcon.ContextMenuStrip = Menu;
        }

        public ToolStripItem AddMenuItem(string text, EventHandler onClick)
        {
            var item = TrayIcon.ContextMenuStrip.Items.Add(text);
            item.Click += onClick;
            return item;
        }

        #region Disposing
        public bool IsDisposed { get; protected set; } = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    Menu.Dispose();
                    TrayIcon.Dispose();
                }

                IsDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
