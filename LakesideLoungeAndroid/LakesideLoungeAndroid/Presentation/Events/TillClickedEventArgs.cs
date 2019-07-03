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
    public class TillClickedEventArgs : EventArgs
    {
        private string buttonPressed;

        public TillClickedEventArgs(string buttonPressed)
        {
            this.buttonPressed = buttonPressed;
        }

        public string ButtonPressed
        {
            get
            {
                return buttonPressed;
            }
        }
    }
}