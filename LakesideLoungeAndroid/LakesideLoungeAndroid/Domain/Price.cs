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

namespace LakesideLoungeAndroid.Domain
{
    public class PriceValue
    {
        int id;
        int variationId;
        DateTime startDate;
        DateTime? endDate = null;
        decimal cost;
        decimal price;

        public PriceValue(int id, int variationId, DateTime startDate, DateTime? endDate, decimal cost, decimal price)
        {
            this.id = id;
            this.variationId = variationId;
            this.startDate = startDate;
            this.endDate = endDate;
            this.cost = cost;
            this.price = price;
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