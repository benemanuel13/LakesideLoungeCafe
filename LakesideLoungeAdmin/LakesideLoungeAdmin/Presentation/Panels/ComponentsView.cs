using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using LakesideLoungeAdmin.Presentation.Adapters;
using LakesideLoungeAdmin.Presentation.Controls;
using LakesideLoungeAdmin.Presentation.EventArgs;
using LakesideLoungeAdmin.Application;
using LakesideLoungeAdmin.Interfaces;

namespace LakesideLoungeAdmin.Presentation.Panels
{
    public class ComponentsView : StackPanel
    {
        private TextBlock title;
        private Image upOne = new Image();
        private Button fullView;
        private Button selectionView;
        private StackPanel subViewPanel;
        private IconView<ItemModelBase> iconView = new IconView<ItemModelBase>();
        private DetailsPanel details = new DetailsPanel();
        private int parent;
        private int selectedId = 0;
        private bool fullViewSelected = true;

        private ComponentsViewService svc = new ComponentsViewService();

        int level = 1;
        bool showingMenu = false;

        public ComponentsView()
        {
            Orientation = Orientation.Vertical;

            StackPanel titleBar = new StackPanel();
            titleBar.Orientation = Orientation.Horizontal;

            StackPanel upPanel = new StackPanel();
            upPanel.Orientation = Orientation.Horizontal;
            upPanel.Width = 600;

            upOne.Source = new BitmapImage(new Uri("/LakesideLoungeAdmin;component/Resources/OneUp.png", System.UriKind.Relative));
            upOne.Width = 28;
            upOne.Height = 28;
            upOne.Margin = new Thickness(10, 10, 0, 0);
            upOne.Visibility = Visibility.Hidden;
            upOne.MouseDown += UpOne_MouseDown;
            upOne.HorizontalAlignment = HorizontalAlignment.Left;
            upPanel.Children.Add(upOne);

            title = new TextBlock();
            title.Text = "Root";
            title.FontSize = 25;
            title.Margin = new Thickness(10, 10, 0, 0);
            upPanel.Children.Add(title);

            titleBar.Children.Add(upPanel);

            subViewPanel = new StackPanel();
            subViewPanel.Orientation = Orientation.Horizontal;
            subViewPanel.HorizontalAlignment = HorizontalAlignment.Right;

            fullView = new Button();
            fullView.Content = "Full View";
            fullView.FontSize = 15;
            fullView.Margin = new Thickness(10, 10, 0, 0);
            fullView.Padding = new Thickness(5, 5, 5, 5);
            fullView.Click += FullView_Click;
            subViewPanel.Children.Add(fullView);

            selectionView = new Button();
            selectionView.Content = "Selection View";
            selectionView.FontSize = 15;
            selectionView.Margin = new Thickness(10, 10, 0, 0);
            selectionView.Padding = new Thickness(5, 5, 5, 5);
            selectionView.Click += SelectionView_Click;
            subViewPanel.Children.Add(selectionView);

            fullView.Background = new SolidColorBrush(Colors.Beige);
            selectionView.Background = new SolidColorBrush(Colors.LightGray);

            subViewPanel.Visibility = Visibility.Hidden;

            titleBar.Children.Add(subViewPanel);

            this.Children.Add(titleBar);

            StackPanel viewPanel = new StackPanel();
            viewPanel.Orientation = Orientation.Horizontal;

            iconView.BorderBrush = new SolidColorBrush(Colors.Black);
            iconView.BorderThickness = new Thickness(3);
            iconView.Margin = new Thickness(10, 5, 0, 0);
            iconView.Padding = new Thickness(5);
            iconView.Height = 500;
            iconView.Width = 800;
            iconView.ItemChecked += IconView_ItemChecked;
            iconView.ItemClicked += IconView_ItemClicked;
            iconView.ItemDoubleClicked += IconView_ItemDoubleClicked;
            iconView.ItemRightClicked += IconView_ItemRightClicked;
            iconView.SubselectionClicked += IconView_SubselectionClicked;
            iconView.ViewRightClicked += IconView_ViewRightClicked;
            viewPanel.Children.Add(iconView);

            details.ItemUpdated += Details_ItemUpdated;
            details.Level = 1;
            details.ParentType = 1;
            viewPanel.Children.Add(details);

            Children.Add(viewPanel);

            Populate(new ComponentsModel());

            details.HidePointPrice();
            details.HidePortions();
            details.ShowGroup();
            details.DisablePortions();
            details.DisableGroup();
        }
        
        private void FullView_Click(object sender, RoutedEventArgs e)
        {
            if (fullViewSelected)
                return;

            fullView.Background = new SolidColorBrush(Colors.Beige);
            selectionView.Background = new SolidColorBrush(Colors.LightGray);

            fullViewSelected = true;

            DisplayFullView(parent, false);
        }

        private void SelectionView_Click(object sender, RoutedEventArgs e)
        {
            if (!fullViewSelected)
                return;

            fullView.Background = new SolidColorBrush(Colors.LightGray);
            selectionView.Background = new SolidColorBrush(Colors.Beige);

            fullViewSelected = false;

            DisplaySelectedView(parent);
        }

        private void IconView_ItemRightClicked(object sender, ItemRightClickedEventArgs<ItemModelBase> e)
        {
            if(level == 1)
            {
                if (!e.Child.ShowIcon)
                {
                    FrameworkElement thisControl = (FrameworkElement)sender;
                    thisControl.ContextMenu = new ContextMenu();

                    MenuItem removeItem = new MenuItem();
                    removeItem.Header = "Remove";
                    removeItem.Icon = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/LakesideLoungeAdmin;component/Resources/REMOVE.bmp", System.UriKind.Absolute))
                    };
                    removeItem.Click += RemoveItem_Click;

                    thisControl.ContextMenu.Items.Add(removeItem);

                    MenuItem deleteItem = new MenuItem();
                    deleteItem.Header = "Delete";
                    deleteItem.Icon = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/LakesideLoungeAdmin;component/Resources/DELETE.bmp", System.UriKind.Absolute))
                    };
                    deleteItem.Click += DeleteItem_Click;

                    thisControl.ContextMenu.Items.Add(deleteItem);

                    showingMenu = true;

                    thisControl.ContextMenu.IsOpen = true;
                }
                else
                {
                    FrameworkElement thisControl = (FrameworkElement)sender;
                    thisControl.ContextMenu = new ContextMenu();

                    MenuItem reinstateItem = new MenuItem();
                    reinstateItem.Header = "Reinstate";
                    reinstateItem.Icon = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/LakesideLoungeAdmin;component/Resources/CUT.bmp", System.UriKind.Absolute))
                    };
                    reinstateItem.Click += ReinstateItem_Click;

                    showingMenu = true;

                    thisControl.ContextMenu.Items.Add(reinstateItem);
                    thisControl.ContextMenu.IsOpen = true;
                }
            }
            else
            {
                if (!fullViewSelected && iconView.Items.Count > 1)
                {
                    FrameworkElement thisControl = (FrameworkElement)sender;
                    thisControl.ContextMenu = new ContextMenu();

                    ComponentModel thisItem = (ComponentModel)iconView.SelectedItem.Child;

                    if (thisItem.Position > 1)
                    {
                        MenuItem upComponentsItem = new MenuItem();
                        upComponentsItem.Header = "Move Component Up";
                        upComponentsItem.Icon = new Image
                        {
                            Source = new BitmapImage(new Uri("pack://application:,,,/LakesideLoungeAdmin;component/Resources/COMPONENT_UP.bmp", System.UriKind.Absolute))
                        };
                        upComponentsItem.Click += UpComponentsItem_Click;

                        thisControl.ContextMenu.Items.Add(upComponentsItem);
                    }

                    if (thisItem.Position < iconView.Items.Count)
                    {
                        MenuItem downComponentsItem = new MenuItem();
                        downComponentsItem.Header = "Move Component Down";
                        downComponentsItem.Icon = new Image
                        {
                            Source = new BitmapImage(new Uri("pack://application:,,,/LakesideLoungeAdmin;component/Resources/COMPONENT_DOWN.bmp", System.UriKind.Absolute))
                        };
                        downComponentsItem.Click += DownComponentsItem_Click;

                        thisControl.ContextMenu.Items.Add(downComponentsItem);
                    }
                }
                else
                    return;
            }
        }

        private void UpComponentsItem_Click(object sender, RoutedEventArgs e)
        {
            ComponentModel movingModel = (ComponentModel)iconView.SelectedItem.Child;
            ComponentModel otherModel = (ComponentModel)iconView.Items.Values.Where(i => i.Child.Position == movingModel.Position - 1).FirstOrDefault().Child;

            svc.SwapComponentPositions(parent, movingModel, otherModel);

            DisplaySelectedView(parent);
        }

        private void DownComponentsItem_Click(object sender, RoutedEventArgs e)
        {
            ComponentModel movingModel = (ComponentModel)iconView.SelectedItem.Child;
            ComponentModel otherModel = (ComponentModel)iconView.Items.Values.Where(i => i.Child.Position == movingModel.Position + 1).FirstOrDefault().Child;

            svc.SwapComponentPositions(parent, movingModel, otherModel);

            DisplaySelectedView(parent);
        }

        private void ReinstateItem_Click(object sender, RoutedEventArgs e)
        {
            svc.ReinstateItem(selectedId);
            iconView.IconItemChild(selectedId).ShowIcon = false;
            iconView.HideIcon(selectedId, "REINSTATED_SELECTED.BMP");
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            svc.RemoveItem(selectedId);
            iconView.IconItemChild(selectedId).ShowIcon = true;
            iconView.ShowIcon(selectedId, "REMOVED_SELECTED.BMP");
        }

        private void IconView_ViewRightClicked(object sender, IconViewRightClickedEventArgs e)
        {
            if (showingMenu)
            {
                showingMenu = false;
                return;
            }

            if (level == 1)
            {
                FrameworkElement thisControl = (FrameworkElement)sender;
                thisControl.ContextMenu = new ContextMenu();

                MenuItem addComponent = new MenuItem();
                
                addComponent.Header = "Add Component";
                addComponent.Click += AddComponent_Click;
                thisControl.ContextMenu.Items.Add(addComponent);

                thisControl.ContextMenu.IsOpen = true;
            }
        }

        private void AddComponent_Click(object sender, RoutedEventArgs e)
        {
            int newId = svc.AddNewComponent("(New Component)", "New Component");
            svc.AddUpdate("ADD_COMPONENT," + newId + "," + "(New Component)" + "," + "New Component");

            Populate(new ComponentsModel());
        }

        private void IconView_ItemChecked(object sender, ItemCheckedEventArgs<ItemModelBase> e)
        {
            if (e.IsChecked)
            {
                iconView.EnableSubSelection(e.Id);
                svc.SetComponentSelected(parent, e.Id, iconView.CheckCount);

                if(level == 1)
                    details.DisableGroup();
                else
                    details.EnableGroup();

                iconView.SelectedItem.Child.Position = iconView.CheckCount;
            }
            else
            {
                iconView.DisableSubSelection(e.Id);
                svc.SetComponentDeselected(parent, e.Id, e.Child.Position);

                IEnumerable<IconItem<ItemModelBase>> items = iconView.Items.Values.Where(i => i.Child.Position > e.Child.Position);

                foreach (IconItem<ItemModelBase> item in items)
                    item.Child.Position--;

                details.DisableGroup();
            }
        }

        private void IconView_SubselectionClicked(object sender, SubselectionClickedEventArgs e)
        {
            if (e.IsChecked)
                svc.SetComponentDefault(parent, e.Id);
            else
                svc.SetComponentUnDefault(parent, e.Id);
        }
        
        private void UpOne_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Populate(new ComponentsModel());
            upOne.Visibility = Visibility.Hidden;
            title.Text = "Root";

            details.DisableGroup();
            //details.EnablePortions();

            details.Reset();
            details.Level = 1;
            level = 1;

            subViewPanel.Visibility = Visibility.Hidden;
            fullViewSelected = true;

            fullView.Background = new SolidColorBrush(Colors.Beige);
            selectionView.Background = new SolidColorBrush(Colors.LightGray);
        }

        private void IconView_ItemClicked(object sender, ItemClickedEventArgs<ItemModelBase> e)
        {
            selectedId = e.Child.Id;

            ItemModelBase model;
            
            details.Title = "Component";
            model = new ComponentModel(e.Child.ParentId, e.Child.Id);
            
            details.ItemName = model.Description;
            details.DisplayName = model.DisplayName;
            details.Price = model.Price;

            details.Set(e.Child.Id, 1, false);

            details.ParentId = parent;

            IconItem<ItemModelBase> icon = (IconItem<ItemModelBase>)sender;

            if (level == 1)
                ;
            else
            {
                if (fullViewSelected && icon.IsChecked)
                {
                    details.EnableGroup();
                    ComponentModel subModel = new ComponentModel(parent, e.Child.Id, true);
                    details.Group = subModel.Group;
                }
                else if(fullViewSelected && !icon.IsChecked)
                {
                    details.DisableGroup();
                    details.Group = 0;
                }
                else if (!fullViewSelected)
                {
                    details.EnableGroup();
                    ComponentModel subModel = new ComponentModel(parent, e.Child.Id, true);
                    details.Group = subModel.Group;
                }
            }
        }

        private void IconView_ItemDoubleClicked(object sender, ItemDoubleClickedEventArgs<ItemModelBase> e)
        {
            details.Reset();

            if (level == 2)
                return;

            details.Level = 2;
            level++;

            DisplayFullView(e.Child.Id, true);
        }

        private void DisplayFullView(int id, bool setParent)
        {
            subViewPanel.Visibility = Visibility.Visible;
            fullView.Background = new SolidColorBrush(Colors.Beige);
            selectionView.Background = new SolidColorBrush(Colors.LightGray);
            fullViewSelected = true;

            ItemModelBase model = new ComponentsModel(id, true);
            ((ComponentsModel)model).SortByName();

            if (model.HasChildren)
            {
                ItemModelBase tempModel = new ComponentModel(0, id);

                title.Text = tempModel.DisplayName;
                upOne.Visibility = Visibility.Visible;
                Populate(model, new ComponentModel(id, id), setParent);
            }
        }

        private void DisplaySelectedView(int id)
        {
            ItemModelBase model = new ComponentsModel(id, true);

            if (model.HasChildren)
            {
                ItemModelBase tempModel = new ComponentModel(0, id);
                ((ComponentModel)tempModel).SortByPosition();

                title.Text = tempModel.DisplayName;
                upOne.Visibility = Visibility.Visible;
                Populate(tempModel, null, false);
            }
        }

        private void Details_ItemUpdated(object sender, ItemUpdatedEventArgs e)
        {
            iconView.SelectedItem.Text = e.Text;
        }

        private void Populate(ItemModelBase model, ItemModelBase selectionModel = null, bool setParent = true)
        {
            selectedId = 0;

            if (setParent)
            {
                if (selectionModel != null)
                    parent = selectionModel.Id;
                else
                {
                    parent = model.Id;

                    if (level == 1)
                        iconView.IconName = "REMOVED.BMP";
                }
            }

            iconView.ClearAdapter();
            IconViewAdapter<ItemModelBase> adapter = new IconViewAdapter<ItemModelBase>(model, selectionModel);

            if (selectionModel != null)
            {
                adapter.HasSelections = true;
                adapter.Subselectable = true;
                iconView.Clickable = false;
            }
            else
                iconView.Clickable = true;

            details.DisableGroup();
            details.DisablePortions();

            iconView.Adapter = adapter;
        }
    }
}
