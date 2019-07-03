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
    class CustomerTypesModel
    {
        List<CustomerTypeModel> models;

        public CustomerTypesModel()
        {
            models = Database.GetCustomerTypeModels();
        }

        public List<CustomerTypeModel> CustomerTypeModels
        {
            get
            {
                return models;
            }
        }
    }
}