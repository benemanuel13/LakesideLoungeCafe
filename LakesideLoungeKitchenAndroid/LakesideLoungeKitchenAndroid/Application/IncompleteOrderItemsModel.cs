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
    public class IncompleteOrderItemsModel
    {
        List<OrderItemViewModel> items = new List<OrderItemViewModel>();

        public IncompleteOrderItemsModel()
        {
            //items = Database.GetIncompleteOrderItems();
        }

        public List<OrderItemViewModel> OrderItems
        {
            get
            {
                return items;
            }
        }
    }
}