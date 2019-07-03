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

using LakesideLoungeKitchenAndroid.Infrastructure;

namespace LakesideLoungeKitchenAndroid.Application
{
    public class AllItemsLayoutService
    {
        public void SetOrderItemState(int id, State state)
        {
            Database.SaveOrderItemState(id, state);
        }

        public List<OrderModel> GetIncompleteOrderModels()
        {
            return Database.GetIncompleteOrderModels();
        }

        public void SaveOrder(OrderModel model)
        {
            Database.SaveOrder(model);
        }

        public void DeleteOrder(OrderModel model)
        {
            Database.DeleteOrder(model);
        }
    }
}