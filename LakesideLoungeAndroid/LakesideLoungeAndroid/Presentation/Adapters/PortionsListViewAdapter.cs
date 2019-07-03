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
    public class PortionsListViewAdapter : BaseAdapter<PortionModel>
    {
        public event EventHandler<PortionSelectedEventArgs> PortionSelected;

        private int componentId;

        PortionsModel model = new PortionsModel();

        public PortionsListViewAdapter(int componentId)
        {
            this.componentId = componentId;
        }

        public override PortionModel this[int position]
        {
            get
            {
                return model.PortionModels[position];
            }
        }

        public override int Count
        {
            get
            {
                return model.PortionModels.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return model.PortionModels[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Activity context = (Activity)parent.Context;

            View view = new LinearLayout(context);

            PortionModel portionModel = model.PortionModels[position];
            TextViewWithId thisItem = new TextViewWithId(context);

            thisItem.Position = position;
            thisItem.ComponentId = componentId;
            thisItem.Text = portionModel.Description;
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
            TextViewWithId thisPortions = (TextViewWithId)sender;

            PortionSelected(this, new PortionSelectedEventArgs(model.PortionModels[thisPortions.Position].Number, thisPortions.ComponentId));
        }
    }
}