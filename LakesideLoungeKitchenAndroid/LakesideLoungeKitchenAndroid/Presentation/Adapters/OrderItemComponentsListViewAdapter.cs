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

using LakesideLoungeKitchenAndroid.Application;

namespace LakesideLoungeKitchenAndroid.Presentation.Adapters
{
    public class OrderItemComponentsListViewAdapter : BaseAdapter<OrderItemComponentModel>
    {
        OrderItemModel model;
        Context context;

        public OrderItemComponentsListViewAdapter(Context context, OrderItemModel model)
        {
            this.context = context;
            this.model = model;
        }

        public override OrderItemComponentModel this[int position]
        {
            get
            {
                return model.ComponentModels[position];
            }
        }

        public override int Count
        {
            get
            {
                return model.ComponentModels.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return model.ComponentModels[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView itemView = new TextView(context);
            itemView.SetTextColor(Color.Black);
            itemView.SetTextSize(Android.Util.ComplexUnitType.Sp, 25);
            itemView.Text = model.ComponentModels[position].DisplayName;

            return itemView;
        }
    }
}