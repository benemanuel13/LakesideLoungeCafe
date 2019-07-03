using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using LakesideLoungeKitchenAndroid.Domain;
using LakesideLoungeKitchenAndroid.Infrastructure;

namespace LakesideLoungeKitchenAndroid.Application
{
    public class SpecificItemLayoutService
    {
        public OrderItemModel GetOrderItemModel(int id)
        {
            OrderItem item = Database.GetOrderItem(id);
            OrderItemModel model = new OrderItemModel(item);

            return model;
        }

        public void DeleteOrder(OrderModel model)
        {
            Database.DeleteOrder(model);
        }
    }
}