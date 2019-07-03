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
using LakesideLoungeAndroid.Presentation.Views;
using LakesideLoungeAndroid.Presentation.Events;

namespace LakesideLoungeAndroid.Presentation.Adapters
{
    public class DiscountPopupListViewAdapter : BaseAdapter<DiscountModel>
    {
        DiscountsModel model = new DiscountsModel();

        public event EventHandler<DiscountSelectedEventArgs> DiscountSelected;

        public DiscountPopupListViewAdapter()
        {
        }

        public override DiscountModel this[int position]
        {
            get
            {
                return model.DiscountModels[position];
            }
        }

        public override int Count
        {
            get
            {
                return model.DiscountModels.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return model.DiscountModels[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Activity context = (Activity)parent.Context;

            View view = new LinearLayout(context);

            DiscountModel discountModel = model.DiscountModels[position];
            TextViewWithId thisItem = new TextViewWithId(context);

            thisItem.Position = position;
            thisItem.Text = discountModel.Description;
            thisItem.SetTextColor(Color.Black);
            thisItem.SetTextSize(Android.Util.ComplexUnitType.Sp, 20);
            thisItem.Click += ThisItem_Click;

            ((LinearLayout)view).AddView(thisItem);
            thisItem.LayoutParameters.Width = 470;

            view.SetPadding(10, 0, 0, 0);

            return view;
        }

        private void ThisItem_Click(object sender, EventArgs e)
        {
            TextViewWithId discount = (TextViewWithId)sender;
            DiscountModel discountModel = model.DiscountModels[discount.Position];

            DiscountSelected(this, new DiscountSelectedEventArgs(discountModel));
        }
    }
}