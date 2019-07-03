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

using LakesideLoungeAndroid.Domain;
using LakesideLoungeAndroid.Infrastructure;

namespace LakesideLoungeAndroid.Application
{
    public class PricesModel
    {
        List<PriceModel> priceModels = new List<PriceModel>();

        public PricesModel(int id)
        {
            List<PriceValue> prices = Database.GetPrices(id);

            foreach (PriceValue price in prices)
                this.priceModels.Add(new PriceModel(price));
        }

        public List<PriceModel> PriceModels
        {
            get
            {
                return priceModels;
            }
        }
    }
}