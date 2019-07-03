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

namespace LakesideLoungeKitchenAndroid.Presentation.EventArgs
{
    public class ItemStartedEventArgs : System.EventArgs
    {
        int id;
        int orderId;

        public ItemStartedEventArgs(int id, int orderId)
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