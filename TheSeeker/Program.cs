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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CurrentFormsSearchConfiguration configCurrent;
            ISearchManager searchManager;

            // Create a Search Manager sort of injecting desired types read from config
            try
            {
                // Read configuration and get Search Type
                configCurrent = ((CurrentFormsSearchConfiguration)ConfigurationManager.GetSection(CurrentFormsSearchConfiguration.Name));

                searchManager = Initialization.SearchManagerFactory.CreateNew(configCurrent);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Failed to instantiate Search manager. Reason:\n{e.InnerException?.Message ?? e.Message}");
                return;
            }

            // Run Search Manager
            try
            {
                InitiateSearchDelegate initSearch = (searchLocation, searchPattern) =>
                {
                    if (!searchManager.SearchBox.IsSearching)
                    {
                        return searchManager.SearchBox.Search(searchLocation, searchPattern);
                    }
                    return false;
                };

                using (var searchForm = new SearchForm(initSearch))
                using (var bitmapIcon = new IconFromHandleWrapper(Resources.TrayIcon))
                using (ISystemTrayIcon trayIcon = new SystemTrayIcon()
                {
                    Text = Application.ProductName,
                    Icon = bitmapIcon.Icon
                })
                {
                    CreateTrayIconMenuItems(trayIcon, searchForm);

                    searchForm.HandleCreated += (sender, e) => trayIcon.Visible = true;
                    searchForm.CancelSearch += (sender, e) =>
                    {
                        searchManager.SearchBox.Stop();
                    };
                    searchManager.SearchBox.SearchStopped += (sender, e) => searchForm.SearchStopped();

                    // On exit, cancel and wait for search to completely stop to allow for proper disposal of objects
                    searchForm.FormClosed += (sender, e) =>
                    {
                        searchManager.SearchBox.StopAndWait();
                    };

                    Application.Run(searchForm);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"An error has occured:\n{e.Message}");
            }
            finally
            {
                searchManager.Dispose();
            }
        }

        private static void CreateTrayIconMenuItems(ISystemTrayIcon trayIcon, SearchForm form)
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

            // TODO: add some more menu items
            /*var cancelMenuItem = trayIcon.AddMenuItem("Cancel", (sender, e) => form.CancelButton.PerformClick());
            

            trayIcon.AddMenuItem("Show", (sender, e) => Application.Exit());*/
        }
    }
}