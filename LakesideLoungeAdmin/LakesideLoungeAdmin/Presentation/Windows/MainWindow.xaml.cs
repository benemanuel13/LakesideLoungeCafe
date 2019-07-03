using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Net.Http;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using Microsoft.Win32;

using LakesideLoungeAdmin.Application;
using LakesideLoungeAdmin.Presentation.Panels;

namespace LakesideLoungeAdmin.Presentation.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowService svc = new MainWindowService();

        Panel currentView;

        public MainWindow()
        {
            InitializeComponent();

            svc.CreateUpdatesTransactionFile();

            currentView = new ItemsView();
            MainPanel.Children.Add(currentView);
        }

        private void Backup_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = ".bak";
            dialog.Filter = "SQLServer Backup File (.bak)|*.bak";
            dialog.FileName = "Lakeside_Backup_" + DateTime.Now.ToShortDateString().Replace("/", "-");
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string fileName = dialog.FileName;
                svc.BackupDatabase(fileName);

                MessageBox.Show("Database Backed Up Successfully.", "Backup");
            }
        }

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".bak";
            dialog.Filter = "SQLServer Backup File (.bak)|*.bak";
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                bool reset = Task.Run(() => ResetWebApi()).Result;

                if (reset)
                {
                    string fileName = dialog.FileName;
                    bool restoreResult = svc.RestoreDatabase(fileName);

                    if (restoreResult)
                    {
                        MessageBox.Show("Database Restored Successfully.", "Restore Success");
                        ReshowCurrentView();
                    }
                    else
                        MessageBox.Show("Database Failed to Restore!", "Restore Failure");
                }
                else
                    MessageBox.Show("Failed to reset WebApi database connection.  Cannot restore database, please contact Ben.", "WebApi stoppage failure.");
            }
        }

        private async Task<bool> ResetWebApi()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response;

            try
            {
                response = await client.GetAsync(new Uri("http://localhost/api/LakesideLounge/ResetDatabase"));
            }
            catch
            {
                return false;
            }

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        private void ReshowCurrentView()
        {
            if (currentView is ItemsView)
            {

                MainPanel.Children.Clear();

                currentView = new ItemsView();
                MainPanel.Children.Add(currentView);
            }
            else if (currentView is ComponentsView)
            {

                MainPanel.Children.Clear();

                currentView = new ComponentsView();
                MainPanel.Children.Add(currentView);
            }
            else if (currentView is IngredientsView)
            {

                MainPanel.Children.Clear();

                currentView = new IngredientsView();
                MainPanel.Children.Add(currentView);
            }
        }

        private void LoadTransactions_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Settings set = Settings.Default;
            string path = set.SDDriveLocation;

            MessageBox.Show(path);
        }

        private void UpdateSDDriveLocation(string newPath)
        {
            Settings set = Settings.Default;
            set.SDDriveLocation = newPath;
            set.Save();
        }

        private void ItemView_Click(object sender, RoutedEventArgs e)
        {
            if (currentView is ItemsView)
                return;

            MainPanel.Children.Clear();

            currentView = new ItemsView();
            MainPanel.Children.Add(currentView);
        }

        private void ComponentsView_Click(object sender, RoutedEventArgs e)
        {
            if (currentView is ComponentsView)
                return;

            MainPanel.Children.Clear();

            currentView = new ComponentsView();
            MainPanel.Children.Add(currentView);
        }

        private void IngredientsView_Click(object sender, RoutedEventArgs e)
        {
            if (currentView is IngredientsView)
                return;

            MainPanel.Children.Clear();

            currentView = new IngredientsView();
            MainPanel.Children.Add(currentView);
        }

        private void SyncView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (currentView is SyncView)
                return;

            MainPanel.Children.Clear();

            currentView = new SyncView();
            MainPanel.Children.Add(currentView);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void ProcessOrders_Click(object sender, RoutedEventArgs e)
        {
            ProcessOrdersWindow window = new ProcessOrdersWindow();
            window.ShowDialog();
        }
    }
}
