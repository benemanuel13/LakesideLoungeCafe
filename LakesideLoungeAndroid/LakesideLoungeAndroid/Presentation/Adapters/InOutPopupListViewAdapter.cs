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
using LakesideLoungeAndroid.Presentation.Events;
using LakesideLoungeAndroid.Presentation.Views;

namespace LakesideLoungeAndroid.Presentation.Adapters
{
    public class InOutPopupListViewAdapter : BaseAdapter<InOutModel>
    {
        InOutsModel model = new InOutsModel();

        public event EventHandler<InOutSelectedEventArgs> InOutSelected;

        public InOutPopupListViewAdapter()
        {
        }

        public override InOutModel this[int position]
        {
            get
            {
                return model.InOutModels[position];
            }
        }

        public override int Count
        {
            get
            {
                return model.InOutModels.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return model.InOutModels[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Activity context = (Activity)parent.Context;

            View view = new LinearLayout(context);

            InOutModel inOutModel = model.InOutModels[position];
            TextViewWithId thisItem = new TextViewWithId(context);

            thisItem.Position = position;
            thisItem.Text = inOutModel.Description;
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
            TextViewWithId discount = (TextViewWithId)sender;
            InOutModel inOutModel = model.InOutModels[discount.Position];

            InOutSelected(this, new InOutSelectedEventArgs(inOutModel));
        }
    }
}