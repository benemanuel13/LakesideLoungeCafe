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
using Android.Util;

using LakesideLoungeKitchenAndroid.Application;
using LakesideLoungeKitchenAndroid.Presentation.EventArgs;

namespace LakesideLoungeKitchenAndroid.Presentation.Controls
{
    public class OrderItemModelView : LinearLayout
    {
        public event EventHandler<ItemStartedEventArgs> ItemStarted;
        public event EventHandler<ItemCompletedEventArgs> ItemCompleted;
        public event EventHandler<ItemDetailsEventArgs> ItemDetails;

        int id;
        int orderId;

        TextView name;
        TextView description;

        Button startButton;
        Button detailsButton;
        Button completeButton;
        Button cancelButton;

        public OrderItemModelView(Context context, OrderItemViewModel model) : base(context)
        {
            id = model.Id;
            orderId = model.OrderId;

            Orientation = Orientation.Horizontal;
            name = new TextView(context);
            name.Text = model.Name;
            name.SetTextSize(ComplexUnitType.Sp, 28);
            name.SetTextColor(Color.Blue);
            this.AddView(name);
            name.LayoutParameters.Width = 300;

            description = new TextView(context);
            description.Text = model.Description;
            description.SetTextSize(ComplexUnitType.Sp, 28);
            description.SetTextColor(Color.Black);
            this.AddView(description);
            description.LayoutParameters.Width = 800;

            startButton = new Button(context);
            startButton.Text = "Start";
            startButton.SetTextSize(ComplexUnitType.Sp, 24);

            if (model.State != State.None)
                startButton.Enabled = false;

            startButton.Click += StartButton_Click;

            this.AddView(startButton);

            detailsButton = new Button(context);
            detailsButton.Text = "Details";
            detailsButton.SetTextSize(ComplexUnitType.Sp, 24);

            if (model.State == State.Locked || !model.ContainsComponents)
                detailsButton.Enabled = false;

            detailsButton.Click += DetailsButton_Click;
            this.AddView(detailsButton);

            completeButton = new Button(context);
            completeButton.Text = "Complete";
            completeButton.SetTextSize(ComplexUnitType.Sp, 24);

            if(model.State != State.Started)
                completeButton.Enabled = false;
           
            completeButton.Click += CompleteButton_Click;

            this.AddView(completeButton);

            cancelButton = new Button(context);
            cancelButton.Text = "Cancel";
            cancelButton.SetTextSize(ComplexUnitType.Sp, 24);
            cancelButton.Click += CancelButton_Click;
            //this.AddView(cancelButton);

            TextView inOutStatus = new TextView(context);
            inOutStatus.TextSize = 25;
            inOutStatus.SetTextColor(Color.Blue);
            inOutStatus.SetPadding(15, 0, 0, 0);

            if (model.InOutStatus == 1)
                inOutStatus.Text = "Eat In";
            else
                inOutStatus.Text = "Take Away";

            this.AddView(inOutStatus);
        }

        public override int Id
        {
            get
            {
                return id;
            }
        }

        public void StartButton_Click(object sender, System.EventArgs e)
        {
            startButton.Enabled = false;
            completeButton.Enabled = true;

            description.SetTextColor(Color.Purple);

            ItemStarted(this, new ItemStartedEventArgs(id, orderId));
        }

        private void DetailsButton_Click(object sender, System.EventArgs e)
        {
            ItemDetails(this, new ItemDetailsEventArgs(id));
        }

        private void CompleteButton_Click(object sender, System.EventArgs e)
        {
            ItemCompleted(this, new ItemCompletedEventArgs(id, orderId));
        }

        private void CancelButton_Click(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
        }

        public string Name
        {
            get
            {
                return name.Text;
            }

            set
            {
                name.Text = value;
            }
        }

        public string Description
        {
            get
            {
                return description.Text;
            }

            set
            {
                description.Text = value;
            }
        }
    }
}