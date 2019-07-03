using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using LakesideLoungeAndroid.Infrastructure;

namespace LakesideLoungeAndroid.Application
{
    public class OrderFragmentApplicationService
    {
        public bool SaveOrder(OrderModel model, bool bluetooth)
        {
            return Database.SaveOrder(model, true, bluetooth);
        }

        public void LockOrderItem(int id)
        {
            BlueTooth.LockOrderItem(id);
        }

        public void UnlockOrderItem(int id)
        {
            BlueTooth.UnlockOrderItem(id);
        }

        public int GetNewOrderId()
        {
            return Database.GetNewOrderId();
        }

        public int GetNewOrderNumber()
        {
            return Database.GetNewOrderNumber();
        }

        public void CompleteOrder(OrderModel model)
        {
            Database.ClearCurrentOrder(true);
        }

        public void PayOrder(OrderModel model)
        {
            Database.PayOrder(model, true);
        }

        public void SetCurrentOrder(int id)
        {
            Database.SetCurrentOrder(id, true);
        }

        public string GetCustomerTypeDescription(int id)
        {
            return Database.GetCustomerTypeDescription(id);
        }

        public bool DeleteOrder(OrderModel model)
        {
            bool sendError = Database.DeleteOrder(model, true, true);

            if (sendError)
                Database.DeleteOrder(model, true, false);

            return sendError;
        }

        public void SaveOrderItemState(int id, State state)
        {
            Database.SaveOrderItemState(id, state);
        }
    }
}