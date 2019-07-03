using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;

using LakesideLoungeAdmin.Application;

namespace LakesideLoungeAdmin.Presentation.Panels
{
    public class AndroidPromptPanel : UserControl
    {
        public event EventHandler<System.EventArgs> PromptButtonClicked;

        AndroidPromptPanelService svc = new AndroidPromptPanelService();
        
        Button promptButton = new Button();

        public AndroidPromptPanel()
        {
            Height = 540;
            Width = 1200;

            promptButton.Content = " Please ensure Android Tablet\n  is Connected and expecting\n  messages then Click Here";
            promptButton.FontSize = 50;
            promptButton.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            promptButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            promptButton.Padding = new System.Windows.Thickness(5, 5, 5, 5);
            promptButton.Click += PromptButton_Click;
            Content = promptButton;
        }

        private void PromptButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (svc.ConnectToTablet())
                PromptButtonClicked(this, System.EventArgs.Empty);
            else
                MessageBox.Show("Connection could not be made!", "Lakeside Lounge");
        }
    }
}
