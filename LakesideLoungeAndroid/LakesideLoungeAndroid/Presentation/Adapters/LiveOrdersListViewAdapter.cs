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

namespace LakesideLoungeAndroid.Presentation.Adapters
{
    public class LiveOrdersListViewAdapter : BaseAdapter<OrderModel>
    {
        LiveOrdersModel model;

        public LiveOrdersListViewAdapter()
        {
            model = new LiveOrdersModel();
            model.OrderModels.Add(new OrderModel(0));
        }

        public override OrderModel this[int position]
        {
            get
            {
                return model.OrderModels[position];
            }
        }

        public override int Count
        {
            get
            {
                return model.OrderModels.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return model.OrderModels[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            Activity context = (Activity)parent.Context;

            OrderModel varModel = model.OrderModels[position];

            view = new LinearLayout(context);

            TextView thisItem = new TextView(context);

            if (varModel.Id > 0)
                thisItem.Text = varModel.DisplayName;
            else
                thisItem.Text = "(new...)";

            thisItem.SetTextSize(Android.Util.ComplexUnitType.Sp, 30);
            thisItem.SetTextColor(Color.Black);

            ((LinearLayout)view).AddView(thisItem);

            view.SetPadding(10, 0, 0, 0);

            return view;
        }
    }
}