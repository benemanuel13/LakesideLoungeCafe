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
    public class PortionsModel
    {
        private List<PortionModel> models = new List<PortionModel>();

        public PortionsModel()
        {
            models.Add(new PortionModel(1, 1, "1"));
            models.Add(new PortionModel(2, 2, "2"));
            models.Add(new PortionModel(3, 3, "3"));
        }

        public List<PortionModel> PortionModels
        {
            get
            {
                return models;
            }
        }
    }
}