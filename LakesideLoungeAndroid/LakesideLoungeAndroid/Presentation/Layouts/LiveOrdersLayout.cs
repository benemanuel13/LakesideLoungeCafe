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

using LakesideLoungeAndroid.Presentation.Fragments;

namespace LakesideLoungeAndroid.Presentation.Layouts
{
    public class LiveOrdersLayout : LinearLayout
    {
        LiveOrdersFragment ordersFragment;
        OrderFragment orderFragment;

        public LiveOrdersLayout(Context context) : base(context)
        {
            this.Orientation = Orientation.Horizontal;
            this.SetBackgroundColor(Color.White);

            FrameLayout leftFrame = new FrameLayout(context);
            leftFrame.Id = 1;
            leftFrame.SetMinimumHeight(500);
            leftFrame.SetMinimumWidth(700);
            leftFrame.SetBackgroundColor(Color.White);
            this.AddView(leftFrame);

            FrameLayout rightFrame = new FrameLayout(context);
            rightFrame.Id = 2;
            rightFrame.SetMinimumHeight(700);
            rightFrame.SetMinimumWidth(1190);
            rightFrame.SetBackgroundColor(Color.White);

            this.AddView(rightFrame);

            ordersFragment = new LiveOrdersFragment();
            orderFragment = new OrderFragment();

            Activity activity = (Activity)context;

            FragmentTransaction ft = activity.FragmentManager.BeginTransaction();
            ft.Add(leftFrame.Id, ordersFragment);
            ft.Add(rightFrame.Id, orderFragment);
            ft.Commit();
        }

        public LiveOrdersFragment LiveOrdersFragment
        {
            get
            {
                return ordersFragment;
            }
        }

        public OrderFragment OrderFragment
        {
            get
            {
                return orderFragment;
            }
        }
    }
}