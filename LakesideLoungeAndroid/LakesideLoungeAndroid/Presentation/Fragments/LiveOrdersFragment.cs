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
using Android.Graphics;
using Android.Graphics.Drawables;
using static Android.Views.ViewGroup;
using Android.Content.Res;

using LakesideLoungeAndroid.Presentation.Adapters;
using LakesideLoungeAndroid.Presentation.Layouts;
using LakesideLoungeAndroid.Application;

namespace LakesideLoungeAndroid.Presentation.Fragments
{
    public class LiveOrdersFragment : Fragment
    {
        ViewGroup container;
        LiveOrdersFragmentApplicationService svc = new LiveOrdersFragmentApplicationService();

        private LiveOrdersListViewAdapter listAdapter;

        public LiveOrdersFragment()
        {
            listAdapter = new LiveOrdersListViewAdapter();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.container = (ViewGroup)container.Parent;
            container.RemoveAllViews();

            LinearLayout layout = new LinearLayout(container.Context);
            layout.Orientation = Android.Widget.Orientation.Vertical;
            layout.SetPadding(0, 10, 0, 0);

            ListView ordersList = new ListView(layout.Context);
            ordersList.Id = 3;
            ordersList.SetBackgroundColor(Color.White);
            ordersList.ItemClick += OrdersList_ItemClick;
            ordersList.Adapter = listAdapter;

            Resources res = container.Context.Resources;
#pragma warning disable CS0618 // Type or member is obsolete
            Drawable shape = res.GetDrawable(Resource.Drawable.VariationsListBorder);
#pragma warning restore CS0618 // Type or member is obsolete
            ordersList.Background = shape;

            layout.AddView(ordersList);

            ordersList.LayoutParameters.Height = 750;
            ordersList.LayoutParameters.Width = 600;

            container.AddView(layout);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void OrdersList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (e.Id > 0)
            {
                OrderModel model = new OrderModel((int)e.Id);
                LiveOrdersLayout layout = (LiveOrdersLayout)container;

                //svc.SetCurrentOrder((int)e.Id);

                layout.OrderFragment.CurrentOrder = model;
                layout.OrderFragment.PopulateOrder(model);
            }
            else
            {
                svc.ClearCurrentOrder();
                OverallLayout overall = (OverallLayout)container.Parent;
                overall.SetLayout(0);
            }
        }

        public LiveOrdersListViewAdapter ListAdapter
        {
            get
            {
                return listAdapter;
            }
        }
    }
}