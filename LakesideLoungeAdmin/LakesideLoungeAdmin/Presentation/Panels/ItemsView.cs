using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using LakesideLoungeAdmin.Presentation.Adapters;
using LakesideLoungeAdmin.Presentation.Controls;
using LakesideLoungeAdmin.Presentation.EventArgs;
using LakesideLoungeAdmin.Application;
using LakesideLoungeAdmin.Interfaces;

namespace LakesideLoungeAdmin.Presentation.Panels
{
    public class ItemsView : StackPanel
    {
        private int currentVariation;
        private int parent;

        private TextBlock title;
        private Image upOne = new Image();
        private Button fullView;
        private Button selectionView;
        private StackPanel subViewPanel;
        private IconView<ItemModelBase> iconView = new IconView<ItemModelBase>();
        private DetailsPanel details = new DetailsPanel();
        private TextBlock currentLocation;

        FrameworkElement thisControl;

        ItemsViewService svc = new ItemsViewService();

        bool showingMenu = false;
        bool showPoints = true;
        private bool fullViewSelected = true;

        int selectedId = 0;

        public ItemsView()
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
            viewPanel.Children.Add(details);

            this.Children.Add(viewPanel);

            currentLocation = new TextBlock();
            currentLocation.Text = "Items View: /Root";
            currentLocation.Margin = new Thickness(10, 0, 0, 0);
            currentLocation.FontWeight = FontWeight.FromOpenTypeWeight(700);
            currentLocation.Foreground = new SolidColorBrush(Colors.Blue);

            this.Children.Add(currentLocation);

            details.HidePortions();
            details.DisablePortions();
            details.DisableVAT();
            details.Portions = 0;

            details.HideGroup();

            Populate(new VariationModel(1));
        }

        private void FullView_Click(object sender, RoutedEventArgs e)
        {
            if (fullViewSelected)
                return;

            fullView.Background = new SolidColorBrush(Colors.Beige);
            selectionView.Background = new SolidColorBrush(Colors.LightGray);

            DisplayFullView(parent);

            fullViewSelected = true;
        }

        private void SelectionView_Click(object sender, RoutedEventArgs e)
        {
            if (!fullViewSelected)
                return;

            fullView.Background = new SolidColorBrush(Colors.LightGray);
            selectionView.Background = new SolidColorBrush(Colors.Beige);

            DisplaySelectedView(parent);

            fullViewSelected = false;
        }

        private void DisplayFullView(int id)
        {
            details.HidePointPrice();
            details.ShowPortions();
            details.ShowGroup();
            details.Reset();

            subViewPanel.Visibility = Visibility.Visible;

            Populate(new ComponentsModel(id, id), new ComponentsModel(id), false);
        }

        private void DisplaySelectedView(int id)
        {
            details.HidePointPrice();
            details.ShowPortions();
            details.ShowGroup();
            details.Reset();

            ItemModelBase tempModel = new ComponentsModel(id);
            ((ComponentsModel)tempModel).SortByPosition();

            Populate(tempModel, null, false);
        }

        private void IconView_ViewRightClicked(object sender, IconViewRightClickedEventArgs e)
        {
            if(showingMenu)
            {
                showingMenu = false;
                return;
            }

            FrameworkElement thisControl = (FrameworkElement)sender;
            VariationModel model = new VariationModel(parent);

            if(model.HasChildren && model.Children[0] is VariationModel)
            {
                MenuItem addVariation = new MenuItem();
                addVariation.Header = "Add Variation";
                addVariation.Click += AddVariation_Click;

                thisControl.ContextMenu = new ContextMenu();
                thisControl.ContextMenu.Items.Add(addVariation);
                thisControl.ContextMenu.IsOpen = true;
            }
        }

        private void AddVariation_Click(object sender, RoutedEventArgs e)
        {
            int position = iconView.Items.Count + 1;

            int newId = svc.AddNewVariation(parent, "New Variation", "New Variation", position, 1);
            svc.AddUpdate("ADD_VARIATION," + newId + "," + parent + ",New Variation,New Variation,0,0,0," + position);

            VariationModel model = new VariationModel(parent, false);
            Populate(model);
        }

        private void IconView_ItemRightClicked(object sender, ItemRightClickedEventArgs<ItemModelBase> e)
        {
            thisControl = (FrameworkElement)sender;
            thisControl.ContextMenu = null;

            if (e.Child is VariationModel)
            {
                thisControl.ContextMenu = new ContextMenu();

                VariationModel model = new VariationModel(e.Id);

                if (!model.ShowIcon)
                {
                    MenuItem cutItem = new MenuItem();
                    cutItem.Header = "Cut";
                    cutItem.Icon = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/LakesideLoungeAdmin;component/Resources/CUT.bmp", System.UriKind.Absolute))
                    };
                    cutItem.Click += CutItem_Click;

                    thisControl.ContextMenu.Items.Add(cutItem);

                    MenuItem copyItem = new MenuItem();
                    copyItem.Header = "Copy";
                    copyItem.Icon = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/LakesideLoungeAdmin;component/Resources/COPY.bmp", System.UriKind.Absolute))
                    };
                    copyItem.Click += CopyItem_Click;

                    thisControl.ContextMenu.Items.Add(copyItem);

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

                    bool shownSeperator = false;
                    MenuItem upItem = new MenuItem();
                    MenuItem downItem = new MenuItem();

                    if (model.Position > 1)
                    {
                        Separator sep = new Separator();

                        shownSeperator = true;
                        thisControl.ContextMenu.Items.Add(sep);

                        upItem.Header = "Move Variation Up";
                        upItem.Icon = new Image
                        {
                            Source = new BitmapImage(new Uri("pack://application:,,,/LakesideLoungeAdmin;component/Resources/COMPONENT_UP.bmp", System.UriKind.Absolute))
                        };
                        upItem.Click += UpItem_Click;

                        thisControl.ContextMenu.Items.Add(upItem);
                    }

                    if(model.Position < iconView.Items.Count)
                    {
                        if(!shownSeperator)
                        {
                            Separator sep = new Separator();

                            shownSeperator = true;
                            thisControl.ContextMenu.Items.Add(sep);
                        }

                        downItem.Header = "Move Variation Down";
                        downItem.Icon = new Image
                        {
                            Source = new BitmapImage(new Uri("pack://application:,,,/LakesideLoungeAdmin;component/Resources/COMPONENT_DOWN.bmp", System.UriKind.Absolute))
                        };
                        downItem.Click += DownItem_Click;

                        thisControl.ContextMenu.Items.Add(downItem);
                    }
                }
                else
                {
                    MenuItem reinstateItem = new MenuItem();
                    reinstateItem.Header = "Reinstate";
                    reinstateItem.Icon = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/LakesideLoungeAdmin;component/Resources/CUT.bmp", System.UriKind.Absolute))
                    };
                    reinstateItem.Click += ReinstateItem_Click;

                    thisControl.ContextMenu.Items.Add(reinstateItem);
                }
                
                if(!model.HasChildren)
                {
                    if(thisControl.ContextMenu == null)
                        thisControl.ContextMenu = new ContextMenu();

                    Separator separator = new Separator();
                    thisControl.ContextMenu.Items.Add(separator);

                    MenuItem addVariationsItem = new MenuItem();
                    addVariationsItem.Header = "Add Layer Of Variations";
                    addVariationsItem.Icon = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/LakesideLoungeAdmin;component/Resources/NEW.bmp", System.UriKind.Absolute))
                    };
                    addVariationsItem.Click += AddVariationsItem_Click;

                    thisControl.ContextMenu.Items.Add(addVariationsItem);

                    MenuItem addComponentsItem = new MenuItem();
                    addComponentsItem.Header = "Add Layer Of Components";
                    addComponentsItem.Icon = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/LakesideLoungeAdmin;component/Resources/NEW.bmp", System.UriKind.Absolute))
                    };
                    addComponentsItem.Click += AddComponentsItem_Click;

                    thisControl.ContextMenu.Items.Add(addComponentsItem);
                }
            }
            else
            {
                if(!fullViewSelected && iconView.Items.Count > 1)
                {
                    thisControl.ContextMenu = new ContextMenu();

                    ComponentModel thisItem = (ComponentModel)iconView.SelectedItem.Child;

                    if(thisItem.Position > 1)
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

                    if(thisItem.Position < iconView.Items.Count)
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

            showingMenu = true;
            thisControl.ContextMenu.IsOpen = true;
        }
        
        private void UpItem_Click(object sender, RoutedEventArgs e)
        {
            VariationModel movingModel = (VariationModel)iconView.SelectedItem.Child;
            VariationModel otherModel = (VariationModel)iconView.Items.Values.Where(i => i.Child.Position == movingModel.Position - 1).FirstOrDefault().Child;

            svc.SwapVariationPositions(movingModel, otherModel);

            VariationModel model = new VariationModel(parent, false);
            Populate(model);
        }

        private void DownItem_Click(object sender, RoutedEventArgs e)
        {
            VariationModel movingModel = (VariationModel)iconView.SelectedItem.Child;
            VariationModel otherModel = (VariationModel)iconView.Items.Values.Where(i => i.Child.Position == movingModel.Position + 1).FirstOrDefault().Child;

            svc.SwapVariationPositions(movingModel, otherModel);

            VariationModel model = new VariationModel(parent, false);
            Populate(model);
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

        private void AddComponentsItem_Click(object sender, RoutedEventArgs e)
        {
            Populate(new ComponentsModel(selectedId, parent), new ComponentsModel(selectedId));

            VariationModel model = new VariationModel(parent, false);
            title.Text = model.Description;

            details.HidePointPrice();
            upOne.Visibility = Visibility.Visible;
            subViewPanel.Visibility = Visibility.Visible;
        }

        private void AddVariationsItem_Click(object sender, RoutedEventArgs e)
        {
            int newId = svc.AddNewVariation(selectedId, "New Variation", "New Variation", 1, 1);
            svc.AddUpdate("ADD_VARIATION," + newId + "," + selectedId + ",New Variation,New Variation,0,0,0,1");

            VariationModel model = new VariationModel(selectedId, false);
            Populate(model);

            title.Text = model.Description;
            currentLocation.Text = currentLocation.Text + "/" + model.Description;
            upOne.Visibility = Visibility.Visible;
        }

        private void ReinstateItem_Click(object sender, RoutedEventArgs e)
        {
            svc.ReinstateItem(selectedId);
            iconView.IconItemChild(selectedId).ShowIcon = false;
            iconView.HideIcon(selectedId, "REINSTATED_SELECTED.BMP");

            VariationModel model = new VariationModel(selectedId);
            svc.AddUpdate("REINSTATE_VARIATION," + selectedId);
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            svc.RemoveItem(selectedId);
            iconView.IconItemChild(selectedId).ShowIcon = true;
            iconView.ShowIcon(selectedId, "REMOVED_SELECTED.BMP");

            svc.AddUpdate("REMOVE_VARIATION," + selectedId);
        }

        private void CopyItem_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CutItem_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you absolutely sure you wish to delete this Variation?", "Are you sure?", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                DeleteVariation();

                VariationModel model = new VariationModel(parent, false);

                if (!model.HasChildren && parent != 1)
                    GoUpLevel();
                else
                    Populate(model);
            }
        }

        private void DeleteVariation()
        {
            VariationModel model = new VariationModel(selectedId, true);
            svc.DeleteVariation(model);
        }

        private void IconView_SubselectionClicked(object sender, SubselectionClickedEventArgs e)
        {
            if (e.IsChecked)
                svc.SetComponentDefault(parent, e.Id);
            else
                svc.SetComponentUnDefault(parent, e.Id);
        }

        private void IconView_ItemChecked(object sender, ItemCheckedEventArgs<ItemModelBase> e)
        {
            if (e.IsChecked)
            {
                iconView.EnableSubSelection(e.Id);
                svc.SetComponentSelected(parent, e.Id, iconView.CheckCount);

                details.EnablePoints();
                details.EnablePortions();
                details.EnableGroup();
                //SelectItem(e.Child, true);
                iconView.SelectItem(e.Child);
                iconView.SelectedItem.Child.Position = iconView.CheckCount;
            }
            else
            {
                iconView.DisableSubSelection(e.Id);
                svc.SetComponentDeselected(parent, e.Id, e.Child.Position);

                IEnumerable<IconItem<ItemModelBase>> items = iconView.Items.Values.Where(i => i.Child.Position > e.Child.Position);

                foreach(IconItem<ItemModelBase> item in items)
                    item.Child.Position--;

                details.DisablePoints();
                details.DisablePortions();
                details.DisableGroup();
            }
        }

        private void Details_ItemUpdated(object sender, ItemUpdatedEventArgs e)
        {
            iconView.SelectedItem.Text = e.Text;
        }

        private void IconView_ItemClicked(object sender, ItemClickedEventArgs<ItemModelBase> e)
        {
            SelectItem(e.Child);
        }

        private void SelectItem(ItemModelBase child)
        {
            int parentType;
            selectedId = child.Id;

            ItemModelBase model;

            if (child is VariationModel)
            {
                details.Title = "Variation";
                parentType = 0;
                model = new VariationModel(child.Id);
                showPoints = true;
            }
            else
            {
                details.Title = "Component";
                parentType = 1;

                bool found = false;
                model = svc.GetComponentModel(parent, child.Id, out found);

                if (found)
                {
                    details.EnablePoints();
                    details.EnableGroup();
                    details.EnablePortions();

                    details.Points = ((ComponentModel)model).Points;
                    details.Portions = ((ComponentModel)model).Portions;
                    details.Group = ((ComponentModel)model).Group;

                    details.PointPrice = 0;
                    showPoints = true;
                }
                else
                {
                    details.DisablePoints();
                    details.DisableGroup();
                    details.Group = 0;
                    details.DisablePortions();
                    details.Portions = 0;

                    showPoints = false;
                }
            }

            details.ItemName = model.Description;
            details.DisplayName = model.DisplayName;
            details.Price = model.Price;

            if (model is VariationModel)
            {
                VariationModel thisModel = (VariationModel)model;
                details.Points = thisModel.Points;
                details.PointPrice = thisModel.PointPrice;
                details.EnablePoints();
                details.VAT = thisModel.VATStatus;
                details.EnableVAT();
                showPoints = true;
            }

            details.Set(child.Id, parentType, showPoints);
            details.ParentId = parent;
        }

        private void IconView_ItemDoubleClicked(object sender, ItemDoubleClickedEventArgs<ItemModelBase> e)
        {
            ItemModelBase model = null;

            if (e.Child is VariationModel)
                model = new VariationModel(e.Child.Id);
            else if (e.Child is ComponentModel)
            {
                details.Reset();
                return;
            }

            if (model.HasChildren && model.Children[0] is VariationModel)
            {
                details.ShowPointPrice();
                details.HidePortions();
                details.HideGroup();

                Populate(model);
                title.Text = e.Child.Description;
                currentLocation.Text = currentLocation.Text + "/" + e.Child.Description;
            }
            else if (model.HasChildren)
            {
                details.HidePointPrice();
                details.ShowPortions();
                details.ShowGroup();

                subViewPanel.Visibility = Visibility.Visible;

                Populate(new ComponentsModel(e.Child.Id, e.Child.ParentId), new ComponentsModel(e.Child.Id));
                title.Text = e.Child.Description;
                currentLocation.Text = currentLocation.Text + "/" + e.Child.Description;
            }

            if (currentVariation != 0)
                upOne.Visibility = Visibility.Visible;            

            details.Reset();
        }

        private void Populate(ItemModelBase model, ItemModelBase selectionModel = null, bool setParent = true)
        {
            selectedId = 0;

            if (setParent)
            {
                currentVariation = model.ParentId;

                if (selectionModel != null)
                    parent = selectionModel.Id;
                else
                {
                    parent = model.Id;
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

            iconView.Adapter = adapter;
        }

        private void UpOne_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GoUpLevel();
        }

        private void GoUpLevel()
        {
            VariationModel model = new VariationModel(currentVariation);
            title.Text = model.Description;

            Populate(model);

            if (currentVariation == 0)
                upOne.Visibility = Visibility.Hidden;

            subViewPanel.Visibility = Visibility.Hidden;

            details.ShowPointPrice();
            details.HidePortions();
            details.HideGroup();

            details.Reset();

            fullViewSelected = true;
            fullView.Background = new SolidColorBrush(Colors.Beige);
            selectionView.Background = new SolidColorBrush(Colors.LightGray);

            SetLocationUpOne();
        }

        private void SetLocationUpOne()
        {
            string text = currentLocation.Text;
            int pos = text.Length - 1;

            while (text.Substring(pos, 1) != "/")
                --pos;

            text = text.Substring(0, pos);
            currentLocation.Text = text;
        }
    }
}
