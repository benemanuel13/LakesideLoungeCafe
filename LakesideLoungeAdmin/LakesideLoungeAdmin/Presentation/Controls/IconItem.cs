using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Drawing;

using LakesideLoungeAdmin.Application;
using LakesideLoungeAdmin.Helpers;
using LakesideLoungeAdmin.Interfaces;
using LakesideLoungeAdmin.Presentation.EventArgs;

using System.Net.Cache;

namespace LakesideLoungeAdmin.Presentation.Controls
{
    public class IconItem<T> : UserControl where T : IListAble<T>
    {
        T item;

        private StackPanel panel = new StackPanel();
        private StackPanel selectionPanel;
        StackPanel iconView;
        private TextBlock text = new TextBlock();
        private CheckBox box;
        private CheckBox subSelection;
        private System.Windows.Controls.Image icon;
        bool selected;

        bool clickingSubselection = false;

        public event EventHandler<ItemCheckedEventArgs<T>> ItemChecked;
        public event EventHandler<ItemClickedEventArgs<T>> ItemClicked;
        public event EventHandler<ItemDoubleClickedEventArgs<T>> ItemDoubleClicked;
        public event EventHandler<ItemRightClickedEventArgs<T>> ItemRightClicked;

        public event EventHandler<SubselectionClickedEventArgs> SubselectionClicked;

        public IconItem(T child, bool selectable, bool subSelectable)
        {
            item = child;

            text.Text = Helper.StripSpaces(child.Description);
            text.Background = new SolidColorBrush(Colors.Beige);
            text.Height = 30;
            text.FontSize = 15;
            text.Padding = new System.Windows.Thickness(10, 0, 0, 0);
            text.MouseDown += Text_MouseDown;
            text.MouseRightButtonDown += Panel_MouseRightButtonDown;

            if (selectable)
            {
                selectionPanel = new StackPanel();
                selectionPanel.Orientation = Orientation.Horizontal;
                selectionPanel.Background = new SolidColorBrush(Colors.Beige);

                selectionPanel.Children.Add(text);
                selectionPanel.Width = 200;

                box = new CheckBox();
                box.Content = selectionPanel;
                box.Background = new SolidColorBrush(Colors.Beige);
                box.Height = 30;
                box.FontSize = 15;
                box.Padding = new System.Windows.Thickness(10, 0, 0, 0);
                box.Click += Box_Click;

                panel.Children.Add(box);

                if (subSelectable)
                {
                    subSelection = new CheckBox();
                    subSelection.Background = new SolidColorBrush(Colors.Beige);
                    subSelection.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    subSelection.FontSize = 12;
                    subSelection.Foreground = new SolidColorBrush(Colors.Red);
                    subSelection.Content = "default";
                    subSelection.Margin = new System.Windows.Thickness(0, 13, 0, 0);
                    subSelection.Click += SubSelection_Click;

                    text.Width = 140;

                    selectionPanel.Children.Add(subSelection);
                }
                else
                    text.Width = 200;
            }
            else
            {
                //panel.Children.Add(text);

                iconView = new StackPanel();
                iconView.Orientation = Orientation.Horizontal;
                iconView.Background = new SolidColorBrush(Colors.Beige);

                text.Width = 200;
                iconView.Children.Add(text);

                icon = new System.Windows.Controls.Image();

                icon.MouseDown += Text_MouseDown;
                icon.MouseRightButtonDown += Panel_MouseRightButtonDown;

                iconView.Children.Add(icon);
                
                panel.Children.Add(iconView);
            }

            Padding = new System.Windows.Thickness(10, 10, 10, 0);

            panel.MouseDown += Text_MouseDown;
            panel.MouseRightButtonDown += Panel_MouseRightButtonDown;

            Content = panel;
        }

        public void SetIcon(string image)
        {
            BitmapImage newImage = new BitmapImage(new Uri("pack://application:,,,/LakesideLoungeAdmin;component/Resources/" + image, System.UriKind.Absolute));

            if(icon != null)
                icon.Source = newImage;
        }

        private void Panel_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (selected)
                ItemRightClicked?.Invoke(this, new ItemRightClickedEventArgs<T>(item));
        }

        private void SubSelection_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            clickingSubselection = true;
            SubselectionClicked(this, new SubselectionClickedEventArgs(item.Id, item.ParentId, (bool)subSelection.IsChecked));
        }

        private void Box_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(clickingSubselection)
            {
                clickingSubselection = false;
                return;
            }

            if (subSelection != null)
                subSelection.IsChecked = false;

            if ((bool)box.IsChecked && subSelection != null)
                subSelection.IsEnabled = true;
            else
                subSelection.IsEnabled = false;

            ItemChecked(sender, new ItemCheckedEventArgs<T>(item.Id, item.ParentId, (bool)box.IsChecked, item));
        }

        private void Text_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                if (selected)
                {
                    DeselectItem();
                    ItemDoubleClicked(this, new ItemDoubleClickedEventArgs<T>(item));
                    e.Handled = true;
                }
                else
                {
                    selected = true;
                    ItemClicked(this, new ItemClickedEventArgs<T>(item));
                    e.Handled = true;
                }
            }
            else
            {
                
            }
        }

        public void SelectItem()
        {
            if (iconView != null)
            {
                iconView.Background = new SolidColorBrush(Colors.Black);

                if (item.ShowIcon)
                    SetIcon("REMOVED_SELECTED.BMP");
                else
                    SetIcon("BLANK_SELECTED.BMP");
            }

            text.Background = new SolidColorBrush(Colors.Black);
            text.Foreground = new SolidColorBrush(Colors.White);

            if (selectionPanel != null)
                selectionPanel.Background = new SolidColorBrush(Colors.Black);

            if (subSelection != null)
            {
                subSelection.Background = new SolidColorBrush(Colors.Black);
                subSelection.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        public void DeselectItem()
        {
            selected = false;

            if (iconView != null)
            {
                iconView.Background = new SolidColorBrush(Colors.Beige);

                if (item.ShowIcon)
                    SetIcon("REMOVED.BMP");
                else
                    SetIcon("BLANK.BMP");
            }

            text.Background = new SolidColorBrush(Colors.Beige);
            text.Foreground = new SolidColorBrush(Colors.Black);

            if (selectionPanel != null)
                selectionPanel.Background = new SolidColorBrush(Colors.Beige);

            if (subSelection != null)
            {
                subSelection.Background = new SolidColorBrush(Colors.Beige);
                subSelection.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        public string Text
        {
            set
            {
                text.Text = value;
            }
        }

        public bool IsChecked
        {
            get
            {
                return (bool)box.IsChecked;
            }

            set
            {
                box.IsChecked = value;
            }
        }

        public bool IsSubselected
        {
            set
            {
                subSelection.IsChecked = value;
            }
        }

        public bool SubselectionEnabled
        {
            set
            {
                if(subSelection != null)
                    subSelection.IsEnabled = value;
            }
        }

        public T Child
        {
            get
            {
                return item;
            }
        }
    }
}
