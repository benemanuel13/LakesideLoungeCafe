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

using Android.Graphics;

using LakesideLoungeAndroid.Application;
using LakesideLoungeAndroid.Helpers;

namespace LakesideLoungeAndroid.Presentation.Adapters
{
    class PricesListViewAdapter : BaseAdapter<PriceModel>
    {
        PricesModel model;

        public PricesModel Model
        {
            set
            {
                model = value;
            }
        }

        public override PriceModel this[int position]
        {
            get
            {
                return model.PriceModels[position];
            }
        }

        public override int Count
        {
            get
            {
                return model.PriceModels.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return model.PriceModels[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //View view = convertView;

            Activity context = (Activity)parent.Context;

            //if (view == null)
            //{
                View view = new LinearLayout(context);

                PriceModel priceModel = model.PriceModels[position];
                TextView thisItem = new TextView(context);

                if(priceModel.EndDate != null)
                    thisItem.Text = Helper.FormatDate(priceModel.StartDate) + " - " + Helper.FormatDate(priceModel.EndDate);
                else
                    thisItem.Text = Helper.FormatDate(priceModel.StartDate) + " onwards";

                thisItem.SetTextColor(Color.Black);
                thisItem.SetTextSize(Android.Util.ComplexUnitType.Sp, 20);

                ((LinearLayout)view).AddView(thisItem);

                view.SetPadding(10, 0, 0, 0);
            //}

            return view;
        }
    }
}