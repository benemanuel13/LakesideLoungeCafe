using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAdmin.Infrastructure;
using LakesideLoungeAdmin.Presentation.EventArgs;

namespace LakesideLoungeAdmin.Application
{
    public class ProcessOrdersWindowService
    {
        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;
        public event EventHandler<EventArgs> ProgressFinished;

        public int OrderCount
        {
            get
            {
                return Database.OrderCount;
            }
        }

        public int ProcessedOrderCount
        {
            get
            {
                return Database.ProcessedOrderCount;
            }
        }

        public int ProcessOrders()
        {
            Database.ProgressChanged += Database_ProgressChanged;

            int count = Database.ProcessOrders();

            Database.ProgressChanged -= Database_ProgressChanged;

            ProgressFinished(null, new EventArgs());

            return count;
        }

        private void Database_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressChanged(sender, e);
        }
    }
}
