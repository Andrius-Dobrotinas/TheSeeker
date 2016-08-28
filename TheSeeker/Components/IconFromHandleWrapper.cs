using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace TheSeeker.Forms
{
    /// <summary>
    /// Wrapper class for icons created from handles, which results in the resulting icon not having its own handle,
    /// which means the handle must be destroyed explicitly which this class does in Dispose
    /// </summary>
    public class IconFromHandleWrapper : IDisposableOnce
    {
        public Icon Icon { get; private set; }

        /// <summary>
        /// Creates a new icon from the supplied icon handle
        /// </summary>
        public IconFromHandleWrapper(IntPtr iconHandle)
        {
            Icon = Icon.FromHandle(iconHandle);
        }

        /// <summary>
        /// Creates a new icon from bitmap
        /// </summary>
        public IconFromHandleWrapper(Bitmap iconBitmap)
        {
            var iconHandle = iconBitmap.GetHicon();
            Icon = Icon.FromHandle(iconHandle);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        #region Disposing
        public bool IsDisposed { get; private set; }

        protected void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    Icon.Dispose();
                }

                DestroyIcon(Icon.Handle);

                IsDisposed = true;
            }
        }

        ~IconFromHandleWrapper()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
