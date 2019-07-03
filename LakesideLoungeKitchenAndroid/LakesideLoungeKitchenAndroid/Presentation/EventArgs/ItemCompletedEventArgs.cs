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

namespace LakesideLoungeKitchenAndroid.Presentation.EventArgs
{
    public class ItemCompletedEventArgs : System.EventArgs
    {
        int id;
        int orderId;

        public ItemCompletedEventArgs(int id, int orderId)
        {
            this.id = id;
            this.orderId = orderId;
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public int OrderId
        {
            get
            {
                return orderId;
            }
        }
    }
}