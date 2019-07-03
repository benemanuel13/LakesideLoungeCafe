using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;

using LakesideLoungeAdmin.Helpers;
using LakesideLoungeAdmin.Presentation.Adapters;
using LakesideLoungeAdmin.Presentation.EventArgs;
using LakesideLoungeAdmin.Interfaces;

namespace LakesideLoungeAdmin.Presentation.Controls
{
    public class IconView<T> : UserControl where T : IListAble<T>
    {
        private IconViewAdapter<T> adapter = null;
        private Dictionary<int, IconItem<T>> items = new Dictionary<int, IconItem<T>>();

        ScrollViewer viewer = new ScrollViewer();
        Grid view = new Grid();

        IconItem<T> selectedItem;

        public event EventHandler<IconViewRightClickedEventArgs> ViewRightClicked;

        public event EventHandler<ItemCheckedEventArgs<T>> ItemChecked;
        public event EventHandler<ItemClickedEventArgs<T>> ItemClicked;
        public event EventHandler<ItemDoubleClickedEventArgs<T>> ItemDoubleClicked;
        public event EventHandler<ItemRightClickedEventArgs<T>> ItemRightClicked;

        public event EventHandler<SubselectionClickedEventArgs> SubselectionClicked;

        private bool clickable = true;
        private string iconName = "";

        public IconView()
        {
            view.MouseRightButtonDown += IconView_MouseRightButtonDown;
            viewer.MouseRightButtonDown += IconView_MouseRightButtonDown;
            MouseLeftButtonDown += IconView_MouseRightButtonDown;

            viewer.Content = view;
            Content = viewer;
        }

        private void IconView_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ViewRightClicked?.Invoke(this, new IconViewRightClickedEventArgs());
        }

        public void ClearAdapter()
        {
            items.Clear();

            adapter = null;
            viewer.Content = null;
        }

        public IconViewAdapter<T> Adapter
        {
            set
            {
                adapter = value;

                int rows = adapter.Items.Count / 3;

                if (adapter.Items.Count % 3 > 0)
                    ++rows;

                int cols;

                if (adapter.Items.Count >= 3)
                    cols = 3;
                else
                    cols = adapter.Items.Count;

                view = new Grid();
                viewer.Content = view;

                for (int i = 0; i < cols; i++)
                {
                    view.ColumnDefinitions.Add(new ColumnDefinition());
                    view.ColumnDefinitions[i].Width = new System.Windows.GridLength(250);
                }

                for (int i = 0; i < rows; i++)
                    view.RowDefinitions.Add(new RowDefinition());

                int rowCount = 0;
                int colCount = 0;

                foreach (T child in adapter.Items)
                {
                    if (colCount < cols)
                        AddItem(rowCount, colCount++, child);
                    else
                    {
                        colCount = 0;
                        AddItem(++rowCount, colCount++, child);
                    }
                }

                if (rowCount > 6)
                    view.Height = 480 + ((rowCount - 6) * 40);
                else
                    view.Height = 480;
            }
        }

        private void AddItem(int row, int col, T child)
        {
            IconItem<T> item = new IconItem<T>(child, adapter.HasSelections, adapter.Subselectable);

            if(!items.ContainsKey(child.Id))
                items.Add(child.Id, item);

            item.ItemClicked += Item_ItemClicked;
            item.ItemDoubleClicked += Item_ItemDoubleClicked;
            item.ItemChecked += Item_ItemChecked;
            item.ItemRightClicked += Item_ItemRightClicked;
            item.SubselectionClicked += Item_SubselectionClicked;

            item.SetValue(Grid.ColumnProperty, col);
            item.SetValue(Grid.RowProperty, row);

            if (adapter.HasSelections && adapter.SelectedItems != null && adapter.SelectedItems.Where(i => i.Id == child.Id).Count() > 0)
                item.IsChecked = true;
            else
                item.SubselectionEnabled = false;

            if (adapter.HasSelections && adapter.SelectedItems != null && adapter.SelectedItems.Where(i => i.Id == child.Id).Where(i=> i.Subselected == true).Count() > 0)
                item.IsSubselected = true;

            if(iconName != "" && child.ShowIcon)
                item.SetIcon(iconName);

            view.Children.Add(item);
        }

        private void Item_ItemRightClicked(object sender, ItemRightClickedEventArgs<T> e)
        {
            ItemRightClicked?.Invoke(sender, e);
        }

        private void Item_SubselectionClicked(object sender, SubselectionClickedEventArgs e)
        {
            SubselectionClicked(sender, e);
        }

        private void Item_ItemChecked(object sender, ItemCheckedEventArgs<T> e)
        {
            ItemChecked(sender, e);
        }

        public void SelectItem(T model)
        {
            IconItem<T> item = items[model.Id];

            if (selectedItem != null && !selectedItem.Equals(item))
                selectedItem.DeselectItem();

            item.SelectItem();
            selectedItem = item;

            ItemClicked(item, new ItemClickedEventArgs<T>(model));
        }

        public void ShowIcon(int id, string icon)
        {
            IconItem<T> item = items[id];
            item.SetIcon(icon);
        }

        public void HideIcon(int id, string icon)
        {
            IconItem<T> item = items[id];
            item.SetIcon(icon);
        }

        public T IconItemChild(int id)
        {
            IconItem<T> item = items[id];
            return item.Child;
        }

        private void Item_ItemClicked(object sender, ItemClickedEventArgs<T> e)
        {
            //if (!clickable)
            //    return;

            IconItem<T> item = (IconItem<T>)sender;

            if (selectedItem != null && !selectedItem.Equals(item))
                selectedItem.DeselectItem();
            
            item.SelectItem();
            selectedItem = item;

            ItemClicked(sender, e);
        }

        private void Item_ItemDoubleClicked(object sender, ItemDoubleClickedEventArgs<T> e)
        {
            ItemDoubleClicked(sender, e);
        }

        public bool Clickable
        {
            set
            {
                clickable = value;
            }
        }

        public IconItem<T> SelectedItem
        {
            get
            {
                return selectedItem;
            }

            set
            {
                selectedItem = value;
            }
        }

        public void DisableSubSelection(int id)
        {
            items[id].SubselectionEnabled = false;
        }

        public void EnableSubSelection(int id)
        {
            items[id].SubselectionEnabled = true;
        }

        public string IconName
        {
            set
            {
                iconName = value;
            }
        }

        public int CheckCount
        {
            get
            {
                return items.Where(i => i.Value.IsChecked == true).Count();
            }
        }

        public Dictionary<int, IconItem<T>> Items
        {
            get
            {
                return items;
            }
        }
    }
}
