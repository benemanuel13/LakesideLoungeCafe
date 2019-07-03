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

namespace LakesideLoungeAndroid.Infrastructure
{
    public class Update
    {
        int id;
        string updateText;

        public Update()
        { }

        public Update(int id, string updateText)
        {
            this.id = id;
            this.updateText = updateText;
        }

        public int Id
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

        public string UpdateText
        {
            get
            {
                return updateText;
            }

            set
            {
                updateText = value;
            }
        }
    }
}