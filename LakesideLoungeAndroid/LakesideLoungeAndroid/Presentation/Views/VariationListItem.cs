using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace LakesideLoungeAndroid.Presentation.Views
{
    public class VariationListItem : View
    {
        //ViewGroup parent;

        public VariationListItem(Context context) : base(context)
        {
            Initialize(context);
        }

        public VariationListItem(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize(context);
        }

        public VariationListItem(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize(context);
        }

        private void Initialize(Context context)
        {
            //Activity view = (ListView)context;
            IList<View> views = new List<View>();

            TextView view = new TextView(context);
            view.Text = "Hello Again";
            view.SetMinHeight(100);
            view.SetMinWidth(100);
            view.Visibility = ViewStates.Visible;
            views.Add(view);

            //this.AddView(view);
            //RootView.AddContentView();
        }
    }
}