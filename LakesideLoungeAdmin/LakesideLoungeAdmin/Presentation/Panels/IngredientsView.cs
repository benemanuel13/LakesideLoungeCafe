using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using LakesideLoungeAdmin.Application;
using LakesideLoungeAdmin.Presentation.Adapters;
using LakesideLoungeAdmin.Presentation.Controls;
using LakesideLoungeAdmin.Presentation.EventArgs;

namespace LakesideLoungeAdmin.Presentation.Panels
{
    public class IngredientsView : StackPanel
    {
        private IngredientsViewService svc = new IngredientsViewService();
        private IconView<IngredientsModelBase> iconView = new IconView<IngredientsModelBase>();
        private IngredientsDetailsPanel details = new IngredientsDetailsPanel();

        bool showingMenu = false;

        public IngredientsView()
        {
            Orientation = Orientation.Horizontal;

            iconView.BorderBrush = new SolidColorBrush(Colors.Black);
            iconView.BorderThickness = new Thickness(3);
            iconView.Margin = new Thickness(10, 5, 0, 0);
            iconView.Padding = new Thickness(5);
            iconView.Height = 400;
            iconView.Width = 800;
            iconView.ItemClicked += IconView_ItemClicked;
            iconView.ItemDoubleClicked += IconView_ItemDoubleClicked;
            iconView.ItemRightClicked += IconView_ItemRightClicked;
            iconView.ViewRightClicked += IconView_ViewRightClicked;
            this.Children.Add(iconView);
            
            details.ItemUpdated += Details_ItemUpdated;
            this.Children.Add(details);

            Populate(new IngredientsModel());
        }

        private void IconView_ItemRightClicked(object sender, ItemRightClickedEventArgs<IngredientsModelBase> e)
        {
            showingMenu = true;

            FrameworkElement thisControl = (FrameworkElement)sender;
            thisControl.ContextMenu = new ContextMenu();

            MenuItem deleteIngredient = new MenuItem();
            deleteIngredient.Header = "Delete Ingredient";
            deleteIngredient.Click += DeleteIngredient_Click;
            thisControl.ContextMenu.Items.Add(deleteIngredient);

            thisControl.ContextMenu.IsOpen = true;
        }

        private void DeleteIngredient_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void IconView_ViewRightClicked(object sender, IconViewRightClickedEventArgs e)
        {
            if (showingMenu)
            {
                showingMenu = false;
                return;
            }

            FrameworkElement thisControl = (FrameworkElement)sender;
            thisControl.ContextMenu = new ContextMenu();

            MenuItem addIngredient = new MenuItem();
            addIngredient.Header = "Add Ingredient";
            addIngredient.Click += AddIngredient_Click;
            thisControl.ContextMenu.Items.Add(addIngredient);

            thisControl.ContextMenu.IsOpen = true;
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            svc.AddIngredient();
            Populate(new IngredientsModel());
        }

        private void Details_ItemUpdated(object sender, ItemUpdatedEventArgs e)
        {
            iconView.SelectedItem.Text = e.Text;
        }

        private void IconView_ItemClicked(object sender, EventArgs.ItemClickedEventArgs<IngredientsModelBase> e)
        {
            IngredientModel model = new IngredientModel(e.Child.Id);
            details.ItemName = model.Description;
            details.DisplayName = model.DisplayName;
            details.PortionSize = model.PortionSize;
            details.PortionType = model.PortionType;
            details.Set(e.Child.Id);
        }

        private void IconView_ItemDoubleClicked(object sender, EventArgs.ItemDoubleClickedEventArgs<IngredientsModelBase> e)
        {
            details.Reset();
        }

        private void Populate(IngredientsModel model)
        {
            iconView.ClearAdapter();
            IconViewAdapter<IngredientsModelBase> adapter = new IconViewAdapter<IngredientsModelBase>(model, null);
            iconView.Adapter = adapter;
        }
    }
}
