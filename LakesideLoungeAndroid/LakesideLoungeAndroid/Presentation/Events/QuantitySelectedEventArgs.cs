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

namespace LakesideLoungeAndroid.Presentation.Events
{
    public class QuantitySelectedEventArgs : EventArgs
    {
        private int position= 0;

        public QuantitySelectedEventArgs(int position)
        {
            this.position = position;
        }

        public int Position
        {
            get
            {
                return position;
            }
        }
    }
}