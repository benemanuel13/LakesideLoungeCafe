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

using LakesideLoungeAndroid.Infrastructure;

namespace LakesideLoungeAndroid.Application
{
    public class MainActivityService
    {
        public void SendOrder(OrderModel model)
        {
            BlueTooth.SaveOrder(model);

            foreach (OrderItemModel itemModel in model.OrderItems)
            {
                BlueTooth.AddOrderItem(itemModel);

                foreach(OrderItemComponentModel componentModel in itemModel.ComponentModels)
                {
                    //OrderItemComponent newComponent = new OrderItemComponent(0, itemModel.Id, componentModel.Id, componentModel.Portions);
                    BlueTooth.AddOrderItemComponent(componentModel);

                    foreach (OrderItemComponentComponentModel subComponentModel in componentModel.Components)
                        BlueTooth.AddOrderItemComponentComponent(subComponentModel);
                }
            }

            BlueTooth.SendRecord("END_OF_ORDER");
        }

        public void SendVoidOrder(OrderModel model)
        {
            BlueTooth.DeleteOrder(model);
        }

        public void AddOrderToQueue(OrderModel model)
        {
            Database.AddOrderToQueue(model);
        }

        public void ClearOrderQueue()
        {
            Database.ClearOrderQueue();
        }
    }
}