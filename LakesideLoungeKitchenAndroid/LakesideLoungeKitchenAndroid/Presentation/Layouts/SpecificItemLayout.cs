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

using LakesideLoungeKitchenAndroid.Application;
using LakesideLoungeKitchenAndroid.Presentation.Adapters;

namespace LakesideLoungeKitchenAndroid.Presentation.Layouts
{
    public class SpecificItemLayout : LinearLayout
    {
        private SpecificItemLayoutService svc = new SpecificItemLayoutService();
        OrderItemModel model;

        LinearLayout mainView;

        public SpecificItemLayout(Context context, int id) : base(context)
        {
            model = svc.GetOrderItemModel(id);

            DisplayOrderItem(context);
        }

        private void DisplayOrderItem(Context context)
        {
            LinearLayout details = new LinearLayout(context);
            details.Orientation = Orientation.Vertical;

            LinearLayout itemDetails = new LinearLayout(context);
            itemDetails.Orientation = Orientation.Horizontal;

            TextView itemTitle = new TextView(context);
            itemTitle.SetTextColor(Color.Black);
            itemTitle.SetTextSize(Android.Util.ComplexUnitType.Sp, 35);
            itemTitle.Text = model.DisplayName;
            itemTitle.SetPadding(10, 10, 30, 0);

            TextView itemStatus = new TextView(context);
            itemStatus.SetTextColor(Color.Blue);
            itemStatus.SetTextSize(Android.Util.ComplexUnitType.Sp, 35);

            if(model.InOutStatus == 1)
                itemStatus.Text = "EAT IN";
            else
                itemStatus.Text = "TAKE AWAY";

            itemStatus.SetPadding(10, 10, 50, 0);

            itemDetails.AddView(itemTitle);
            //itemDetails.AddView(itemStatus);

            details.AddView(itemDetails);

            //mainView = new ListView(context);
            //adapter = new OrderItemComponentsListViewAdapter(context, model);
            //mainView.Adapter = adapter;

            mainView = new LinearLayout(context);
            mainView.Orientation = Orientation.Vertical;
            mainView.SetPadding(10, 10, 0, 0);

            model.ComponentModels.Sort();

            foreach (OrderItemComponentModel componentModel in model.ComponentModels)
            {
                TextView newView = new TextView(context);
                newView.TextSize = 25;
                newView.SetTextColor(Color.Black);

                string name;
                if (componentModel.Portions > 1)
                    name = componentModel.DisplayName + " (x " + componentModel.Portions + ")";
                else
                    name = componentModel.DisplayName;

                newView.Text = name;

                mainView.AddView(newView);

                foreach (OrderItemComponentComponentModel subComponentModel in componentModel.Components)
                {
                    newView = new TextView(context);
                    newView.TextSize = 20;
                    newView.SetTextColor(Color.Black);

                    if (subComponentModel.Portions > 1)
                        name = subComponentModel.DisplayName + " (x " + subComponentModel.Portions + ")";
                    else
                        name = subComponentModel.DisplayName;

                    newView.Text = name;

                    newView.SetPadding(100, 0, 0, 0);

                    mainView.AddView(newView);
                }
            }

            details.AddView(mainView);

            mainView.LayoutParameters.Width = 730;
            mainView.LayoutParameters.Height = 800;

            this.AddView(details);

            Button returnButton = new Button(context);
            returnButton.Text = "Return To Main List";
            returnButton.SetTextSize(Android.Util.ComplexUnitType.Sp, 30);
            returnButton.Click += ReturnButton_Click;

            this.AddView(returnButton);
        }

        private void ReturnButton_Click(object sender, System.EventArgs e)
        {
            OverallLayout layout = (OverallLayout)this.Parent;
            layout.DisplayAllItems();
        }

        public void UpdateView(OrderItemModel model)
        {
            RemoveAllViews();

            this.model = model;
            DisplayOrderItem(this.Context);
        }

        public int OrderItemId
        {
            get
            {
                return model.Id;
            }
        }

        public void DeleteOrder(int id)
        {
            OrderModel oldModel = new OrderModel(id);
            svc.DeleteOrder(oldModel);

            if(oldModel.OrderItems.ContainsKey(id))
            {
                RemoveAllViews();
                DisplayRemovedMessage();
            }
        }

        private void DisplayRemovedMessage()
        {
            TextView mainMessage = new TextView(this.Context);
            mainMessage.Text = "The Order this item belongs to has been Voided.";
            mainMessage.TextSize = 40;

            this.AddView(mainMessage);

            mainMessage.LayoutParameters.Width = 600;

            Button returnButton = new Button(this.Context);
            returnButton.Text = "Return To Main List";
            returnButton.SetTextSize(Android.Util.ComplexUnitType.Sp, 30);
            returnButton.Click += ReturnButton_Click;

            this.AddView(returnButton);
        }
    }
}