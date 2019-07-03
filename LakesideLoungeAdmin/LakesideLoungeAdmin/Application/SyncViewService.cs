using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAdmin.Infrastructure;

namespace LakesideLoungeAdmin.Application
{
    public class SyncViewService
    {
        public void DisconnectFromTablet()
        {
            Network.Disconnect();
        }

        public bool IsConnectedToTablet()
        {
            return Network.IsConnected();
        }
    }
}
