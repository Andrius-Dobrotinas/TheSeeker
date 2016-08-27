using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;
using System.Reflection;
using System.Linq;
using TheSeeker.Configuration;
using System.Linq.Expressions;

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

            // Create a Search Manager sort of injecting desired types read from config
            try
            {
                // Read configuration and get Search Type
                var configCurrent = ((CurrentFormsSearchConfiguration)ConfigurationManager.GetSection(CurrentFormsSearchConfiguration.Name));

                // Search Type
                searchManager = Starter.GetTypes(configCurrent);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Failed to instantiate Search manager. Reason:\n{e.InnerException?.Message ?? e.Message}");
                return;
            }

            // Run Search Manager
            try
            {
               // Application.Run(new SearchForm(searchManager));
            }
            catch (Exception e)
            {
                MessageBox.Show($"An error has occured:\n{e.Message}");
            }
        }
    }
}