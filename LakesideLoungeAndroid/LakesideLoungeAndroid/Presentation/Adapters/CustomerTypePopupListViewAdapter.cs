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
using LakesideLoungeAndroid.Application;

namespace LakesideLoungeAndroid.Presentation.Adapters
{
    public class CustomerTypePopupListViewAdapter : BaseAdapter<CustomerTypeModel>
    {
        CustomerTypesModel model = new CustomerTypesModel();

        public event EventHandler<CustomerTypeSelectedEventArgs> CustomerTypeSelected;

        public override CustomerTypeModel this[int position]
        {
            get
            {
                return model.CustomerTypeModels[position];
            }
        }

        public override int Count
        {
            get
            {
                return model.CustomerTypeModels.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return model.CustomerTypeModels[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Activity context = (Activity)parent.Context;

            View view = new LinearLayout(context);

            CustomerTypeModel discountModel = model.CustomerTypeModels[position];
            TextViewWithId thisItem = new TextViewWithId(context);

            thisItem.Position = position;
            thisItem.Text = discountModel.Description;
            thisItem.SetTextColor(Color.Black);
            thisItem.SetTextSize(Android.Util.ComplexUnitType.Sp, 30);
            thisItem.Click += ThisItem_Click;

            ((LinearLayout)view).AddView(thisItem);

            thisItem.LayoutParameters.Width = 300;

            view.SetPadding(10, 0, 0, 0);

            return view;
        }

        private void ThisItem_Click(object sender, EventArgs e)
        {
            TextViewWithId customerType = (TextViewWithId)sender;
            CustomerTypeModel customerTypeModel = model.CustomerTypeModels[customerType.Position];

            CustomerTypeSelected(this, new CustomerTypeSelectedEventArgs(customerTypeModel));
        }
    }
}