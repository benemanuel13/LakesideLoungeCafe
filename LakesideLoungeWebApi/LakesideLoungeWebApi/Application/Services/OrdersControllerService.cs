using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LakesideLoungeWebApi.Domain;
using LakesideLoungeWebApi.Infrastructure;

namespace LakesideLoungeWebApi.Application.Services
{
    public class OrdersControllerService
    {
        public void SaveOrder(Order order)
        {
            Database.SaveOrder(order);
        }
    }
}