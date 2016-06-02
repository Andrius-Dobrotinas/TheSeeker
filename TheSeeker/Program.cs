using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;

namespace TheSeeker.Forms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ISearchManager searchManager;

            try
            {
                Type type = Type.GetType(ConfigurationManager.AppSettings["SearchManagerFactory"]);

                ISearchManagerFactory factory = (ISearchManagerFactory)Activator.CreateInstance(type);
                searchManager = factory.CreateSearchManager();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Failed to instantiate Search manager ({e.InnerException?.Message ?? e.Message})");
                return;
            }

            try
            { 
                Application.Run(new SearchForm(searchManager));
            }
            catch (Exception e)
            {
                MessageBox.Show($"An error has occured ({e.Message})");
            }
        }
    }
}