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

using LakesideLoungeAndroid.Presentation.Events;
using LakesideLoungeAndroid.Presentation.Views;

namespace LakesideLoungeAndroid.Presentation.Adapters
{
    public class QuantityPopupListViewAdapter : BaseAdapter<int>
    {
        public event EventHandler<QuantitySelectedEventArgs> QuantitySelected;

        public QuantityPopupListViewAdapter()
        {
        }

        public override int this[int position]
        {
            get
            {
                return position;
            }
        }

        public override int Count
        {
            get
            {
                return 5;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Activity context = (Activity)parent.Context;

            View view = new LinearLayout(context);
            TextViewWithId thisItem = new TextViewWithId(context);

            thisItem.Position = position;
            thisItem.Text = (position + 1).ToString();
            thisItem.SetTextColor(Color.Black);
            thisItem.SetTextSize(Android.Util.ComplexUnitType.Sp, 20);
            thisItem.Click += ThisItem_Click;

            ((LinearLayout)view).AddView(thisItem);
            thisItem.LayoutParameters.Width = 300;

            view.SetPadding(10, 0, 0, 0);

            return view;
        }

        private void ThisItem_Click(object sender, EventArgs e)
        {
            TextViewWithId quantity = (TextViewWithId)sender;

            QuantitySelected(this, new QuantitySelectedEventArgs(quantity.Position));
        }
    }
}