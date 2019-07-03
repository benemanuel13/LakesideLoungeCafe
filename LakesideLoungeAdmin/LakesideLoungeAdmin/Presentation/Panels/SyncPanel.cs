using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;

using LakesideLoungeAdmin.Application;

namespace LakesideLoungeAdmin.Presentation.Panels
{
    public class SyncPanel : UserControl
    {
        SyncPanelService svc = new SyncPanelService();

        Button downloadOrdersButton = new Button();
        Button uploadItemsButton = new Button();

        public SyncPanel()
        {
            Height = 540;
            Width = 1200;

            StackPanel buttonsPanel = new StackPanel();
            buttonsPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            buttonsPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            
            downloadOrdersButton.Content = "Download Orders";
            downloadOrdersButton.FontSize = 30;
            downloadOrdersButton.Padding = new System.Windows.Thickness(5, 5, 5, 5);
            downloadOrdersButton.Margin = new System.Windows.Thickness(0, 5, 0, 25);
            downloadOrdersButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            downloadOrdersButton.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            downloadOrdersButton.Click += DownloadOrdersButton_Click;

            uploadItemsButton.Content = "Upload Items";
            uploadItemsButton.FontSize = 30;
            uploadItemsButton.Padding = new System.Windows.Thickness(5, 5, 5, 5);
            uploadItemsButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            uploadItemsButton.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            uploadItemsButton.Click += UploadItemsButton_Click;

            buttonsPanel.Children.Add(downloadOrdersButton);
            buttonsPanel.Children.Add(uploadItemsButton);

            Content = buttonsPanel;
        }

        private void DownloadOrdersButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            svc.RequestOrders();
        }

        private void UploadItemsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            svc.SendUpdates();
        }
    }
}
