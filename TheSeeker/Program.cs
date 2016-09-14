using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;
using System.Linq;
using TheSeeker.Configuration;
using TheSeeker.Forms.Properties;

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
            Logging.ILogger log = new Logging.DefaultTextTraceSourceLogger("Main");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            CurrentFormsSearchConfiguration configCurrent;
            ISearchManager searchManager;

            // Create a Search Manager sort of injecting desired types from config
            try
            {
                // Read configuration
                configCurrent = ((CurrentFormsSearchConfiguration)ConfigurationManager.GetSection(CurrentFormsSearchConfiguration.Name));
                if (string.IsNullOrEmpty(configCurrent.SearchType))
                    throw new ConfigurationErrorsException("\"SearchType\" cannot be empty");

                searchManager = Initialization.SearchManagerFactory.CreateNew(configCurrent,
                    ((SearchTypesConfigurationSection)ConfigurationManager.GetSection(SearchTypeElement.Name)).SearchTypes[configCurrent.SearchType]);
            }
            catch (Exception e)
            {
                log.LogError("Failed to instantiate Search manager", e.InnerException ?? e);
                MessageBox.Show($"Failed to instantiate Search manager. See log for details");
                return;
            }

            // Run Search Manager
            try
            {
                IFormSettingsProvider formSettings = new SearchFormSettingsProvider();
                using (var bitmapIcon = new IconFromHandleWrapper(Resources.TrayIcon))
                using (ISystemTrayIcon trayIcon = new SystemTrayIcon()
                {
                    Text = Application.ProductName,
                    Icon = bitmapIcon.Icon
                })
                using (var searchForm = new SearchForm(formSettings, searchManager.SearchBox, trayIcon.Menu))
                {
                    CreateTrayIconMenuItems(trayIcon, searchForm);

                    searchForm.HandleCreated += (sender, e) => trayIcon.Visible = true;

                    Application.Run(searchForm);

                    // Save form settings
                    formSettings.Save();
                }
            }
            catch (Exception e)
            {
                log.LogError("An error has occured", e);
                MessageBox.Show("An error has occured. See log for details");
            }
            finally
            {
                searchManager.Dispose();
            }
        }

        private static void CreateTrayIconMenuItems(ISystemTrayIcon trayIcon, ISearchForm form)
        {
            // Make "Show/Hide" menu item text change according to the current state of the owner
            EventHandler onDoubleClick = (sender, e) =>
            {
                if (form.Visible)
                    form.Hide();
                else
                    form.Show();
            };
            var showMenuItem = trayIcon.AddMenuItem("Show", onDoubleClick);
            trayIcon.TrayIcon.DoubleClick += onDoubleClick;
            trayIcon.Menu.Opening += (sender, e) => showMenuItem.Text = form.Visible ? "Hide" : "Show";

            trayIcon.AddMenuItem("Exit", (sender, e) => Application.Exit());
        }
    }
}