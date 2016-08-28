using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;
using System.Linq;
using TheSeeker.Configuration;
using TheSeeker.Startup;

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
                searchManager = Factory.CreateSearchManager(configCurrent);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Failed to instantiate Search manager. Reason:\n{e.InnerException?.Message ?? e.Message}");
                return;
            }

            // Run Search Manager
            try
            {
                using (var searchForm = new SearchForm(searchManager))
                using (var bitmapIcon = new IconFromHandleWrapper(Resources.TrayIcon))
                using (ISystemTrayIcon trayIcon = new SystemTrayIcon()
                {
                    Text = Application.ProductName,
                    Icon = bitmapIcon.Icon
                })
                {
                    CreateTrayIconMenuItems(trayIcon, searchForm);

                    searchForm.HandleCreated += (sender, e) => trayIcon.Visible = true;
                    Application.Run(searchForm);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"An error has occured:\n{e.Message}");
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
        }
    }
}