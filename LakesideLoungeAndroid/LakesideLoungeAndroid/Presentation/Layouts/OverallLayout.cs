using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Graphics;

using LakesideLoungeAndroid.Application;
using LakesideLoungeAndroid.Presentation.Activities;
using LakesideLoungeAndroid.Infrastructure;
using LakesideLoungeAndroid.Presentation.Fragments;

namespace LakesideLoungeAndroid.Presentation.Layouts
{
    public class OverallLayout : FrameLayout
    {
        private OverallLayoutService svc = new OverallLayoutService();
        private ViewGroup currentView;
        private SplashScreen splashScreen;

        private string fileName = "LakesideLoungeDB.db3";

        public OverallLayout(Context context) : base(context)
        {
            this.SetBackgroundColor(Color.White);
            this.SetPadding(100, 60, 0, 0);
        }

        public void ShowSplashScreen()
        {
            splashScreen = new SplashScreen(this.Context);
            AddView(splashScreen);
        }

        public void SetLayout(Layout layout)
        {
            switch (layout)
            {
                case Activities.Layout.CurrentOrder:
                    {
                        if (currentView is CurrentOrderLayout)
                            return;

                        RemoveAllViews();

                        currentView = new CurrentOrderLayout(this.Context);

                        AddView(currentView);
                        break;
                    }
                case Activities.Layout.LiveOrders:
                    {
                        if (currentView is LiveOrdersLayout)
                            return;
                        else if (currentView is CurrentOrderLayout)
                        {
                            CurrentOrderLayout orderLayout = (CurrentOrderLayout)currentView;
                            OrderFragment orderFragment = orderLayout.OrderFragment;

                            if (orderFragment.CurrentOrder.OrderItems.Count > 0)
                            {
                                if (orderFragment.SaveCurrentOrder(true))
                                {
                                    orderFragment.SaveCurrentOrder(false);
                                    ((MainActivity)this.Context).AddOrderToQueue(orderFragment.CurrentOrder);
                                }
                            }
                        }

                        RemoveAllViews();

                        currentView = new LiveOrdersLayout(this.Context);

                        AddView(currentView);
                        break;
                    }
                case Activities.Layout.Admin:
                    {
                        if (currentView is AdminLayout)
                            return;

                        RemoveAllViews();

                        currentView = new AdminLayout(this.Context);

                        AddView(currentView);
                        break;
                    }
                case Activities.Layout.Reports:
                    {
                        if (currentView is ReportLayout)
                            return;

                        RemoveAllViews();

                        currentView = new ReportLayout(this.Context);

                        AddView(currentView);
                        break;
                    }
                default:
                    {
                        throw new Exception("Invalid layout selected.");
                    }
            }
        }

        public async void DoStartUp()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            if(!File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + fileName))
            {
                splashScreen.CurrentMessage = "Initializing Database";
                await Task.Run(() => Database.InitializeDatabase(this.Context));

                //Remember to comment this out in production:
                //Log.DeleteTransactionFiles();
            }

            ((MainActivity)this.Context).OrderQueue = Database.GetOrderQueue();
            
            //splashScreen.CurrentMessage = "Processing Transactions";
            //await Task.Run(() => Log.RunTransactions("Orders"));
            //await Task.Run(() => Log.RunTransactions("Updates"));

            //Log.CreateTransactionFile("Orders");

            splashScreen.CurrentMessage = "Lakeside Lounge Cafe";
            await Task.Run(() => { });

            TimeSpan span = new TimeSpan(0, 0, 4);
            while (watch.Elapsed < span);

            SetLayout(Activities.Layout.CurrentOrder);
        }

        public void RecieveRecord(string record)
        {
            if (record.StartsWith("ITEM_STARTED"))
            {
                int id = Int32.Parse(record.Substring(13, record.Length - 13));

                if (currentView is CurrentOrderLayout)
                    ((CurrentOrderLayout)currentView).OrderFragment.SetOrderItemStarted(id);
                else
                    SaveOrderItemState(id, State.Started);
            }
            else if (record.StartsWith("ITEM_COMPLETED"))
            {
                int id = Int32.Parse(record.Substring(15, record.Length - 15));

                if (currentView is CurrentOrderLayout)
                    ((CurrentOrderLayout)currentView).OrderFragment.SetOrderItemCompleted(id);
                else
                    SaveOrderItemState(id, State.Completed);
            }
        }

        private void SaveOrderItemState(int id, State state)
        {
            svc.SaveOrderItemState(id, state);
        }
    }
}