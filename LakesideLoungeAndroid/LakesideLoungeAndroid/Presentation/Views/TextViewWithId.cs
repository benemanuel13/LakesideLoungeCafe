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

namespace LakesideLoungeAndroid.Presentation.Views
{
    public class TextViewWithId : TextView
    {
        int position;
        int componentId;

        public TextViewWithId(Context context) : base(context)
        {
        }

        public int Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public int ComponentId
        {
            get
            {
                return componentId;
            }

            set
            {
                componentId = value;
            }
        }
    }
}