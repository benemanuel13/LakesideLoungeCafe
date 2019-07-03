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

namespace LakesideLoungeKitchenAndroid.Application
{
    public enum State : int
    {
        None = 0,
        Started = 1,
        Completed = 2,
        Locked = 3
    }
}