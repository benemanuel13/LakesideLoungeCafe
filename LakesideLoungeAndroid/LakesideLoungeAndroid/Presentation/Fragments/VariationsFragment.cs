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
using LakesideLoungeAndroid.Presentation.Structs;
using LakesideLoungeAndroid.Presentation.Views;
using LakesideLoungeAndroid.Application;

namespace LakesideLoungeAndroid.Presentation.Fragments
{
    public class VariationsFragment : Fragment
    {
        int thisPosition = 0;
        int variationId;
        int componentId;

        VariationsFragmentApplicationService svc = new VariationsFragmentApplicationService();
        ViewGroup container;

        BorderedList variationsList;
        private VariationListViewAdapter listAdapter;
        private ComponentListViewAdapter componentsAdapter;
        Dictionary<int, OrderItemComponentModel> components = new Dictionary<int, OrderItemComponentModel>();

        OrderItemComponentModel currentComponent;
        List<OrderItemComponentModel> editedComponents = new List<OrderItemComponentModel>();

        int currentOrderItemId = 0;
        int newOrderItemId;
        bool orderItemOverriden = false;

        int newOrderItemComponentId;

        TextView inOutText;
        int currentInOutId = 1;

        Button discountButton;
        TextView discountText;
        int currentDiscountId = 1;

        Button quantityButton;
        TextView quantityText;
        int currentQuantity = 1;

        PopupWindow popupWindow;

        ImageView backButton;
        ImageView forwardButton;
        ImageView confirmButton;

        List<VariationPosition> positions = new List<VariationPosition>();

        public VariationsFragment(int id)
        {
            listAdapter = new VariationListViewAdapter(id);
            positions.Add(new VariationPosition() { PosType = PositionType.Variations, Id = 1 });
            newOrderItemComponentId = svc.GetNewOrderItemComponentId();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            container.RemoveAllViews();

            this.container = (ViewGroup)container.Parent.Parent;

            LinearLayout layout = new LinearLayout(container.Context);
            layout.Orientation = Android.Widget.Orientation.Vertical;

            variationsList = new BorderedList(container.Context);
            variationsList.Id = 3;
            variationsList.ItemClick += VariationsList_ItemClick;
            variationsList.Adapter = listAdapter;

            Resources res = container.Context.Resources;
#pragma warning disable CS0618 // Type or member is obsolete
            Drawable shape = res.GetDrawable(Resource.Drawable.VariationsListBorder);
#pragma warning restore CS0618 // Type or member is obsolete

            //layout.AddView(variationsList);

            if (this.container is CurrentOrderLayout)
            {
                LinearLayout inOutLayout = new LinearLayout(container.Context);
                inOutLayout.SetPadding(0, 10, 0, 0);

                Button inOutButton = new Button(container.Context);
                inOutButton.Click += InOutButton_Click;
                inOutButton.Text = "Eat In / Out";
                inOutButton.SetTextSize(ComplexUnitType.Sp, 23);

                inOutText = new TextView(container.Context);
                inOutText.SetTextSize(ComplexUnitType.Sp, 23);
                inOutText.SetTextColor(Color.Black);
                inOutText.SetPadding(10, 0, 0, 0);
                inOutText.Text = "Eat In";

                inOutLayout.AddView(inOutButton);
                inOutLayout.AddView(inOutText);

                layout.AddView(inOutLayout);
                inOutButton.LayoutParameters.Width = 390;
                inOutButton.LayoutParameters.Height = 120;

                LinearLayout discountLayout = new LinearLayout(container.Context);

                discountButton = new Button(container.Context);
                discountButton.Click += DiscountButton_Click;
                discountButton.Text = "Set Discount";
                discountButton.SetTextSize(ComplexUnitType.Sp, 23);

                discountText = new TextView(container.Context);
                discountText.SetTextSize(ComplexUnitType.Sp, 23);
                discountText.SetTextColor(Color.Black);
                discountText.SetPadding(10, 0, 0, 0);
                discountText.Text = "No Discount";

                discountLayout.AddView(discountButton);
                discountLayout.AddView(discountText);

                discountText.LayoutParameters.Width = 470;

                layout.AddView(discountLayout);
                discountButton.LayoutParameters.Width = 390;
                discountButton.LayoutParameters.Height = 120;

                LinearLayout quantityLayout = new LinearLayout(container.Context);

                quantityButton = new Button(container.Context);
                quantityButton.Click += QuantityButton_Click;
                quantityButton.Text = "Set Quantity";
                quantityButton.SetTextSize(ComplexUnitType.Sp, 23);

                quantityText = new TextView(container.Context);
                quantityText.SetTextSize(ComplexUnitType.Sp, 23);
                quantityText.SetTextColor(Color.Black);
                quantityText.SetPadding(10, 0, 0, 0);
                quantityText.Text = "1";

                quantityLayout.AddView(quantityButton);
                quantityLayout.AddView(quantityText);

                layout.AddView(quantityLayout);
                quantityButton.LayoutParameters.Width = 390;
                quantityButton.LayoutParameters.Height = 120;
            }
            else
            {
                //variationsList.LayoutParameters.Height = 750;
            }

            layout.AddView(variationsList);

            variationsList.LayoutParameters.Height = 550;
            variationsList.LayoutParameters.Width = 750;

            LinearLayout buttonLayout = new LinearLayout(container.Context);
            buttonLayout.Orientation = Android.Widget.Orientation.Horizontal;

            buttonLayout.SetBackgroundColor(Color.White);

            backButton = new ImageView(container.Context);
#pragma warning disable CS0618 // Type or member is obsolete
            backButton.SetImageDrawable(res.GetDrawable(Resource.Drawable.Back));
#pragma warning restore CS0618 // Type or member is obsolete
            backButton.Click += BackButton_Click;
            backButton.Visibility = ViewStates.Invisible;

            forwardButton = new ImageView(container.Context);
#pragma warning disable CS0618 // Type or member is obsolete
            forwardButton.SetImageDrawable(res.GetDrawable(Resource.Drawable.Forward));
#pragma warning restore CS0618 // Type or member is obsolete
            forwardButton.Click += ForwardButton_Click;
            forwardButton.Visibility = ViewStates.Invisible;

            confirmButton = new ImageView(container.Context);
#pragma warning disable CS0618 // Type or member is obsolete
            confirmButton.SetImageDrawable(res.GetDrawable(Resource.Drawable.Confirm));
#pragma warning restore CS0618 // Type or member is obsolete
            confirmButton.Click += ConfirmButton_Click;
            confirmButton.Visibility = ViewStates.Invisible;

            buttonLayout.AddView(backButton);
            buttonLayout.AddView(forwardButton);
            buttonLayout.AddView(confirmButton);

            buttonLayout.SetPadding(40, 0, 0, 0);
            layout.AddView(buttonLayout);

            container.AddView(layout);

            newOrderItemId = svc.GetNewOrderItemId();

            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void QuantityButton_Click(object sender, EventArgs e)
        {
            if (popupWindow != null)
                return;

            FrameLayout mainView = new FrameLayout(this.Context);

            ListView popupView = new ListView(this.Context);

            QuantityPopupListViewAdapter adapter = new QuantityPopupListViewAdapter();
            adapter.QuantitySelected += Adapter_QuantitySelected;
            popupView.Adapter = adapter;

            mainView.AddView(popupView);

            popupWindow = new PopupWindow(mainView);
            popupWindow.Width = 80;
            popupWindow.Height = 300;

            Resources res = this.Context.Resources;
#pragma warning disable CS0618 // Type or member is obsolete
            Drawable shape = res.GetDrawable(Resource.Drawable.PopupBorder);
#pragma warning restore CS0618 // Type or member is obsolete
            popupWindow.SetBackgroundDrawable(shape);

            popupWindow.ShowAsDropDown(quantityText, 0, 0);
        }

        private void Adapter_QuantitySelected(object sender, Events.QuantitySelectedEventArgs e)
        {
            currentQuantity = e.Position + 1;
            quantityText.Text = (e.Position + 1).ToString();

            popupWindow.Dismiss();
            popupWindow = null;
        }

        public void Reset()
        {
            currentOrderItemId = 0;
            currentDiscountId = 1;
            currentQuantity = 1;

            quantityButton.Enabled = true;

            discountText.Text = "No Discount";
            quantityText.Text = "1";

            positions.Clear();
            components.Clear();
            editedComponents.Clear();

            currentComponent = null;

            thisPosition = 0;

            orderItemOverriden = false;

            positions.Add(new VariationPosition() { PosType = PositionType.Variations, Id = 1 });
            DisplayVariations(1);
        }

        private void InOutButton_Click(object sender, EventArgs e)
        {
            if (popupWindow != null)
                return;

            FrameLayout mainView = new FrameLayout(this.Context);

            ListView popupView = new ListView(this.Context);

            InOutPopupListViewAdapter adapter = new InOutPopupListViewAdapter();
            adapter.InOutSelected += Adapter_InOutSelected;
            popupView.Adapter = adapter;

            mainView.AddView(popupView);

            popupWindow = new PopupWindow(mainView);
            popupWindow.Width = 300;
            popupWindow.Height = 200;

            Resources res = this.Context.Resources;
#pragma warning disable CS0618 // Type or member is obsolete
            Drawable shape = res.GetDrawable(Resource.Drawable.VariationsListBorder);
#pragma warning restore CS0618 // Type or member is obsolete
            popupWindow.SetBackgroundDrawable(shape);

            popupWindow.ShowAsDropDown(inOutText, 0, 0);
        }

        private void Adapter_InOutSelected(object sender, Events.InOutSelectedEventArgs e)
        {
            currentInOutId = e.Model.Id;
            inOutText.Text = e.Model.Description;

            popupWindow.Dismiss();
            popupWindow = null;
        }

        private void DiscountButton_Click(object sender, EventArgs e)
        {
            if (popupWindow != null)
                return;

            FrameLayout mainView = new FrameLayout(this.Context);

            ListView popupView = new ListView(this.Context);
            
            DiscountPopupListViewAdapter adapter = new DiscountPopupListViewAdapter();
            adapter.DiscountSelected += Adapter_DiscountSelected;
            popupView.Adapter = adapter;

            mainView.AddView(popupView);

            popupWindow = new PopupWindow(mainView);
            popupWindow.Width = 470;
            popupWindow.Height = 270;

            Resources res = this.Context.Resources;
#pragma warning disable CS0618 // Type or member is obsolete
            Drawable shape = res.GetDrawable(Resource.Drawable.VariationsListBorder);
#pragma warning restore CS0618 // Type or member is obsolete
            popupWindow.SetBackgroundDrawable(shape);

            popupWindow.ShowAsDropDown(discountText, 0, 0);
        }

        private void Adapter_DiscountSelected(object sender, Events.DiscountSelectedEventArgs e)
        {
            currentDiscountId = e.Model.Id;
            discountText.Text = e.Model.Description;
            popupWindow.Dismiss();

            popupWindow = null;
        }

        public void Show()
        {
            if (container is CurrentOrderLayout)
            {
                CurrentOrderLayout thisLayout = (CurrentOrderLayout)container;

                if (thisPosition > 0)
                    backButton.Visibility = ViewStates.Visible;
                else
                    backButton.Visibility = ViewStates.Invisible;

                if (thisPosition < positions.Count() - 1)
                    forwardButton.Visibility = ViewStates.Visible;
                else
                    forwardButton.Visibility = ViewStates.Invisible;
            }
            else if (container is AdminLayout)
            {
                
            }
        }

        private void ForwardButton_Click(object sender, EventArgs e)
        {
            ++thisPosition;

            VariationPosition pos = positions[thisPosition];

            if (pos.PosType == PositionType.Variations)
            {
                if (currentOrderItemId != 0 && positions.Count - 1 == thisPosition && !orderItemOverriden)
                    confirmButton.Visibility = ViewStates.Visible;
                else
                    confirmButton.Visibility = ViewStates.Invisible;

                DisplayVariations(pos.Id);
            }
            else
                DisplayComponents(pos.Id, true, ComponentListMode.Variation);

            Show();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if(currentComponent != null)
            {
                currentComponent = null;

                DisplayComponents(positions[thisPosition].Id, true, ComponentListMode.Variation);
                Show();

                return;
            }

            --thisPosition;
            DisplayVariations(positions[thisPosition].Id);

            confirmButton.Visibility = ViewStates.Invisible;

            Show();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (container is CurrentOrderLayout)
            {
                CurrentOrderLayout thisLayout = (CurrentOrderLayout)container;
                OrderFragment orderFragment = thisLayout.OrderFragment;

                OrderItemModel model = GetOrderItemModel(variationId, orderFragment);

                UpdateOrder(orderFragment, model, true);

                Reset();
            }
        }

        private OrderItemModel GetOrderItemModel(int variationId, OrderFragment orderFragment)
        {
            int orderId = orderFragment.CurrentOrderId;

            int id;

            if (currentOrderItemId == 0)
                //id = svc.CreateNewOrderItem(orderId, variationId, currentInOutId, currentDiscountId);
                id = newOrderItemId++;
            else
                id = currentOrderItemId;

            OrderItemModel model = new OrderItemModel(id, orderId, variationId, currentInOutId, currentDiscountId, State.None);

            return model;
        }

        private void UpdateOrder(OrderFragment orderFragment, OrderItemModel model, bool addComponents)
        {
            if (addComponents)
                foreach (OrderItemComponentModel componentModel in components.Values)
                    model.AddComponentModel(componentModel);

            if (currentOrderItemId == 0)
            {
                for (int i = 0; i < currentQuantity; i++)
                {
                    OrderItemModel newModel = model.Clone();

                    if (i != 0)
                        newModel.Id = newOrderItemId++;

                    foreach (OrderItemComponentModel componentModel in newModel.ComponentModels)
                    {
                        if (i == 0)
                            break;

                        componentModel.Id = newOrderItemComponentId++;

                        foreach(OrderItemComponentComponentModel subComponent in componentModel.Components)
                            subComponent.ParentId = componentModel.Id;
                    }

                    orderFragment.AddOrderItem(newModel);
                    orderFragment.AddOrderItemModel(newModel);
                }
            }
            else
            {
                orderFragment.UnlockOrderItem();
                orderFragment.UpdateOrderItem(model);
            }
        }

        private void VariationsList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (container is CurrentOrderLayout)
            {
                if (positions.Count - 1 > thisPosition)
                {
                    if ((int)e.Id != positions[thisPosition + 1].Id)
                    {
                        orderItemOverriden = true;
                        positions.RemoveRange(thisPosition + 1, positions.Count - thisPosition - 1);

                        components.Clear();
                    }
                    else
                    {
                        ++thisPosition;

                        if (positions[thisPosition].PosType == PositionType.Variations)
                        {
                            if (currentOrderItemId != 0 && positions.Count - 1 == thisPosition && !orderItemOverriden)
                                confirmButton.Visibility = ViewStates.Visible;
                            else
                                confirmButton.Visibility = ViewStates.Invisible;

                            DisplayVariations((int)e.Id);
                        }
                        else
                            DisplayComponents((int)e.Id, true, ComponentListMode.Variation);

                        return;
                    }
                }
                else if(positions.Count - 1 == thisPosition && (int)e.Id != positions[thisPosition].Id)
                {
                    orderItemOverriden = true;
                    confirmButton.Visibility = ViewStates.Invisible;
                }

                FragmentRoute thisRoute = svc.SelectVariation((int)e.Id);

                switch (thisRoute)
                {
                    default:
                        {
                            CurrentOrderLayout thisLayout = (CurrentOrderLayout)container;
                            OrderFragment orderFragment = thisLayout.OrderFragment;

                            OrderItemModel model = GetOrderItemModel((int)e.Id, orderFragment);

                            UpdateOrder(orderFragment, model, false);
                            
                            Reset();

                            break;
                        }
                    case FragmentRoute.Variations:
                        {
                            ++thisPosition;
                            DisplayVariations((int)e.Id);

                            positions.Add(new VariationPosition() { PosType = PositionType.Variations, Id = (int)e.Id });

                            break;
                        }
                    case FragmentRoute.Components:
                        {
                            ++thisPosition;
                            DisplayComponents((int)e.Id, false, ComponentListMode.Variation);

                            positions.Add(new VariationPosition() { PosType = PositionType.Components, Id = (int)e.Id });

                            break;
                        }
                }
            }
        }

        public void CheckBoxes()
        {
            if (currentComponent == null)
            {
                foreach (CheckBoxWithId component in componentsAdapter.CheckBoxes)
                    if (components.ContainsKey(component.ComponentId))
                        component.Check();
            }
            else
            {
                foreach (CheckBoxWithId component in componentsAdapter.CheckBoxes)
                    if (currentComponent.Components.Where(c => c.ComponentId == component.ComponentId).Count() > 0)
                        component.Check();
            }
        }

        private void SetPortions()
        {
            foreach (TextViewWithId textView in componentsAdapter.Portions)
            {
                if (currentComponent == null)
                {
                    if (components.ContainsKey(textView.ComponentId))
                        textView.Text = " " + components[textView.ComponentId].Portions.ToString();
                }
                else
                {
                    if(currentComponent.Components.Where(c => c.ComponentId == textView.ComponentId).Count() > 0)
                        textView.Text = " " + currentComponent.Components.Where(c => c.ComponentId == textView.ComponentId).First().Portions.ToString();
                }
            }
        }

        public int Position
        {
            get
            {
                return thisPosition;
            }
        }

        private void DisplayVariations(int id)
        {
            if(currentOrderItemId == 0 && confirmButton != null)
                confirmButton.Visibility = ViewStates.Invisible;

            variationsList.Adapter = null;
            listAdapter = new VariationListViewAdapter(id);
            variationsList.Adapter = listAdapter;

            Show();
        }

        private void DisplayComponents(int id, bool ignoreDefaults, ComponentListMode mode)
        {
            if (mode == ComponentListMode.Variation)
                variationId = id;
            else
                componentId = id;

            confirmButton.Visibility = ViewStates.Visible;

            variationsList.Adapter = null;
            componentsAdapter = new ComponentListViewAdapter(id, this.Context, mode, ignoreDefaults);
            componentsAdapter.ComponentChecked += ListAdapter_ComponentChecked;
            componentsAdapter.PortionClicked += ComponentsAdapter_PortionClicked;
            componentsAdapter.ArrowClicked += ComponentsAdapter_ArrowClicked;

            foreach (CheckBoxWithId checkBox in componentsAdapter.CheckBoxes)
                if (checkBox.Checked && mode == ComponentListMode.Variation)
                    AddComponent(checkBox.ComponentId, componentsAdapter[checkBox.Position].Portions, componentsAdapter[checkBox.Position].Position);
                else if (checkBox.Checked)
                    AddComponentComponent(checkBox.ComponentId);

            CheckBoxes();
            SetPortions();

            variationsList.Adapter = componentsAdapter;

            backButton.Visibility = ViewStates.Visible;
            forwardButton.Visibility = ViewStates.Invisible;
        }

        private void ComponentsAdapter_ArrowClicked(object sender, Events.ArrowClickedEventArgs e)
        {
            currentComponent = components[e.Arrow.ComponentId];

            DisplayComponents(e.Arrow.ComponentId, true, ComponentListMode.Component);

            if(!editedComponents.Contains(currentComponent))
                editedComponents.Add(currentComponent);

            confirmButton.Visibility = ViewStates.Invisible;
        }

        private void ComponentsAdapter_PortionClicked(object sender, Events.PortionClickedEventArgs e)
        {
            if (popupWindow != null)
                return;

            FrameLayout mainView = new FrameLayout(this.Context);

            ListView popupView = new ListView(this.Context);

            PortionsListViewAdapter adapter = new PortionsListViewAdapter(e.View.ComponentId);
            adapter.PortionSelected += Adapter_PortionSelected;
            popupView.Adapter = adapter;

            mainView.AddView(popupView);

            popupWindow = new PopupWindow(mainView);
            popupWindow.Width = 100;
            popupWindow.Height = 200;

            Resources res = this.Context.Resources;
#pragma warning disable CS0618 // Type or member is obsolete
            Drawable shape = res.GetDrawable(Resource.Drawable.VariationsListBorder);
#pragma warning restore CS0618 // Type or member is obsolete
            popupWindow.SetBackgroundDrawable(shape);

            popupWindow.ShowAsDropDown(e.View, 10, 0);
        }

        private void Adapter_PortionSelected(object sender, Events.PortionSelectedEventArgs e)
        {
            if (currentComponent == null)
                components[e.ComponentId].Portions = e.Number;
            else
                currentComponent.Components.Where(c => c.ComponentId == e.ComponentId).First().Portions = e.Number;

            componentsAdapter.Model.ComponentModels.Where(c => c.Id == e.ComponentId).First().Portions = e.Number;

            SetPortions();

            popupWindow.Dismiss();
            popupWindow = null;
        }

        private void ListAdapter_ComponentChecked(object sender, Events.ComponentCheckedEventArgs e)
        {
            if (currentComponent == null)
            {
                if (components.ContainsKey(e.Model.Id))
                    components.Remove(e.Model.Id);
                else
                    AddComponent(e.Model.Id, e.Model.Portions, e.Model.Position);
            }
            else
            {
                if (currentComponent.Components.Where(c => c.ComponentId == e.Model.Id).Count() > 0)
                    currentComponent.Components.Remove(currentComponent.Components.Where(c => c.ComponentId == e.Model.Id).First());
                else
                    AddComponentComponent(e.Model.Id);
            }
        }

        private void AddComponent(int id, int portions, int position)
        {
            ComponentModel component = svc.GetComponentModel(id);

            OrderItemComponentModel componentModel = null;

            if (currentOrderItemId == 0)
                componentModel = new OrderItemComponentModel(newOrderItemComponentId, newOrderItemId, id, component.Name, component.DisplayName, component.Cost, component.Price, portions, position);
            else
                componentModel = new OrderItemComponentModel(newOrderItemComponentId, currentOrderItemId, id, component.Name, component.DisplayName, component.Cost, component.Price, portions, position);

            if(editedComponents.Where(ec => ec.Id == id).Count() == 0)
                foreach(ComponentModel model in component.Components.Values)
                    if(model.IsDefault)
                        componentModel.AddComponent(new OrderItemComponentComponentModel(newOrderItemComponentId, model.Id, model.Name, model.DisplayName, model.Cost, model.Price, model.Portions, model.IsDefault));

            components.Add(id, componentModel);

            newOrderItemComponentId++;
        }

        private void AddComponentComponent(int componentId)
        {
            ComponentModel model = svc.GetComponentModel(componentId);
            
            currentComponent.AddComponent(new OrderItemComponentComponentModel(currentComponent.Id, model.Id, model.Name, model.DisplayName, model.Cost, model.Price, model.Portions, model.IsDefault));
        }

        public void RehydrateOrderItem(OrderItemModel model)
        {
            currentOrderItemId = model.Id;
            quantityButton.Enabled = false;

            positions.Clear();
            components.Clear();

            variationId = model.VariationId;

            FragmentRoute route = svc.SelectVariation(model.VariationId);

            if (route == FragmentRoute.Components)
            {
                foreach (OrderItemComponentModel component in model.ComponentModels)
                {
                    components.Add(component.ComponentId, component);

                    if (component.HasComponents)
                        editedComponents.Add(component);
                }

                positions.Add(new VariationPosition() { PosType = PositionType.Components, Id = model.VariationId });
            }

            VariationModel newModel = new VariationModel(model.VariationId);
            int parentId = newModel.ParentId;

            while (newModel.ParentId != 0)
            {
                positions.Insert(0, new VariationPosition() { PosType = PositionType.Variations, Id = newModel.ParentId });
                newModel = new VariationModel(newModel.ParentId);
            }

            thisPosition = positions.Count - 1;

            if (route == FragmentRoute.Components)
                DisplayComponents(model.VariationId, true, ComponentListMode.Variation);
            else
            {
                confirmButton.Visibility = ViewStates.Visible;
                DisplayVariations(parentId);
            }

            currentDiscountId = model.DiscountId;

            DiscountModel discountModel = svc.GetDiscountModel(currentDiscountId);
            discountText.Text = discountModel.Description;

            currentInOutId = model.InOutStatus;

            if (currentInOutId == 1)
                inOutText.Text = "Eat In";
            else
                inOutText.Text = "Take Away";

            Show();
        }
    }

    public enum ComponentListMode
    {
        Variation,
        Component
    }
}