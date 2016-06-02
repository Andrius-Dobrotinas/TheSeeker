using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheSeeker.Forms
{
    public static class FormsHelper
    {
        public static Thread RunOnNewThread(Form form, bool backgroundThread)
        {
            Thread thread = new Thread(() => Application.Run(form));
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = backgroundThread;
            thread.Start();
            return thread;
        }
    }
}
