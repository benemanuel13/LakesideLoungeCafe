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

namespace LakesideLoungeAndroid.Application
{
    public class PriceModel
    {
        int id;
        DateTime startDate;
        DateTime? endDate = null;
        decimal cost;
        decimal price;

        public PriceModel(PriceValue price)
        {
            id = price.Id;
            startDate = price.StartDate;
            endDate = price.EndDate;
            cost = price.Cost;
            this.price = price.Price;
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
        }

        public DateTime? EndDate
        {
            get
            {
                return endDate;
            }
        }

        public decimal Cost
        {
            get
            {
                return cost;
            }
        }

        public decimal Price
        {
            get
            {
                return price;
            }
        }
    }
}