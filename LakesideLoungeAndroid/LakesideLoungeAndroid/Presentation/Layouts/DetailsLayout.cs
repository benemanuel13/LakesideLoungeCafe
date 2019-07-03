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
using LakesideLoungeAndroid.Presentation.Adapters;

namespace LakesideLoungeAndroid.Presentation.Layouts
{
    class DetailsLayout : FrameLayout
    {
        ListView pricesList;

        ViewGroup container;

        List<PriceModel> prices = new List<PriceModel>();
        PricesListViewAdapter adapter;

        public DetailsLayout(PricesModel model, Context context) : base(context)
        {
            SetPadding(0, 80, 0, 0);

            pricesList = new ListView(context);
            adapter = new PricesListViewAdapter();
            adapter.Model = model;

            pricesList.Adapter = adapter;

            this.AddView(pricesList);
        }

        public ViewGroup Container
        {
            set
            {
                container = value;
            }
        }
    }
}