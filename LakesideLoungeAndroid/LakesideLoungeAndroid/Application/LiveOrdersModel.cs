using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAndroid.Domain;
using LakesideLoungeAndroid.Infrastructure;

namespace LakesideLoungeAndroid.Application
{
    public class LiveOrdersModel
    {
        private List<OrderModel> orderModels = new List<OrderModel>();

        public LiveOrdersModel()
        {
            LiveOrders orders = Database.GetLiveOrders();

            foreach (Order order in orders.Orders)
            {
                OrderModel newOrderModel = new OrderModel(order.Id, true, order.OrderNumber);
                newOrderModel.Name = order.Name;
                orderModels.Add(newOrderModel);
            }
        }

        public List<OrderModel> OrderModels
        {
            get
            {
                return orderModels;
            }
        }

    }
}
