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
    public class ItemDetailsEventArgs : System.EventArgs
    {
        int id;

        public ItemDetailsEventArgs(int id)
        {
            this.id = id;
        }

        public int Id
        {
            get
            {
                return id;
            }
        }
    }
}