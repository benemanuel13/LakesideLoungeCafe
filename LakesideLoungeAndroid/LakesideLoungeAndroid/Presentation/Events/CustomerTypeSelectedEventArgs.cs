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

namespace LakesideLoungeAndroid.Presentation.Events
{
    public class CustomerTypeSelectedEventArgs : EventArgs
    {
        CustomerTypeModel model;

        public CustomerTypeSelectedEventArgs(CustomerTypeModel model)
        {
            this.model = model;
        }

        public CustomerTypeModel Model
        {
            get
            {
                return model;
            }
        }
    }
}