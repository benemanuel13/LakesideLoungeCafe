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
    public class InOutsModel
    {
        List<InOutModel> models = new List<InOutModel>();

        public InOutsModel()
        {
            InOutModel model = new Application.InOutModel(1, "Eat In");
            models.Add(model);

            model = new Application.InOutModel(2, "Take Away");
            models.Add(model);
        }

        public List<InOutModel> InOutModels
        {
            get
            {
                return models;
            }
        }
    }
}