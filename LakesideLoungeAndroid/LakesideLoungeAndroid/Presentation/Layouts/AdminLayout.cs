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
using LakesideLoungeAndroid.Presentation.Fragments;
using LakesideLoungeAndroid.Infrastructure;

namespace LakesideLoungeAndroid.Presentation.Layouts
{
    public class AdminLayout : LinearLayout
    {
        AdminLayoutService svc = new AdminLayoutService();

        Button listen;
        TextView listenerStatus;

        Button uploadOrders;
        TextView uploadProgress;

        Button downloadUpdates;
        TextView downloadProgress;

        Button sendUpdates;
        TextView sendProgress;

        Button deleteOldOrders;
        TextView deleteOldOrdersProgress;

        //bool listening = false;

        public AdminLayout(Context context) : base(context)
        {
            Orientation = Orientation.Vertical;
            SetMinimumHeight(500);
            SetMinimumWidth(1920);
            SetBackgroundColor(Color.White);

            listen = new Button(context);
            listen.Text = "Listen for connections";
            listen.SetTextSize(Android.Util.ComplexUnitType.Sp, 30);
            listen.Click += Listen_Click;
            //AddView(listen);

            listenerStatus = new TextView(context);
            listenerStatus.Text = "Not Listening";
            listenerStatus.SetTextSize(Android.Util.ComplexUnitType.Sp, 25);
            //AddView(listenerStatus);

            uploadOrders = new Button(context);
            uploadOrders.Text = "Upload Orders";
            uploadOrders.SetTextSize(Android.Util.ComplexUnitType.Sp, 30);
            uploadOrders.Click += UploadOrders_Click;
            AddView(uploadOrders);

            uploadProgress = new TextView(context);
            uploadProgress.Text = "";
            uploadProgress.SetTextSize(Android.Util.ComplexUnitType.Sp, 30);
            uploadProgress.SetTextColor(Color.Black);
            LinearLayout.LayoutParams lParams0 = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            lParams0.SetMargins(0, 0, 0, 10);
            uploadProgress.LayoutParameters = lParams0;
            AddView(uploadProgress);

            downloadUpdates = new Button(context);
            downloadUpdates.Text = "Download Updates";
            downloadUpdates.SetTextSize(Android.Util.ComplexUnitType.Sp, 30);
            downloadUpdates.Click += DownloadUpdates_Click;
            AddView(downloadUpdates);

            downloadProgress = new TextView(context);
            downloadProgress.Text = "";
            downloadProgress.SetTextSize(Android.Util.ComplexUnitType.Sp, 30);
            downloadProgress.SetTextColor(Color.Black);
            LinearLayout.LayoutParams lParams1 = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            lParams1.SetMargins(0, 0, 0, 10);
            downloadProgress.LayoutParameters = lParams1;
            AddView(downloadProgress);

            sendUpdates = new Button(context);
            sendUpdates.Text = "Send Updates To Kitchen";
            sendUpdates.SetTextSize(Android.Util.ComplexUnitType.Sp, 30);
            sendUpdates.Click += SendUpdates_Click;
            AddView(sendUpdates);

            sendProgress = new TextView(context);
            sendProgress.Text = "";
            sendProgress.SetTextSize(Android.Util.ComplexUnitType.Sp, 30);
            sendProgress.SetTextColor(Color.Black);
            AddView(sendProgress);

            deleteOldOrders = new Button(context);
            deleteOldOrders.Text = "Delete Old Orders";
            deleteOldOrders.SetTextSize(Android.Util.ComplexUnitType.Sp, 30);
            deleteOldOrders.Click += DeleteOldOrders_Click;
            AddView(deleteOldOrders);

            deleteOldOrdersProgress = new TextView(context);
            deleteOldOrdersProgress.Text = "";
            deleteOldOrdersProgress.SetTextSize(Android.Util.ComplexUnitType.Sp, 30);
            deleteOldOrdersProgress.SetTextColor(Color.Black);
            AddView(deleteOldOrdersProgress);
        }

        private void DeleteOldOrders_Click(object sender, EventArgs e)
        {
            deleteOldOrdersProgress.Text = "Deleting old orders.";

            Database.DeleteOldOrders();

            deleteOldOrdersProgress.Text = "Old orders deleted.";
        }

        private void SendUpdates_Click(object sender, EventArgs e)
        {
            List<Update> updates = Database.GetUpdates();

            if(updates.Count == 0)
            {
                sendProgress.Text = "No Updates to Send.";
                return;
            }

            if(BlueTooth.Test())
            {
                sendProgress.Text = "Failed to Send Updates.";
                return;
            }
            else
            {
                sendProgress.Text = "Sending Updates...";

                foreach (Update update in updates)
                    BlueTooth.SendRecord(update.UpdateText);
            }

            sendProgress.Text = "Updates Sent.";

            Database.ClearUpdates();
        }

        private void DownloadUpdates_Click(object sender, EventArgs e)
        {
            Network.GetUpdates(downloadProgress);
        }

        private void UploadOrders_Click(object sender, EventArgs e)
        {
            Network.SendOrders(uploadProgress);
        }

        private void Listen_Click(object sender, EventArgs e)
        {
            if (!svc.IsListening())
            {
                listen.Text = "Stop listening for connections";
                listenerStatus.Text = "Listening";

                svc.StartListening();
            }
            else
            {
                listen.Text = "Listen for connections";
                listenerStatus.Text = "Not Listening";
                
                svc.StopListening();
            }
        }
    }
}