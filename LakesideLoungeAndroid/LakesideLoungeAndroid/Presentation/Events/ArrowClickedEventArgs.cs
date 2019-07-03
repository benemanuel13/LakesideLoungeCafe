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

using LakesideLoungeAndroid.Presentation.Views;

namespace LakesideLoungeAndroid.Presentation.Events
{
    public class ArrowClickedEventArgs
    {
        ImageViewWithId arrow;

        public ArrowClickedEventArgs(ImageViewWithId view)
        {
            arrow = view;
        }

        public ImageViewWithId Arrow
        {
            get
            {
                return arrow;
            }
        }
    }
}