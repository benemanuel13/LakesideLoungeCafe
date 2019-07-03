using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;

using LakesideLoungeAdmin.Application;

namespace LakesideLoungeAdmin.Presentation.Panels
{
    public class SyncView : StackPanel
    {
        AndroidPromptPanel panel = new AndroidPromptPanel();
        SyncPanel syncPanel;

        SyncViewService svc = new SyncViewService();

        public SyncView()
        {
            panel.PromptButtonClicked += Panel_PromptButtonClicked;
            Children.Add(panel);
        }

        ~SyncView()
        {
            if (svc.IsConnectedToTablet())
                svc.DisconnectFromTablet();
        }

        private void Panel_PromptButtonClicked(object sender, System.EventArgs e)
        {
            Children.Clear();
            panel = null;

            syncPanel = new SyncPanel();

            Children.Add(syncPanel);
        }
    }
}
