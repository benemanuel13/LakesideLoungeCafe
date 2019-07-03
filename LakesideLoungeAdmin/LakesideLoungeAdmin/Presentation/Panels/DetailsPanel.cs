using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using LakesideLoungeAdmin.Application;
using LakesideLoungeAdmin.Presentation.Controls;
using LakesideLoungeAdmin.Presentation.EventArgs;

namespace LakesideLoungeAdmin.Presentation.Panels
{
    public class DetailsPanel : UserControl
    {
        public event EventHandler<ItemUpdatedEventArgs> ItemUpdated;

        StackPanel mainPanel = new StackPanel();

        Grid mainGrid = new Grid();
        TextBlock title = new TextBlock();
        TextBox name = new TextBox();
        TextBox displayName = new TextBox();
        TextBox price = new TextBox();
        TextBlock pointsTitle;
        TextBox points = new TextBox();
        TextBlock pointPriceTitle;
        TextBox pointPrice = new TextBox();

        TextBlock vatTitle;
        ComboBox vat = new ComboBox();

        TextBlock portionTitle;
        ComboBox portions = new ComboBox();
        TextBlock groupTitle;
        ComboBox group = new ComboBox();
        CheckList ingredients;
        ScrollViewer viewer = new ScrollViewer();
        Button saveButton;
        Button cancelButton;

        private int parentId = 0;
        private int parentType = 0;
        private int itemId;
        private int itemType;

        private int level = 0;

        bool settingUp = true;
        bool nameChanged = false;
        bool priceChanged = false;

        string oldName;
        string oldDisplayName;
        string oldPrice;
        string oldPoints;
        string oldPointPrice;
        int oldVAT;
        int oldPortions;
        int oldGroup;

        DetailsPanelService svc = new DetailsPanelService();

        public DetailsPanel()
        {
            Height = 500;
            Width = 350;
            Margin = new Thickness(12, 3, 0, 0);
            Padding = new Thickness(5);
            BorderBrush = new SolidColorBrush(Colors.Black);
            BorderThickness = new Thickness(3);
            Background = new SolidColorBrush(Colors.Beige);

            title.Text = "Lakeside Lounge Cafe";
            title.Foreground = new SolidColorBrush(Colors.Blue);
            title.Height = 35;
            title.FontSize = 25;
            title.Margin = new Thickness(0, 0, 0, 5);

            mainPanel.Orientation = Orientation.Vertical;

            mainPanel.Children.Add(title);

            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            mainGrid.ColumnDefinitions[0].MaxWidth = 100;
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());

            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions[0].MaxHeight = 25;
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions[1].MaxHeight = 25;
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions[2].MaxHeight = 25;
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions[3].MaxHeight = 25;
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions[4].MaxHeight = 25;
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions[5].MaxHeight = 25;

            TextBlock nameTitle = new TextBlock();
            nameTitle.Text = "Name:";
            nameTitle.SetValue(Grid.ColumnProperty, 0);
            nameTitle.SetValue(Grid.RowProperty, 0);
            mainGrid.Children.Add(nameTitle);

            name.SetValue(Grid.ColumnProperty, 1);
            name.SetValue(Grid.RowProperty, 0);
            name.Margin = new Thickness(0, 0, 0, 3);
            name.TextChanged += Name_TextChanged;
            mainGrid.Children.Add(name);

            TextBlock displayNameTitle = new TextBlock();
            displayNameTitle.Text = "Display Name:";
            displayNameTitle.SetValue(Grid.ColumnProperty, 0);
            displayNameTitle.SetValue(Grid.RowProperty, 1);
            mainGrid.Children.Add(displayNameTitle);

            displayName.SetValue(Grid.ColumnProperty, 1);
            displayName.SetValue(Grid.RowProperty, 1);
            displayName.Margin = new Thickness(0, 0, 0, 3);
            displayName.TextChanged += DisplayName_TextChanged;
            mainGrid.Children.Add(displayName);

            TextBlock priceTitle = new TextBlock();
            priceTitle.Text = "Price (£):";
            priceTitle.SetValue(Grid.ColumnProperty, 0);
            priceTitle.SetValue(Grid.RowProperty, 2);
            mainGrid.Children.Add(priceTitle);

            price.SetValue(Grid.ColumnProperty, 1);
            price.SetValue(Grid.RowProperty, 2);
            price.Margin = new Thickness(0, 0, 0, 3);
            price.TextChanged += Price_TextChanged;
            mainGrid.Children.Add(price);

            pointsTitle = new TextBlock();
            pointsTitle.Text = "Points:";
            pointsTitle.SetValue(Grid.ColumnProperty, 0);
            pointsTitle.SetValue(Grid.RowProperty, 3);
            mainGrid.Children.Add(pointsTitle);

            points.SetValue(Grid.ColumnProperty, 1);
            points.SetValue(Grid.RowProperty, 3);
            points.Margin = new Thickness(0, 0, 0, 3);
            points.TextChanged += Points_TextChanged;
            mainGrid.Children.Add(points);

            pointPriceTitle = new TextBlock();
            pointPriceTitle.Text = "Point Price (£):";
            pointPriceTitle.SetValue(Grid.ColumnProperty, 0);
            pointPriceTitle.SetValue(Grid.RowProperty, 4);
            mainGrid.Children.Add(pointPriceTitle);

            pointPrice.SetValue(Grid.ColumnProperty, 1);
            pointPrice.SetValue(Grid.RowProperty, 4);
            pointPrice.Margin = new Thickness(0, 0, 0, 3);
            pointPrice.TextChanged += PointPrice_TextChanged;
            mainGrid.Children.Add(pointPrice);


            vatTitle = new TextBlock();
            vatTitle.Text = "VAT Status:";
            vatTitle.SetValue(Grid.ColumnProperty, 0);
            vatTitle.SetValue(Grid.RowProperty, 5);
            mainGrid.Children.Add(vatTitle);

            vat.SetValue(Grid.ColumnProperty, 1);
            vat.SetValue(Grid.RowProperty, 5);
            vat.Items.Add("No VAT");
            vat.Items.Add("VAT If Eaten In");
            vat.Items.Add("VAT Always");
            vat.SelectedIndex = 0;
            vat.SelectionChanged += Vat_SelectionChanged;
            vat.Margin = new Thickness(0, 0, 0, 3);
            mainGrid.Children.Add(vat);


            portionTitle = new TextBlock();
            portionTitle.Text = "Default Portions:";
            portionTitle.SetValue(Grid.ColumnProperty, 0);
            portionTitle.SetValue(Grid.RowProperty, 5);
            mainGrid.Children.Add(portionTitle);

            portions.SetValue(Grid.ColumnProperty, 1);
            portions.SetValue(Grid.RowProperty, 5);
            portions.Items.Add("0");
            portions.Items.Add("1");
            portions.Items.Add("2");
            portions.Items.Add("3");
            portions.Items.Add("4");
            portions.Items.Add("5");
            portions.SelectedIndex = 0;
            portions.Margin = new Thickness(0, 0, 0, 3);
            portions.SelectionChanged += Portions_SelectionChanged;
            mainGrid.Children.Add(portions);

            groupTitle = new TextBlock();
            groupTitle.Text = "Group:";
            groupTitle.SetValue(Grid.ColumnProperty, 0);
            groupTitle.SetValue(Grid.RowProperty, 4);
            mainGrid.Children.Add(groupTitle);

            group.SetValue(Grid.ColumnProperty, 1);
            group.SetValue(Grid.RowProperty, 4);
            group.Items.Add("0");
            group.Items.Add("1");
            group.Items.Add("2");
            group.Items.Add("3");
            group.SelectedIndex = 0;
            group.Margin = new Thickness(0, 0, 0, 3);
            group.SelectionChanged += Group_SelectionChanged;
            mainGrid.Children.Add(group);

            mainPanel.Children.Add(mainGrid);

            StackPanel titlePanel = new StackPanel();
            titlePanel.Orientation = Orientation.Horizontal;

            TextBlock ingredientsTitle = new TextBlock();
            ingredientsTitle.Text = "Ingredients:";
            ingredientsTitle.Margin = new Thickness(0, 5, 0, 0);
            titlePanel.Children.Add(ingredientsTitle);

            TextBlock portionsTitle = new TextBlock();
            portionsTitle.Text = "Portions";
            portionsTitle.Margin = new Thickness(200, 5, 0, 0);
            titlePanel.Children.Add(portionsTitle);

            mainPanel.Children.Add(titlePanel);

            viewer.Height = 200;
            viewer.BorderBrush = new SolidColorBrush(Colors.Red);
            viewer.BorderThickness = new Thickness(1);
            viewer.Margin = new Thickness(3, 5, 3, 5);

            ingredients = new CheckList();
            ingredients.ItemChecked += Ingredients_ItemChecked;
            ingredients.PortionChosen += Ingredients_PortionChosen;
            viewer.Content = ingredients;
            
            mainPanel.Children.Add(viewer);

            StackPanel buttonsPanel = new StackPanel();
            buttonsPanel.Margin = new Thickness(0, 10, 0, 0);

            saveButton = new Button();
            saveButton.Content = "Save";
            saveButton.IsEnabled = false;
            saveButton.Margin = new Thickness(0, 0, 0, 3);
            saveButton.Click += SaveButton_Click;

            cancelButton = new Button();
            cancelButton.Content = "Cancel";
            cancelButton.IsEnabled = false;
            cancelButton.Click += CancelButton_Click;

            buttonsPanel.Children.Add(saveButton);
            buttonsPanel.Children.Add(cancelButton);

            mainPanel.Children.Add(buttonsPanel);

            Content = mainPanel;

            Reset();
        }

        private void Vat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableButtons();
        }

        private void Group_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableButtons();
        }

        private void Portions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableButtons();
        }

        public void EnablePoints()
        {
            points.IsEnabled = true;

            if (points.Text == "")
                points.Text = "0";
        }

        public void EnableVAT()
        {
            vat.IsEnabled = true;
        }

        public void DisableVAT()
        {
            vat.IsEnabled = false;
        }

        public void DisablePoints()
        {
            points.Text = "";
            points.IsEnabled = false;
        }

        public void ShowPointPrice()
        {
            pointPriceTitle.Visibility = Visibility.Visible;
            pointPrice.Visibility = Visibility.Visible;
        }

        public void HidePointPrice()
        {
            pointPriceTitle.Visibility = Visibility.Hidden;
            pointPrice.Visibility = Visibility.Hidden;
        }

        public void EnableGroup()
        {
            group.IsEnabled = true;
            groupTitle.Visibility = Visibility.Visible;
            group.Visibility = Visibility.Visible;
        }

        public void EnablePortions()
        {
            portions.IsEnabled = true;
            portionTitle.Visibility = Visibility.Visible;
            portions.Visibility = Visibility.Visible;
        }

        public void DisableGroup()
        {
            group.IsEnabled = false;
        }

        public void DisablePortions()
        {
            portions.IsEnabled = false;
        }

        public void ShowGroup()
        {
            pointPriceTitle.Visibility = Visibility.Hidden;
            pointPrice.Visibility = Visibility.Hidden;

            groupTitle.Visibility = Visibility.Visible;
            group.Visibility = Visibility.Visible;

            HideVAT();
        }

        public void HideGroup()
        {
            groupTitle.Visibility = Visibility.Hidden;
            group.Visibility = Visibility.Hidden;

            ShowVAT();
        }

        public void ShowPortions()
        {
            portionTitle.Visibility = Visibility.Visible;
            portions.Visibility = Visibility.Visible;
        }

        public void HidePortions()
        {
            portionTitle.Visibility = Visibility.Hidden;
            portions.Visibility = Visibility.Hidden;
        }

        public void ShowVAT()
        {
            vatTitle.Visibility = Visibility.Visible;
            vat.Visibility = Visibility.Visible;
        }

        public void HideVAT()
        {
            vatTitle.Visibility = Visibility.Hidden;
            vat.Visibility = Visibility.Hidden;
        }

        private void PointPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButtons();
        }

        private void Points_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButtons();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            name.Text = oldName;
            displayName.Text = oldDisplayName;
            price.Text = oldPrice;
            points.Text = oldPoints;
            pointPrice.Text = oldPointPrice;
            portions.SelectedIndex = oldPortions;
            group.SelectedIndex = oldGroup;
            vat.SelectedIndex = oldVAT;

            settingUp = true;
            ingredients.CheckItems(itemId, itemType);
            settingUp = false;

            saveButton.IsEnabled = false;
            cancelButton.IsEnabled = false;

            nameChanged = false;
            priceChanged = false;
        }

        private void Ingredients_PortionChosen(object sender, PortionChosenEventArgs e)
        {
            nameChanged = true;
            EnableButtons();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            decimal result;
            if (decimal.TryParse(price.Text, out result) == false)
            {
                MessageBox.Show("Invalid Price Entered", "Invalid Price", MessageBoxButton.OK);
                price.Text = oldPrice;

                if (!nameChanged)
                {
                    saveButton.IsEnabled = false;
                    cancelButton.IsEnabled = false;
                }

                return;
            }

            if (points.IsEnabled)
            {
                float pointResult;
                if (float.TryParse(points.Text, out pointResult) == false)
                {
                    MessageBox.Show("Invalid Points Entered", "Invalid Points", MessageBoxButton.OK);
                    points.Text = oldPoints;

                    if (!nameChanged)
                    {
                        saveButton.IsEnabled = false;
                        cancelButton.IsEnabled = false;
                    }

                    return;
                }
            }

            if (parentType != 0 && pointPrice.IsEnabled)
            {
                decimal pointPriceResult;
                if (decimal.TryParse(pointPrice.Text, out pointPriceResult) == false)
                {
                    MessageBox.Show("Invalid Points Price Entered", "Invalid Points Price", MessageBoxButton.OK);
                    pointPrice.Text = oldPointPrice;

                    if (!nameChanged)
                    {
                        saveButton.IsEnabled = false;
                        cancelButton.IsEnabled = false;
                    }

                    return;
                }
            }

            if (svc.UpdateModel(parentType, priceChanged, parentId, itemId, itemType, name.Text, displayName.Text, price.Text, ingredients.SelectedIngredients, level, vat.SelectedIndex + 1, Points, PointPrice, Portions, Group))
                price.Text = oldPrice;

            oldName = name.Text;
            oldDisplayName = displayName.Text;
            oldPrice = price.Text;
            oldPoints = points.Text;
            oldPointPrice = pointPrice.Text;
            oldPortions = Portions;
            oldGroup = Group;
            oldVAT = vat.SelectedIndex;

            saveButton.IsEnabled = false;
            cancelButton.IsEnabled = false;

            nameChanged = false;
            priceChanged = false;

            ItemUpdated(this, new ItemUpdatedEventArgs(name.Text));
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            nameChanged = true;
            EnableButtons();
        }

        private void DisplayName_TextChanged(object sender, TextChangedEventArgs e)
        {
            nameChanged = true;
            EnableButtons();
        }

        private void Price_TextChanged(object sender, TextChangedEventArgs e)
        {
            priceChanged = true;
            EnableButtons();
        }

        private void Ingredients_ItemChecked(object sender, EventArgs.ItemCheckedEventArgs<ItemModelBase> e)
        {
            nameChanged = true;
            EnableButtons();
        }

        private void EnableButtons()
        {
            if (!settingUp)
            {
                saveButton.IsEnabled = true;
                cancelButton.IsEnabled = true;
            }
        }

        public string Title
        {
            set
            {
                title.Text = value;
            }
        }

        public string ItemName
        {
            get
            {
                return name.Text;
            }

            set
            {
                settingUp = true;
                name.Text = value;
                oldName = value;
                settingUp = false;
            }
        }

        public string DisplayName
        {
            get
            {
                return displayName.Text;
            }

            set
            {
                settingUp = true;
                displayName.Text = value;
                oldDisplayName = value;
                settingUp = false;
            }
        }

        public decimal Price
        {
            get
            {
                return Decimal.Parse(price.Text);
            }

            set
            {
                settingUp = true;
                price.Text = value.ToString("#0.00");
                oldPrice = price.Text;
                settingUp = false;
            }
        }

        public float Points
        {
            get
            {
                try
                {
                    return float.Parse(points.Text);
                }
                catch
                {
                    return 0;
                }
            }

            set
            {
                settingUp = true;
                points.Text = value.ToString();
                oldPoints = points.Text;
                settingUp = false;
            }
        }

        public decimal PointPrice
        {
            get
            {
                try
                {
                    return Decimal.Parse(pointPrice.Text);
                }
                catch
                {
                    return 0;
                }
            }

            set
            {
                settingUp = true;
                pointPrice.Text = value.ToString("0.00");
                oldPointPrice = pointPrice.Text;
                settingUp = false;
            }
        }

        public void Set(int id, int parentType, bool showPoints)
        {
            name.IsEnabled = true;
            displayName.IsEnabled = true;
            price.IsEnabled = true;
            points.IsEnabled = true;

            if (showPoints)
            {
                points.IsEnabled = true;
                pointPrice.IsEnabled = true;
            }
            else
            {
                points.IsEnabled = false;
                pointPrice.IsEnabled = false;
            }

            viewer.IsEnabled = true;

            settingUp = true;
            ingredients.CheckItems(id, parentType);
            settingUp = false;

            itemId = id;
            itemType = parentType;

            nameChanged = false;
            priceChanged = false;

            saveButton.IsEnabled = false;
            cancelButton.IsEnabled = false;
        }

        public void Reset()
        {
            settingUp = true;

            title.Text = "Lakeside Lounge Cafe";
            
            name.IsEnabled = false;
            name.Text = "";

            displayName.IsEnabled = false;
            displayName.Text = "";

            price.IsEnabled = false;
            price.Text = "";
            priceChanged = false;

            points.IsEnabled = false;
            points.Text = "";

            pointPrice.IsEnabled = false;
            pointPrice.Text = "";

            vat.IsEnabled = false;
            vat.SelectedIndex = 0;

            portions.IsEnabled = false;
            portions.SelectedIndex = 1;

            group.IsEnabled = false;
            group.SelectedIndex = 0;

            ingredients.ClearItems();
            viewer.IsEnabled = false;

            saveButton.IsEnabled = false;
            cancelButton.IsEnabled = false;

            settingUp = false;
        }

        public int ParentType
        {
            set
            {
                parentType = value;
            }
        }

        public int ParentId
        {
            set
            {
                parentId = value;
            }
        }

        public int Level
        {
            get
            {
                return level;
            }

            set
            {
                level = value;
            }
        }

        public int Group
        {
            get
            {
                return group.SelectedIndex;
            }

            set
            {
                settingUp = true;
                group.SelectedIndex = value;
                oldGroup = value;
                settingUp = false;
                
            }
        }

        public int Portions
        {
            get
            {
                return portions.SelectedIndex;
            }

            set
            {
                settingUp = true;
                portions.SelectedIndex = value;
                oldPortions = value;
                settingUp = false;
            }
        }

        public int VAT
        {
            get
            {
                return vat.SelectedIndex + 1;
            }

            set
            {
                settingUp = true;
                vat.SelectedIndex = value - 1;
                oldVAT = value - 1;
                settingUp = false;
            }
        }
    }
}
