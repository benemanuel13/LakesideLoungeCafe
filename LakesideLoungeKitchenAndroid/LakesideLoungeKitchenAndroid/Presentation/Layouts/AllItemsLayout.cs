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
using LakesideLoungeKitchenAndroid.Presentation.Controls;
using LakesideLoungeKitchenAndroid.Presentation.EventArgs;

namespace LakesideLoungeKitchenAndroid.Presentation.Layouts
{
    public class AllItemsLayout : LinearLayout
    {
        ListView mainView;
        MainViewListViewAdapter adapter;

        Dictionary<int, OrderModel> orderModels = new Dictionary<int, OrderModel>();

        AllItemsLayoutService svc = new AllItemsLayoutService();

        public event EventHandler<ItemDetailsEventArgs> ItemDetails;

        public AllItemsLayout(Context context) : base(context)
        {
            SetBackgroundColor(Color.White);

            mainView = new ListView(context);
            mainView.SetPadding(10, 5, 0, 0);

            adapter = new MainViewListViewAdapter(context);
            adapter.ItemStarted += Adapter_ItemStarted;
            adapter.ItemCompleted += Adapter_ItemCompleted;
            adapter.ItemDetails += Adapter_ItemDetails;
            mainView.Adapter = adapter;

            AddView(mainView);

            InitAdapter();
            adapter.NotifyDataSetChanged();
        }

        private void InitAdapter()
        {
            List<OrderModel> incompleteOrders = svc.GetIncompleteOrderModels();

            foreach (OrderModel model in incompleteOrders)
                AddOrder(model);
        }

        private void Adapter_ItemDetails(object sender, EventArgs.ItemDetailsEventArgs e)
        {
            ItemDetails(sender, e);
        }

        private void Adapter_ItemStarted(object sender, EventArgs.ItemStartedEventArgs e)
        {
            //set OrderItem State to Started in database.
            svc.SetOrderItemState(e.Id, State.Started);

            orderModels[e.OrderId].OrderItems[e.Id].State = State.Started;

            adapter.SetState(e.Id, State.Started);
            //send STARTED message to Till Tablet
            ((MainActivity)Context).SendRecord("ITEM_STARTED," + e.Id);
        }

        private void Adapter_ItemCompleted(object sender, EventArgs.ItemCompletedEventArgs e)
        {
            //set OrderItem State to Completed in database.
            svc.SetOrderItemState(e.Id, State.Completed);
            
            orderModels[e.OrderId].OrderItems[e.Id].State = State.Completed;

            //send COMPLETED message to Till tablet.
            ((MainActivity)Context).SendRecord("ITEM_COMPLETED," + e.Id);

            adapter.RemoveItem(e.Id);
            adapter.NotifyDataSetChanged();

            OrderModel model = orderModels[e.OrderId];

            if (model.OrderItems.Values.Where(i => i.State != State.Completed).Count() == 0)
                orderModels.Remove(e.OrderId);
        }
        
        public void AddOrder(OrderModel model)
        {
            if (orderModels.ContainsKey(model.Id))
            {
                foreach (OrderItemModel itemModel in orderModels[model.Id].OrderItems.Values)
                {
                    if (!model.OrderItems.ContainsKey(itemModel.Id))
                    {
                        //Delete itemModelView
                        adapter.RemoveItem(itemModel.Id);
                        adapter.NotifyDataSetChanged();
                    }
                }

                orderModels.Remove(model.Id);
            }

            if (model.OrderItems.Values.Where(i => i.State != State.Completed).Count() != 0)
                orderModels.Add(model.Id, model);

            foreach (OrderItemModel itemModel in model.OrderItems.Values)
            {
                if (adapter.Items.Where(a => a.Id == itemModel.Id).Count() != 0)
                {
                    OrderItemViewModel item = adapter.Items.Where(a => a.Id == itemModel.Id).Single();
                    item.Name = model.Name;
                    item.Description = itemModel.DisplayName;
                    item.State = itemModel.State;

                    adapter.NotifyDataSetChanged();
                }
                else if(itemModel.State != State.Completed)
                {
                    OrderItemViewModel viewModel = new OrderItemViewModel(itemModel.Id, itemModel.OrderId, model.Name, itemModel.DisplayName, itemModel.State, itemModel.ComponentModels.Count > 0, itemModel.InOutStatus);
                    adapter.AddItem(viewModel);
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        public void LockOrderItem(int id)
        {
            if(adapter.Items.Where(a=> a.Id == id).Count() > 0)
            {
                adapter.SetState(id, State.Locked);
                adapter.NotifyDataSetChanged();
            }
        }

        public void UnlockOrderItem(int id)
        {
            if (adapter.Items.Where(a => a.Id == id).Count() > 0)
            {
                adapter.SetState(id, State.None);
                adapter.NotifyDataSetChanged();
            }
        }

        public void DeleteOrder(int id)
        {
            adapter.RemoveOrder(id);
            adapter.NotifyDataSetChanged();

            OrderModel oldModel = new OrderModel(id);
            svc.DeleteOrder(oldModel);

            if(orderModels.ContainsKey(id))
                orderModels.Remove(id);
        }
    }
}