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
    public class PortionModel
    {
        private int id;
        private int number;
        private string description;

        public PortionModel(int id, int number, string description)
        {
            this.id = id;
            this.number = number;
            this.description = description;
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public int Number
        {
            get
            {
                return number;
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