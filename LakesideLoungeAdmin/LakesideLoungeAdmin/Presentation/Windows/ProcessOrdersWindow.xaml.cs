using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using LakesideLoungeAdmin.Application;

namespace LakesideLoungeAdmin.Presentation.Windows
{
    /// <summary>
    /// Interaction logic for ProcessOrdersWindow.xaml
    /// </summary>
    public partial class ProcessOrdersWindow : Window
    {
        private ProcessOrdersWindowService svc = new ProcessOrdersWindowService();

        private int ordersToProcess = 0;

        public ProcessOrdersWindow()
        {
            InitializeComponent();

            int orderCount = svc.OrderCount;
            TotalOrders.Content = orderCount;

            int processedOrderCount = svc.ProcessedOrderCount;
            ProcessedOrders.Content = processedOrderCount;

            ordersToProcess = orderCount - processedOrderCount;

            svc.ProgressChanged += Svc_ProgressChanged;
            svc.ProgressFinished += Svc_ProgressFinished;
        }

        private void Svc_ProgressFinished(object sender, System.EventArgs e)
        {
            ProcessingBar.Value = 100;
        }

        private void Svc_ProgressChanged(object sender, EventArgs.ProgressChangedEventArgs e)
        {
            ProcessingBar.Value = (e.NewValue / ordersToProcess) * 100;
        }

        private void ProcessOrders_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Processed " + svc.ProcessOrders() + " Orders.", "Process Orders");
            this.Close();
        }
    }
}
