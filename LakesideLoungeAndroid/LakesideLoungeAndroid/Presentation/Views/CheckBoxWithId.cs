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
    public class CheckBoxWithId : CheckBox
    {
        int id;
        int position;
        int group;

        public CheckBoxWithId(Context context) : base(context)
        {
            
        }

        public void Check()
        {
            this.Checked = true;
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
                return id;
            }

            set
            {
                id = value;
            }
        }

        public int Group
        {
            get
            {
                return group;
            }

            set
            {
                group = value;
            }
        }
    }
}