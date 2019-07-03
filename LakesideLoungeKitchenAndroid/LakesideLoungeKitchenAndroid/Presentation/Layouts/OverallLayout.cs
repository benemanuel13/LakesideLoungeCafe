using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Graphics;

using LakesideLoungeKitchenAndroid.Application;

namespace LakesideLoungeKitchenAndroid.Presentation.Layouts
{
    public class OverallLayout : FrameLayout
    {
        private static string fileName = "LakesideLoungeKitchenDB.db3";

        OrderModel model;
        OrderItemModel itemModel;
        OrderItemComponentModel componentModel;

        OverallLayoutService svc = new OverallLayoutService();

        LinearLayout currentLayout;
        private SplashScreen splashScreen;

        public OverallLayout(Context context) : base(context)
        {
            this.SetBackgroundColor(Color.White);
        }

        private void PopulateDummyData()
        {
            OrderModel model = new Application.OrderModel();
            model.Id = 1;
            model.Name = "Ben";
            OrderItemModel itemModel = new Application.OrderItemModel(1, 1, 4, "Sandwich", 0, 0, State.None);

            OrderItemComponentModel itemComponent = new OrderItemComponentModel(1, "Brie", "Brie", 1, 1, 1, 1);
            itemModel.AddComponentModel(itemComponent);

            itemComponent = new OrderItemComponentModel(2, "Something", "Something", 1, 2, 1, 2);
            itemModel.AddComponentModel(itemComponent);

            model.AddOrderItemModel(itemModel);

            itemModel = new Application.OrderItemModel(2, 1, 5, "Coffee - Americano", 0, 0, State.None);
            model.AddOrderItemModel(itemModel);

            itemModel = new Application.OrderItemModel(3, 1, 6, "Coffee - Flat White", 0, 0, State.None);
            model.AddOrderItemModel(itemModel);

            ((AllItemsLayout)currentLayout).AddOrder(model);

            svc.SaveOrder(model);
        }

        public void ShowSplashScreen()
        {
            splashScreen = new SplashScreen(this.Context);
            AddView(splashScreen);
        }

        public async void DoStartUp()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            if(!File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + fileName))
            {
                splashScreen.CurrentMessage = "Initializing Database";
                await Task.Run(() => svc.InitializeDatabase(this.Context));
            }

            splashScreen.CurrentMessage = "Lakeside Lounge Cafe (Kitchen)";
            await Task.Run(() => { });

            TimeSpan span = new TimeSpan(0, 0, 3);
            while (watch.Elapsed < span);

            DisplayAllItems();
            await Task.Run(() => { });

            //PopulateDummyData();

            ((MainActivity)this.Context).LayoutFinished = true;
        }

        private void OverallLayout_ItemDetails(object sender, EventArgs.ItemDetailsEventArgs e)
        {
            DisplaySpecificItem(e.Id);
        }

        public void DisplaySpecificItem(int id)
        {
            RemoveAllViews();
            currentLayout = new SpecificItemLayout(this.Context, id);
            AddView(currentLayout);
        }

        public void DisplayAllItems()
        {
            RemoveAllViews();
            currentLayout = new AllItemsLayout(this.Context);
            ((AllItemsLayout)currentLayout).ItemDetails += OverallLayout_ItemDetails;
            AddView(currentLayout);
        }

        public void RecieveRecord(string record)
        {
            if (record.StartsWith("SAVE_ORDER"))
            {
                model = new OrderModel();
                int firstComma = record.IndexOf(",");
                int secondComma = record.IndexOf(",", firstComma + 1);
                model.Id = Int32.Parse(record.Substring(firstComma + 1, secondComma - firstComma - 1));

                int thirdComma = record.IndexOf(",", secondComma + 1);
                model.Name = record.Substring(secondComma + 1, thirdComma - secondComma - 1);

                int fourthComma = record.IndexOf(",", thirdComma + 1);
                model.CustomerType = Int32.Parse(record.Substring(thirdComma + 1, fourthComma - thirdComma - 1));

                int fifthComma = record.IndexOf(",", fourthComma + 1);
                model.Date = DateTime.Parse(record.Substring(fourthComma + 1, fifthComma - fourthComma - 1));

                model.OrderNumber = int.Parse(record.Substring(fifthComma + 1, record.Length - fifthComma - 1));
            }
            else if (record.StartsWith("ADD_ORDER_ITEM"))
            {
                int firstComma = record.IndexOf(",");
                int secondComma = record.IndexOf(",", firstComma + 1);
                int id = Int32.Parse(record.Substring(firstComma + 1, secondComma - firstComma - 1));

                int thirdComma = record.IndexOf(",", secondComma + 1);
                int orderId = Int32.Parse(record.Substring(secondComma + 1, thirdComma - secondComma - 1));

                int fourthComma = record.IndexOf(",", thirdComma + 1);
                int variationId = Int32.Parse(record.Substring(thirdComma + 1, fourthComma - thirdComma - 1));

                int fifthComma = record.IndexOf(",", fourthComma + 1);
                string variationDisplayName = record.Substring(fourthComma + 1, fifthComma - fourthComma - 1);

                int sixthComma = record.IndexOf(",", fifthComma + 1);
                int inOutStatus = Int32.Parse(record.Substring(fifthComma + 1, sixthComma - fifthComma - 1));

                int seventhComma = record.IndexOf(",", sixthComma + 1);
                int discountId = Int32.Parse(record.Substring(sixthComma + 1, seventhComma - sixthComma - 1));
                State state = (State)Int32.Parse(record.Substring(seventhComma + 1, record.Length - seventhComma - 1));

                itemModel = new OrderItemModel(id, orderId, variationId, variationDisplayName, inOutStatus, discountId, state);
                model.OrderItems.Add(id, itemModel);
            }
            else if (record.StartsWith("ADD_ORDERITEM_COMPONENT"))
            {
                int firstComma = record.IndexOf(",");
                int secondComma = record.IndexOf(",", firstComma + 1);
                int id = Int32.Parse(record.Substring(firstComma + 1, secondComma - firstComma - 1));

                int thirdComma = record.IndexOf(",", secondComma + 1);
                int orderItemId = Int32.Parse(record.Substring(secondComma + 1, thirdComma - secondComma - 1));

                int fourthComma = record.IndexOf(",", thirdComma + 1);
                int componentId = Int32.Parse(record.Substring(thirdComma + 1, fourthComma - thirdComma - 1));

                int fifthComma = record.IndexOf(",", fourthComma + 1);
                string componentDisplayName = record.Substring(fourthComma + 1, fifthComma - fourthComma - 1);

                int sixthComma = record.IndexOf(",", fifthComma + 1);
                int portions = Int32.Parse(record.Substring(fifthComma + 1, sixthComma - fifthComma - 1));

                int position = Int32.Parse(record.Substring(sixthComma + 1, record.Length - sixthComma - 1));

                componentModel = new OrderItemComponentModel(id, "", componentDisplayName, orderItemId, componentId, portions, position);

                itemModel.ComponentModels.Add(componentModel);
            }
            else if (record.StartsWith("ADD_ORDERITEM_SUBCOMPONENT"))
            {
                int firstComma = record.IndexOf(",");
                int secondComma = record.IndexOf(",", firstComma + 1);
                int parentId = Int32.Parse(record.Substring(firstComma + 1, secondComma - firstComma - 1));

                int thirdComma = record.IndexOf(",", secondComma + 1);
                int componentId = Int32.Parse(record.Substring(secondComma + 1, thirdComma - secondComma - 1));

                int fourthComma = record.IndexOf(",", thirdComma + 1);
                string name = record.Substring(thirdComma + 1, fourthComma - thirdComma - 1);

                int fifthComma = record.IndexOf(",", fourthComma + 1);
                string displayName = record.Substring(fourthComma + 1, fifthComma - fourthComma - 1);
                int portions = Int32.Parse(record.Substring(fifthComma + 1, record.Length - fifthComma - 1));

                OrderItemComponentComponentModel subComponentModel = new OrderItemComponentComponentModel(parentId, componentId, name, displayName, portions);

                componentModel.AddComponent(subComponentModel);
            }
            else if (record.StartsWith("END_OF_ORDER"))
            {
                if (currentLayout is SpecificItemLayout)
                {
                    SpecificItemLayout thisLayout = (SpecificItemLayout)currentLayout;
                    if (model.OrderItems.ContainsKey(thisLayout.OrderItemId))
                        thisLayout.UpdateView(model.OrderItems[thisLayout.OrderItemId]);
                }
                else if (currentLayout is AllItemsLayout)
                {
                    AllItemsLayout thisLayout = (AllItemsLayout)currentLayout;
                    thisLayout.AddOrder(model);
                }

                svc.SaveOrder(model);
                model = null;
            }
            else if (record.StartsWith("LOCK_ORDER_ITEM"))
            {
                if (currentLayout is AllItemsLayout)
                {
                    AllItemsLayout thisLayout = (AllItemsLayout)currentLayout;

                    int firstComma = record.IndexOf(",");
                    int id = Int32.Parse(record.Substring(firstComma + 1, record.Length - firstComma - 1));

                    thisLayout.LockOrderItem(id);
                }
            }
            else if (record.StartsWith("UNLOCK_ORDER_ITEM"))
            {
                if (currentLayout is AllItemsLayout)
                {
                    AllItemsLayout thisLayout = (AllItemsLayout)currentLayout;

                    int firstComma = record.IndexOf(",");
                    int id = Int32.Parse(record.Substring(firstComma + 1, record.Length - firstComma - 1));

                    thisLayout.UnlockOrderItem(id);
                }
            }
            else if (record.StartsWith("DELETE_ORDER"))
            {
                int firstComma = record.IndexOf(",");
                int id = Int32.Parse(record.Substring(firstComma + 1, record.Length - firstComma - 1));

                if (currentLayout is AllItemsLayout)
                {
                    AllItemsLayout thisLayout = (AllItemsLayout)currentLayout;
                    thisLayout.DeleteOrder(id);
                }
                else
                {
                    SpecificItemLayout thisLayout = (SpecificItemLayout)currentLayout;
                    thisLayout.DeleteOrder(id);
                }
            }
            else if (record.StartsWith("ADD_VARIATION,"))
                AddVariation(record);
            else if (record.StartsWith("UPDATE_VARIATION,"))
                UpdateVariation(record);
            else if (record.StartsWith("ADD_COMPONENT,"))
                AddComponent(record);
            else if (record.StartsWith("UPDATE_COMPONENT,"))
                UpdateComponent(record);
        }

        private void AddVariation(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int id = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            int parentId = Int32.Parse(update.Substring(secondComma + 1, thirdComma - secondComma - 1));

            int fourthComma = update.IndexOf(",", thirdComma + 1);
            string name = update.Substring(thirdComma + 1, fourthComma - thirdComma - 1);

            int fifthComma = update.IndexOf(",", fourthComma + 1);
            string displayName = update.Substring(fourthComma + 1, fifthComma - fourthComma - 1);

            int sixthComma = update.IndexOf(",", fifthComma + 1);
            decimal price = decimal.Parse(update.Substring(fifthComma + 1, sixthComma - fifthComma - 1));

            int seventhComma = update.IndexOf(",", sixthComma + 1);
            float points = float.Parse(update.Substring(sixthComma + 1, seventhComma - sixthComma - 1));

            decimal pointPrice = decimal.Parse(update.Substring(seventhComma + 1, update.Length - seventhComma - 1));

            svc.AddVariation(id, parentId, name, displayName, price, points, pointPrice);
        }

        private void UpdateVariation(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int id = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            int parentId = Int32.Parse(update.Substring(secondComma + 1, thirdComma - secondComma - 1));

            int fourthComma = update.IndexOf(",", thirdComma + 1);
            string name = update.Substring(thirdComma + 1, fourthComma - thirdComma - 1);

            int fifthComma = update.IndexOf(",", fourthComma + 1);
            string displayName = update.Substring(fourthComma + 1, fifthComma - fourthComma - 1);

            int sixthComma = update.IndexOf(",", fifthComma + 1);
            float points = float.Parse(update.Substring(fifthComma + 1, sixthComma - fifthComma - 1));

            decimal pointPrice = decimal.Parse(update.Substring(sixthComma + 1, update.Length - sixthComma - 1));

            svc.UpdateVariation(id, parentId, name, displayName, points, pointPrice);
        }

        private void AddComponent(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int id = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            string name = update.Substring(secondComma + 1, thirdComma - secondComma - 1);

            string displayName = update.Substring(thirdComma + 1, update.Length - thirdComma - 1);

            svc.AddComponent(id, name, displayName);
        }

        private void UpdateComponent(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int id = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            string name = update.Substring(secondComma + 1, thirdComma - secondComma - 1);

            string displayName = update.Substring(thirdComma + 1, update.Length - thirdComma - 1);

            svc.UpdateComponent(id, name, displayName);
        }
    }
}