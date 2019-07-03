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

using LakesideLoungeAndroid.Application;
using LakesideLoungeAndroid.Presentation.Activities;

namespace LakesideLoungeAndroid.Infrastructure
{
    public class BlueTooth
    {
        private static MainActivity activity;
        
        public static Context Context
        {
            set
            {
                activity = (MainActivity)value;
            }
        }

        public static bool SendRecord(string record)
        {
            return activity.SendRecord(record);
        }

        public static void LockOrderItem(int id)
        {
            activity.SendRecord("LOCK_ORDER_ITEM," + id.ToString());
        }

        public static void UnlockOrderItem(int id)
        {
            activity.SendRecord("UNLOCK_ORDER_ITEM," + id.ToString());
        }

        public static bool Test()
        {
            return SendRecord("TEST");
        }

        public static void SaveOrder(OrderModel model)
        {
            string name;

            if (model.Name == "")
                name = "Order #" + model.OrderNumber;
            else
                name = model.Name;

            string line = "SAVE_ORDER," + model.Id.ToString() + "," + name + "," + model.CustomerType.ToString() + "," + model.Date.ToShortDateString() + "," + model.OrderNumber.ToString();
            SendRecord(line);
        }

        public static void DeleteOrder(OrderModel model)
        {
            string line = "DELETE_ORDER," + model.Id;
            SendRecord(line);
        }

        public static void AddOrderItem(OrderItemModel model)
        {
            int state = (int)model.State;
            string line = "ADD_ORDER_ITEM," + model.Id.ToString() + "," + model.OrderId.ToString() + "," + model.VariationId.ToString() + "," + model.DisplayName + "," + model.InOutStatus.ToString() + "," + model.DiscountId.ToString() + "," + state.ToString();
            SendRecord(line);
        }

        public static void AddOrderItemComponent(OrderItemComponentModel component)
        {
            //ComponentModel model = Database.GetComponentModel(component.ComponentId);

            string line = "ADD_ORDERITEM_COMPONENT," + component.Id.ToString() + "," + component.OrderItemId.ToString() + "," + component.ComponentId.ToString() + "," + component.DisplayName + "," + component.Portions +"," + component.Position;
            SendRecord(line);
        }

        public static void AddOrderItemComponentComponent(OrderItemComponentComponentModel component)
        {
            string line = "ADD_ORDERITEM_SUBCOMPONENT," + component.ParentId.ToString() + "," + component.ComponentId.ToString() + "," + component.Name + "," + component.DisplayName + "," + component.Portions;
            SendRecord(line);
        }
    }
}