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
    public class ComponentCheckedEventArgs : EventArgs
    {
        private ComponentModel model;

        public ComponentCheckedEventArgs(ComponentModel model)
        {
            this.model = model;
        }

        public ComponentModel Model
        {
            get
            {
                return model;
            }
        }

    }
}