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

namespace LakesideLoungeAndroid.Presentation.Layouts
{
    public class ReportLayout : LinearLayout
    {
        private ReportsLayoutService svc = new ReportsLayoutService();

        public ReportLayout(Context context) : base(context)
        {
            this.SetBackgroundColor(Color.White);

            TextView takingsTitle = new TextView(context);
            takingsTitle.TextSize = 25;
            takingsTitle.Text = "Todays Takings: ";
            takingsTitle.SetTextColor(Color.Black);
            takingsTitle.SetPadding(10, 10, 0, 0);

            decimal todaysTotal = 0;

            List<OrderModel> todaysOrders = svc.GetTodaysOrders();

            foreach (OrderModel model in todaysOrders)
                todaysTotal += model.TotalPrice();

            TextView takings = new TextView(context);
            takings.TextSize = 35;
            takings.Text = "£" + todaysTotal.ToString("###.00");
            takings.SetTextColor(Color.Blue);
            takings.SetPadding(10, 10, 0, 0);

            this.AddView(takingsTitle);
            this.AddView(takings);
        }
    }
}