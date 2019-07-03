using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Microsoft.AspNet.SignalR;

namespace LakesideLoungeWeb.Infrastructure
{
    public class TestHub : Hub
    {
        public void Send(string message)
        {
            //Clients.Client()
            Clients.All.addNewMessageToPage(message);
        }

        public override Task OnConnected()
        {
            //Context.
            return base.OnConnected();
        }

    }
}