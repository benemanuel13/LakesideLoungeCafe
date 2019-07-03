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
    public class PortionSelectedEventArgs : EventArgs
    {
        private int number;
        private int componentId;

        public PortionSelectedEventArgs(int number, int componentId)
        {
            this.number = number;
            this.componentId = componentId;
        }

        public int Number
        {
            get
            {
                return number;
            }
        }

        public int ComponentId
        {
            get
            {
                return componentId;
            }
        }
    }
}