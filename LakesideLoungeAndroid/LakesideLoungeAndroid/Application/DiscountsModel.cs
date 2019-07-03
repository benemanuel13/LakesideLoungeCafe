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

using LakesideLoungeAndroid.Infrastructure;

namespace LakesideLoungeAndroid.Application
{
    public class DiscountsModel
    {
        List<DiscountModel> models;

        public DiscountsModel()
        {
            models = Database.GetDiscountModels();
        }

        public List<DiscountModel> DiscountModels
        {
            get
            {
                return models;
            }
        }
    }
}