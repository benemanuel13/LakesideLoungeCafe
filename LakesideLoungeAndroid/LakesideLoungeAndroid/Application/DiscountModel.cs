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
    public class DiscountModel
    {
        int id;
        string description;
        int discountType;
        decimal discount;

        public DiscountModel(int id, string description, int discountType, decimal discount)
        {
            this.id = id;
            this.description = description;
            this.discountType = discountType;
            this.discount = discount;
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
        }

        public int DiscountType
        {
            get
            {
                return discountType;
            }
        }

        public decimal Discount
        {
            get
            {
                return discount;
            }
        }
    }
}