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

namespace LakesideLoungeAndroid.Application
{
    public class InOutModel
    {
        int id;
        string description;

        public InOutModel(int id, string description)
        {
            this.id = id;
            this.description = description;
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
        }
    }
}