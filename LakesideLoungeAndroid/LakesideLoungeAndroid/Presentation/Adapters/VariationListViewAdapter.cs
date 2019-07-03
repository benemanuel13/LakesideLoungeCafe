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
    public class VariationListViewAdapter : BaseAdapter<VariationModel>
    {
        VariationsModel model;

        public VariationListViewAdapter(int id)
        {
            model = new VariationsModel(id);
        }

        public override VariationModel this[int position]
        {
            get
            {
                return model.VariationModels[position];
            }
        }

        public override int Count
        {
            get
            {
                return model.VariationModels.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return model.VariationModels[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Activity context = (Activity)parent.Context;
              
            View view = new LinearLayout(context);

            VariationModel varModel = model.VariationModels[position];
            TextView thisItem = new TextView(context);

            thisItem.Text = varModel.Name;
            thisItem.SetTextColor(Color.Black);
            thisItem.SetTextSize(Android.Util.ComplexUnitType.Sp, 30);

            ((LinearLayout)view).AddView(thisItem);

            view.SetPadding(10, 0, 0, 0);
            
            return view;
        }
    }
}