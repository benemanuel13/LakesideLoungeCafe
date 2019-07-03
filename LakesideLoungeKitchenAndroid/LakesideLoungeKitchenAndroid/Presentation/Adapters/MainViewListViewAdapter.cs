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

using LakesideLoungeKitchenAndroid.Application;
using LakesideLoungeKitchenAndroid.Presentation.Controls;
using LakesideLoungeKitchenAndroid.Presentation.EventArgs;

namespace LakesideLoungeKitchenAndroid.Presentation.Adapters
{
    public class MainViewListViewAdapter : BaseAdapter<OrderItemViewModel>
    {
        public event EventHandler<ItemStartedEventArgs> ItemStarted;
        public event EventHandler<ItemCompletedEventArgs> ItemCompleted;
        public event EventHandler<ItemDetailsEventArgs> ItemDetails;

        Context context;

        IncompleteOrderItemsModel model;

        public MainViewListViewAdapter(Context context)
        {
            this.context = context;
            model = new IncompleteOrderItemsModel();
        }

        public List<OrderItemViewModel> Items
        {
            get
            {
                return model.OrderItems;
            }
        }

        private void ThisItem_ItemCompleted(object sender, ItemCompletedEventArgs e)
        {
            ItemCompleted(sender, e);
        }

        private void ThisItem_ItemStarted(object sender, EventArgs.ItemStartedEventArgs e)
        {
            ItemStarted(sender, e);
        }

        public override OrderItemViewModel this[int position]
        {
            get
            {
                return model.OrderItems[position];
            }
        }

        public override int Count
        {
            get
            {
                return model.OrderItems.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return model.OrderItems[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            OrderItemModelView thisItem = new OrderItemModelView(context, model.OrderItems[position]);
            thisItem.ItemStarted += ThisItem_ItemStarted;
            thisItem.ItemCompleted += ThisItem_ItemCompleted;
            thisItem.ItemDetails += ThisItem_ItemDetails;

            return thisItem;
        }

        private void ThisItem_ItemDetails(object sender, ItemDetailsEventArgs e)
        {
            ItemDetails(sender, e);
        }

        public void AddItem(OrderItemViewModel model)
        {
            this.model.OrderItems.Add(model);
            this.model.OrderItems.Sort();
        }

        public void RemoveItem(int id)
        {
            OrderItemViewModel model = this.model.OrderItems.Where(a => a.Id == id).Single();
            this.model.OrderItems.Remove(model);
        }

        public void RemoveOrder(int orderId)
        {
            List<int> itemsToRemove = new List<int>();

            foreach (OrderItemViewModel itemModel in this.model.OrderItems.Where(a => a.OrderId == orderId))
                itemsToRemove.Add(itemModel.Id);

            foreach (int id in itemsToRemove)
            {
                OrderItemViewModel itemModel = this.model.OrderItems.Where(a => a.Id == id).First();
                this.model.OrderItems.Remove(itemModel);
            }
        }

        public void SetState(int id, State state)
        {
            OrderItemViewModel model = this.model.OrderItems.Where(a => a.Id == id).Single();
            model.State = state;
        }
    }
}