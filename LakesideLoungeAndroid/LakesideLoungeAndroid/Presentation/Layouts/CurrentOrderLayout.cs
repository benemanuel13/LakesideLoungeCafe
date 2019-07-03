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
using Android.Content.Res;

using LakesideLoungeAndroid.Presentation.Fragments;

namespace LakesideLoungeAndroid.Presentation.Layouts
{
    public class CurrentOrderLayout : LinearLayout
    {
        Fragment leftFragment;
        OrderFragment orderFragment;

        FrameLayout leftFrame;
        FrameLayout rightFrame;

        public CurrentOrderLayout(Context context) : base(context)
        {
            Orientation = Android.Widget.Orientation.Vertical;
            SetMinimumHeight(500);
            SetMinimumWidth(1920);
            SetBackgroundColor(Color.White);

            LinearLayout frames = new LinearLayout(context);
            frames.Orientation = Android.Widget.Orientation.Horizontal;
            
            leftFrame = new FrameLayout(frames.Context);
            leftFrame.Id = 1;
            leftFrame.SetMinimumHeight(500);
            leftFrame.SetMinimumWidth(850);
            leftFrame.SetBackgroundColor(Color.White);
            frames.AddView(leftFrame);

            rightFrame = new FrameLayout(frames.Context);
            rightFrame.Id = 2;
            rightFrame.SetMinimumHeight(700);
            rightFrame.SetMinimumWidth(1040);
            rightFrame.SetBackgroundColor(Color.White);
            frames.AddView(rightFrame);

            AddView(frames);

            VariationsFragment variationsFragment = new VariationsFragment(1);
            leftFragment = variationsFragment;
            orderFragment = new OrderFragment();

            Activity activity = (Activity)context;

            FragmentTransaction ft = activity.FragmentManager.BeginTransaction();
            ft.Add(leftFrame.Id, variationsFragment);
            ft.Add(rightFrame.Id, orderFragment);
            ft.Commit();
        }

        public OrderFragment OrderFragment
        {
            get
            {
                return orderFragment;
            }
        }

        public Fragment LeftFragment
        {
            get
            {
                return leftFragment;
            }

            set
            {
                leftFragment = value;
            }
        }

        //public void Reset()
        //{
            //VariationsFragment variationsFragment = new VariationsFragment(1);

            //Activity activity = (Activity)this.Context;

            //FragmentTransaction ft = activity.FragmentManager.BeginTransaction();
            //ft.Replace(1, variationsFragment);
            //ft.Commit();    

            //VariationsFragment fragment = (VariationsFragment)leftFragment;
            //fragment.Reset();
        //}
    }

    public enum ViewMode
    {
        Order,
        LiveOrders,
        Admin
    }
}