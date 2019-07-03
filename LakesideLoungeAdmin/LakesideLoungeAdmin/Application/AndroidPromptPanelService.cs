using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAdmin.Infrastructure;

namespace LakesideLoungeAdmin.Application
{
    public class AndroidPromptPanelService
    {
        public bool ConnectToTablet()
        {
            try
            {
                Network.Connect();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
