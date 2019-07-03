using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using Android.Graphics;
using Android.Views.InputMethods;
using Android.Graphics.Drawables;
using Android.Content.Res;

using LakesideLoungeAndroid.Application;
using LakesideLoungeAndroid.Presentation.Adapters;
using LakesideLoungeAndroid.Presentation.Layouts;
using LakesideLoungeAndroid.Helpers;
using LakesideLoungeAndroid.Presentation.Views;
using LakesideLoungeAndroid.Presentation.Activities;
using LakesideLoungeAndroid.Presentation.Popups;

namespace LakesideLoungeAndroid.Presentation.Fragments
{
    public class OrderFragment : Fragment
    {
        TableLayout layout;
        TextView orderTitleView;
        EditText nameEditText;
        TextView totalView;
        Button navigateButton;

        Button customerTypeButton;

        //int customerTypeId = 1;
        TextView customerTypeText;

        List<ImageViewWithPosition> deleteButtons = new List<ImageViewWithPosition>();
        List<ImageViewWithId> editButtons = new List<ImageViewWithId>();
        List<TextView> nameViews = new List<TextView>();
        List<TextView> priceViews = new List<TextView>();

        PopupWindow popupWindow;
        TillPopup tillPopup = null;

        ViewGroup container;
        OrderModel currentOrder;

        bool isEditing = false;

        int editingPosition;
        int lockedOrderItemId = 0;

        OrderFragmentApplicationService svc = new OrderFragmentApplicationService();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            container.RemoveAllViews();

            this.container = container;

            LinearLayout root = new LinearLayout(container.Context);
            root.Orientation = Android.Widget.Orientation.Vertical;
            root.SetPadding(10, 0, 0, 0);

            orderTitleView = new TextView(container.Context);
            orderTitleView.SetTextSize(ComplexUnitType.Sp, 28);
            orderTitleView.SetTextColor(Color.DarkBlue);
            orderTitleView.Click += OrderTitleView_Click;

            root.AddView(orderTitleView);

            nameEditText = new EditText(container.Context);
            nameEditText.SetTextSize(ComplexUnitType.Sp, 30);
            nameEditText.SetTextColor(Color.Black);
            nameEditText.SetMaxLines(1);
            nameEditText.TextChanged += NameEditText_TextChanged;
            nameEditText.Visibility = ViewStates.Gone;

            root.AddView(nameEditText);

            LinearLayout title = new LinearLayout(container.Context);
            TextView nameView = new TextView(container.Context);

            nameView.Text = "Item";
            nameView.SetTextSize(ComplexUnitType.Sp, 25);
            nameView.SetWidth(600);
            nameView.SetPadding(10, 0, 0, 0);
            nameView.SetTextColor(Color.Black);

            TextView priceView = new TextView(container.Context);
            priceView.Text = "Price (£)";
            priceView.SetTextSize(ComplexUnitType.Sp, 25);
            priceView.SetTextColor(Color.Black);

            title.AddView(nameView);
            title.AddView(priceView);

            root.AddView(title);

            ScrollView scrollView = new ScrollView(container.Context);

            layout = new TableLayout(container.Context);
            layout.SetBackgroundColor(Color.White);

            scrollView.AddView(layout);
            root.AddView(scrollView);

            LinearLayout footer = new LinearLayout(container.Context);

            if (container.Parent.Parent is CurrentOrderLayout)
            {
                customerTypeButton = new Button(container.Context);
                customerTypeButton.Text = "Customer Type";
                customerTypeButton.Click += CustomerTypeButton_Click;
                customerTypeButton.SetTextSize(ComplexUnitType.Sp, 23);
                footer.AddView(customerTypeButton);

                customerTypeText = new TextView(container.Context);
                customerTypeText.SetTextSize(ComplexUnitType.Sp, 23);
                customerTypeText.SetTextColor(Color.Black);
                customerTypeText.SetPadding(10, 0, 0, 0);
                
                footer.AddView(customerTypeText);

                customerTypeButton.LayoutParameters.Height = 120;
                customerTypeText.LayoutParameters.Width = 150;
            }

            totalView = new TextView(container.Context);
            totalView.TextSize = 35;
            totalView.SetTextColor(Color.Black);

            if (container.Parent.Parent is CurrentOrderLayout)
                totalView.SetPadding(120, 10, 0, 10);
            else
                totalView.SetPadding(580, 10, 0, 10);

            totalView.Text = "£ 0.00";

            footer.AddView(totalView);
            root.AddView(footer);

            //footer.LayoutParameters.Width = 500;

            if (container.Parent.Parent is CurrentOrderLayout)
            {
                LinearLayout buttonsLayout = new LinearLayout(container.Context);
                buttonsLayout.Orientation = Android.Widget.Orientation.Horizontal;
                //buttonsLayout.SetPadding(220, 0, 0, 0);

                Button voidButton = new Button(container.Context);
                voidButton.Text = "Void";
                voidButton.Click += VoidButton_Click;
                voidButton.SetTextSize(ComplexUnitType.Sp, 23);

                Button completeButton = new Button(container.Context);
                completeButton.Text = "Complete Order";
                completeButton.Click += CompleteButton_Click;
                completeButton.SetTextSize(ComplexUnitType.Sp, 23);

                Button payButton = new Button(container.Context);
                payButton.Text = "Pay Order";
                payButton.Click += PayButton_Click;
                payButton.SetTextSize(ComplexUnitType.Sp, 23);

                buttonsLayout.AddView(voidButton);
                buttonsLayout.AddView(completeButton);
                buttonsLayout.AddView(payButton);

                root.AddView(buttonsLayout);

                voidButton.LayoutParameters.Height = 120;
                completeButton.LayoutParameters.Height = 120;
                payButton.LayoutParameters.Height = 120;
            }
            else if (container.Parent is LiveOrdersLayout)
            {
                LiveOrdersLayout layout = (LiveOrdersLayout)container.Parent;

                if (layout.LiveOrdersFragment.ListAdapter.Count > 1)
                {
                    LinearLayout buttonsLayout = new LinearLayout(container.Context);
                    buttonsLayout.Orientation = Android.Widget.Orientation.Horizontal;
                    buttonsLayout.SetPadding(500, 0, 0, 0);

                    navigateButton = new Button(container.Context);
                    navigateButton.Text = "Go To Order";
                    navigateButton.SetTextSize(ComplexUnitType.Sp, 23);
                    navigateButton.Click += NavigateButton_Click;

                    buttonsLayout.AddView(navigateButton);
                    navigateButton.LayoutParameters.Height = 120;

                    root.AddView(buttonsLayout);
                }
                else
                    orderTitleView.Text = "";
            }

            container.AddView(root);

            scrollView.LayoutParameters.Height = 470;

            if (container.Parent is LiveOrdersLayout)
                currentOrder = OrderModel.GetFirstLiveOrderModel();
            else
                currentOrder = OrderModel.GetCurrentOrderModel();

            if (currentOrder.OrderNumber == 0)
                currentOrder.OrderNumber = svc.GetNewOrderNumber();

            PopulateOrder(currentOrder);
            
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void CustomerTypeButton_Click(object sender, EventArgs e)
        {
            if (tillPopup != null)
                return;

            if (popupWindow != null)
                return;

            FrameLayout mainView = new FrameLayout(this.Context);

            ListView popupView = new ListView(this.Context);

            CustomerTypePopupListViewAdapter adapter = new CustomerTypePopupListViewAdapter();
            adapter.CustomerTypeSelected += Adapter_CustomerTypeSelected;
            popupView.Adapter = adapter;

            mainView.AddView(popupView);

            popupWindow = new PopupWindow(mainView);
            popupWindow.Width = 400;
            popupWindow.Height = 300;

            Resources res = this.Context.Resources;
#pragma warning disable CS0618 // Type or member is obsolete
            Drawable shape = res.GetDrawable(Resource.Drawable.VariationsListBorder);
#pragma warning restore CS0618 // Type or member is obsolete
            popupWindow.SetBackgroundDrawable(shape);

            popupWindow.ShowAsDropDown(customerTypeText, 0, 0);
        }

        private void Adapter_CustomerTypeSelected(object sender, Events.CustomerTypeSelectedEventArgs e)
        {
            currentOrder.CustomerType = e.Model.Id;
            customerTypeText.Text = e.Model.Description;

            popupWindow.Dismiss();
            popupWindow = null;
        }

        private void OrderTitleView_Click(object sender, EventArgs e)
        {
            if (container.Parent.Parent is CurrentOrderLayout)
            {
                orderTitleView.Visibility = ViewStates.Gone;
                nameEditText.Visibility = ViewStates.Visible;

                nameEditText.RequestFocus();

                InitializeName();
                ShowKeyboard();
            }
        }

        private void NameEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (isEditing)
                return;

            char[] lastChars = e.Text.ToArray<char>();

            if (lastChars.Length == 0)
                return;

            string stringChars = Helper.ConvertStringFromChars(lastChars);
            int endLinePos = stringChars.IndexOf("\n");

            if (endLinePos != -1)
            {
                isEditing = true;
                nameEditText.Text = stringChars.Substring(0, endLinePos) + stringChars.Substring(endLinePos + 1, stringChars.Length - endLinePos - 1);
                isEditing = false;
                
                FinalizeName();
                HideKeyboard();
            }
        }

        private void InitializeName()
        {
            isEditing = true;
            nameEditText.Text = currentOrder.Name;
            nameEditText.SetSelection(currentOrder.Name.Length);
            isEditing = false;
        }

        private void FinalizeName()
        {
            currentOrder.Name = nameEditText.Text;

            if (currentOrder.Name == "")
                orderTitleView.Text = "Order # " + currentOrder.OrderNumber.ToString();
            else
                orderTitleView.Text = currentOrder.Name;

            nameEditText.Visibility = ViewStates.Gone;
            orderTitleView.Visibility = ViewStates.Visible;
        }

        private void ShowKeyboard()
        {
            InputMethodManager manager = this.Context.GetSystemService(Android.Content.Context.InputMethodService) as InputMethodManager;
            manager.ShowSoftInput(nameEditText, ShowFlags.Forced);
        }

        private void HideKeyboard()
        {
            InputMethodManager manager = this.Context.GetSystemService(Android.Content.Context.InputMethodService) as InputMethodManager;
            manager.HideSoftInputFromWindow(nameEditText.WindowToken, HideSoftInputFlags.None);
        }

        public OrderModel CurrentOrder
        {
            get
            {
                return currentOrder;
            }

            set
            {
                currentOrder = value;
            }
        }

        private void VoidButton_Click(object sender, EventArgs e)
        {
            if (tillPopup != null)
                return;

            if (currentOrder.OrderItems.Count > 0)
            {
                AlertDialog.Builder alertConfirm = new AlertDialog.Builder(this.Context);
                alertConfirm.SetTitle("Delete Order");
                alertConfirm.SetCancelable(false);
                alertConfirm.SetPositiveButton("OK", DeleteOrder);
                alertConfirm.SetNegativeButton("Cancel", delegate { });
                alertConfirm.SetMessage("Are you sure you wish to delete this order?");
                alertConfirm.Show();
            }
        }

        private void DeleteOrder(object sender, EventArgs e)
        {
            OrderModel oldOrder = new OrderModel(currentOrder.Id);
            oldOrder.Void = true;
            
            if (svc.DeleteOrder(oldOrder))
                ((MainActivity)this.Context).AddOrderToQueue(oldOrder);

            ResetOrder();
        }

        private void CompleteButton_Click(object sender, EventArgs e)
        {
            if (tillPopup != null)
                return;

            if (currentOrder.OrderItems.Count > 0)
            {
                if (SaveCurrentOrder(true))
                {
                    //Add Order To Message Queue
                    SaveCurrentOrder(false);
                    ((MainActivity)this.Context).AddOrderToQueue(currentOrder);

                    //await ShowBluetoothError();
                    //if ((bool)bluetoothReturn)
                    //{
                     //   bluetoothReturn = null;
                     //   return;
                    //}
                    //bluetoothReturn = null;
                    //SaveCurrentOrder(false);
                }
                
                CompleteOrder();

                ResetOrder();
            }
        }

        private void PayButton_Click(object sender, EventArgs e)
        {
            if (tillPopup != null)
                return;

            if (currentOrder.OrderItems.Count > 0)
            {
                TillLayout tillLayout = new TillLayout(this.Context, this);
                tillLayout.TotalDue = currentOrder.TotalPrice();

                tillPopup = new TillPopup(tillLayout);
                tillPopup.Width = 600;
                tillPopup.Height = 700;

                tillPopup.ShowAtLocation(container, GravityFlags.NoGravity, 90, 100);

                //tillLayout.LayoutParameters.Height = 800;
            }
        }

        public void PayOrder()
        {
            if (SaveCurrentOrder(true))
            {
                SaveCurrentOrder(false);
                ((MainActivity)this.Context).AddOrderToQueue(currentOrder);
            }

            PayCurrentOrder();

            ResetOrder();

            if(tillPopup != null)
            {
                tillPopup.Dismiss();
                tillPopup = null;
            }
        }

        public void CancelTill()
        {
            tillPopup.Dismiss();
            tillPopup = null;
        }

        private void ResetOrder()
        {
            currentOrder = new OrderModel(svc.GetNewOrderId(), true, svc.GetNewOrderNumber());

            orderTitleView.Text = "Order #" + currentOrder.OrderNumber.ToString();
            customerTypeText.Text = "Patient";

            layout.RemoveAllViews();

            nameViews.Clear();
            priceViews.Clear();
            deleteButtons.Clear();
            editButtons.Clear();

            if (container.Parent.Parent is CurrentOrderLayout)
                totalView.SetPadding(120, 10, 0, 10);
            else
                totalView.SetPadding(580, 10, 0, 10);

            totalView.Text = "£ 0.00";

            ((VariationsFragment)((CurrentOrderLayout)container.Parent.Parent).LeftFragment).Reset();
        }

        private void NavigateButton_Click(object sender, EventArgs e)
        {
            SetCurrentOrder();

            LiveOrdersLayout layout = (LiveOrdersLayout)container.Parent;
            OverallLayout overallLayout = (OverallLayout)layout.Parent;
            overallLayout.SetLayout(0);
        }

        public void AddOrderItem(OrderItemModel model)
        {
            TableRow newRow = new TableRow(container.Context);

            TextView nameView = new TextView(container.Context);
            TextView priceView = new TextView(container.Context);

            nameView.Text = model.DisplayName;
            nameView.SetPadding(10, 0, 0, 0);
            nameView.SetTextSize(ComplexUnitType.Sp, 20);
            nameView.SetTextColor(Color.Black);

            priceView.Text = model.Price.ToString("#0.00");
            priceView.SetPadding(90, 0, 0, 0);
            priceView.SetTextSize(ComplexUnitType.Sp, 20);
            priceView.SetBackgroundColor(Color.White);
            priceView.SetTextColor(Color.Black);

            newRow.AddView(nameView);
            newRow.AddView(priceView);

            nameViews.Add(nameView);
            priceViews.Add(priceView);

            if (container.Parent.Parent is CurrentOrderLayout)
            {
                ImageViewWithPosition deleteImage = new ImageViewWithPosition(container.Context);

                Resources res = container.Context.Resources;
                if (model.State == State.None)
#pragma warning disable CS0618 // Type or member is obsolete
                    deleteImage.SetImageDrawable(res.GetDrawable(Resource.Drawable.Delete));
#pragma warning restore CS0618 // Type or member is obsolete
                else if (model.State == State.Started)
#pragma warning disable CS0618 // Type or member is obsolete
                    deleteImage.SetImageDrawable(res.GetDrawable(Resource.Drawable.Locked));
#pragma warning restore CS0618 // Type or member is obsolete
                else
#pragma warning disable CS0618 // Type or member is obsolete
                    deleteImage.SetImageDrawable(res.GetDrawable(Resource.Drawable.Completed));
#pragma warning restore CS0618 // Type or member is obsolete

                deleteImage.SetPadding(15, 0, 0, 0);

                if(model.State == State.None)
                    deleteImage.Click += DeleteImage_Click;

                deleteImage.Id = model.Id;
                deleteImage.Position = deleteButtons.Count;
                deleteButtons.Add(deleteImage);

                newRow.AddView(deleteImage);

                ImageViewWithId editImage = new ImageViewWithId(container.Context);

                if(model.State == State.None)
#pragma warning disable CS0618 // Type or member is obsolete
                editImage.SetImageDrawable(res.GetDrawable(Resource.Drawable.Pen));
#pragma warning restore CS0618 // Type or member is obsolete
                else if (model.State == State.Started)
#pragma warning disable CS0618 // Type or member is obsolete
                    editImage.SetImageDrawable(res.GetDrawable(Resource.Drawable.Locked));
#pragma warning restore CS0618 // Type or member is obsolete
                else if (model.State == State.Completed)
#pragma warning disable CS0618 // Type or member is obsolete
                    editImage.SetImageDrawable(res.GetDrawable(Resource.Drawable.Completed));
#pragma warning restore CS0618 // Type or member is obsolete

                editImage.SetPadding(15, 0, 0, 0);

                if(model.State == State.None)
                    editImage.Click += EditImage_Click;

                editImage.Id = model.Id;
                editImage.Position = deleteButtons.Count - 1;
                editButtons.Add(editImage);

                newRow.AddView(editImage);
            }
            
            layout.AddView(newRow);

            nameView.LayoutParameters.Width = 600;
        }

        private void EditImage_Click(object sender, EventArgs e)
        {
            if (tillPopup != null)
                return;

            ImageViewWithId image = (ImageViewWithId)sender;

            if (container.Parent.Parent is CurrentOrderLayout)
            {
                CurrentOrderLayout layout = (CurrentOrderLayout)container.Parent.Parent;
                VariationsFragment variations = (VariationsFragment)layout.LeftFragment;

                editingPosition = image.Position;

                nameViews[editingPosition].SetTextColor(Color.DarkGreen);

                OrderItemModel thisModel = currentOrder.OrderItems[editingPosition];

                svc.LockOrderItem(thisModel.Id);
                lockedOrderItemId = thisModel.Id;

                variations.RehydrateOrderItem(thisModel);
            }
        }

        private void DeleteImage_Click(object sender, EventArgs e)
        {
            if (tillPopup != null)
                return;

            ImageViewWithPosition image = (ImageViewWithPosition)sender;
            layout.RemoveViewAt(image.Position);
            nameViews.RemoveAt(image.Position);
            priceViews.RemoveAt(image.Position);
            deleteButtons.RemoveAt(image.Position);
            editButtons.RemoveAt(image.Position);
            
            for(int i = image.Position; i < deleteButtons.Count; ++i)
            {
                ImageViewWithPosition deleteImage = deleteButtons[i];
                --deleteImage.Position;

                ImageViewWithId editImage = editButtons[i];
                --editImage.Position;
            }

            currentOrder.RemoveOrderItem(image.Position);

            SetTotalView(CurrentOrder);
        }

        private void SetTotalView(OrderModel model)
        {
            decimal total = 0;

            foreach (OrderItemModel item in model.OrderItems)
                total += item.Price;

            if (total > 9.99M)
            {
                if (container.Parent.Parent is CurrentOrderLayout)
                    totalView.SetPadding(120, 10, 0, 10);
                else
                    totalView.SetPadding(580, 10, 0, 10);
            }
            else
            {
                if (container.Parent.Parent is CurrentOrderLayout)
                    totalView.SetPadding(140, 10, 0, 10);
                else
                    totalView.SetPadding(630, 10, 0, 10);
            }

            totalView.Text = "£ " + total.ToString("#0.00");
        }

        public void AddOrderItemModel(OrderItemModel model)
        {
            currentOrder.AddOrderItemModel(model);
            SetTotalView(currentOrder);
        }

        public void PopulateOrder(OrderModel model)
        {   
            layout.RemoveAllViews();

            if (model.Id != 0 && currentOrder.Name == "")
                orderTitleView.Text = "Order #" + model.OrderNumber;
            else if (model.Id != 0 && model.Name != "")
            {
                orderTitleView.Text = model.Name;
                isEditing = true;
                nameEditText.Text = model.Name;
                nameEditText.SetSelection(model.Name.Length);
                isEditing = false;
            }

            foreach (OrderItemModel itemModel in model.OrderItems)
                AddOrderItem(itemModel);

            if(customerTypeText != null)
                customerTypeText.Text = svc.GetCustomerTypeDescription(model.CustomerType);

            SetTotalView(model);
        }

        public void SetOrderItemStarted(int id)
        {
            if (currentOrder.OrderItems.Where(i => i.Id == id).Count() != 0)
            {
                OrderItemModel model = currentOrder.OrderItems.Where(i => i.Id == id).Single();
                model.State = State.Started;

                Resources res = container.Context.Resources;
                ImageViewWithId editButton = editButtons.Where(b => b.Id == id).Single();
#pragma warning disable CS0618 // Type or member is obsolete
                editButton.SetImageDrawable(res.GetDrawable(Resource.Drawable.Locked));
#pragma warning restore CS0618 // Type or member is obsolete

                editButton.Click -= EditImage_Click;

                ImageViewWithPosition deleteButton = deleteButtons.Where(b => b.Id == id).Single();
#pragma warning disable CS0618 // Type or member is obsolete
                deleteButton.SetImageDrawable(res.GetDrawable(Resource.Drawable.Locked));
#pragma warning restore CS0618 // Type or member is obsolete

                deleteButton.Click -= DeleteImage_Click;
            }

            SaveOrderItemState(id, State.Started);
        }

        public void SetOrderItemCompleted(int id)
        {
            if (currentOrder.OrderItems.Where(i => i.Id == id).Count() != 0)
            {
                OrderItemModel model = currentOrder.OrderItems.Where(i => i.Id == id).Single();
                model.State = State.Completed;

                Resources res = container.Context.Resources;
                ImageViewWithId editButton = editButtons.Where(b => b.Id == id).Single();
#pragma warning disable CS0618 // Type or member is obsolete
                editButton.SetImageDrawable(res.GetDrawable(Resource.Drawable.Completed));
#pragma warning restore CS0618 // Type or member is obsolete

                ImageViewWithPosition deleteButton = deleteButtons.Where(b => b.Id == id).Single();
#pragma warning disable CS0618 // Type or member is obsolete
                deleteButton.SetImageDrawable(res.GetDrawable(Resource.Drawable.Completed));
#pragma warning restore CS0618 // Type or member is obsolete
            }

            SaveOrderItemState(id, State.Completed);
        }

        private void SaveOrderItemState(int orderItemId, State state)
        {
            svc.SaveOrderItemState(orderItemId, state);
        }

        public int CurrentOrderId
        {
            get
            {
                return currentOrder.Id;
            }
        }

        public bool SaveCurrentOrder(bool bluetooth)
        {
            return svc.SaveOrder(currentOrder, bluetooth);
        }

        private void SetCurrentOrder()
        {
            svc.SetCurrentOrder(currentOrder.Id);
        }

        private void CompleteOrder()
        {
            svc.CompleteOrder(currentOrder);
        }

        private void PayCurrentOrder()
        {
            svc.PayOrder(currentOrder);
        }

        public void UpdateOrderItem(OrderItemModel model)
        {
            nameViews[editingPosition].Text = model.DisplayName;
            nameViews[editingPosition].SetTextColor(Color.Black);

            priceViews[editingPosition].Text = model.Price.ToString("#0.00");

            currentOrder.UpdateOrderItemModel(editingPosition, model);

            SetTotalView(currentOrder);
        }

        public void UnlockOrderItem()
        {
            if (lockedOrderItemId != 0)
            {
                svc.UnlockOrderItem(lockedOrderItemId);
                lockedOrderItemId = 0;
            }
        }
    }
}