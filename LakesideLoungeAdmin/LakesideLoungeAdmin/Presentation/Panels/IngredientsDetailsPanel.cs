using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using LakesideLoungeAdmin.Application;
using LakesideLoungeAdmin.Presentation.EventArgs;

namespace LakesideLoungeAdmin.Presentation.Panels
{
    public class IngredientsDetailsPanel : UserControl
    {
        public event EventHandler<ItemUpdatedEventArgs> ItemUpdated;

        StackPanel mainPanel = new StackPanel();

        Grid mainGrid = new Grid();
        TextBlock title = new TextBlock();
        TextBox name = new TextBox();
        TextBox displayName = new TextBox();
        TextBox portionSize = new TextBox();
        ComboBox portionType = new ComboBox();
        TextBox reorderLevel = new TextBox();
        TextBox reorderQuantity = new TextBox();

        Button saveButton;
        Button cancelButton;

        Grid stockGrid;
        TextBox addItems;
        TextBox addPortionsPerItem;
        TextBox addCostPerItem;
        Button addStockButton;

        private int itemId;

        bool settingUp = true;
        bool portionSizeChanged = false;
        bool nameChanged = false;

        string oldName;
        string oldDisplayName;
        string oldPortionSize;
        int oldPortionType;

        IngredientsDetailsPanelService svc = new IngredientsDetailsPanelService();

        public IngredientsDetailsPanel()
        {
            Height = 490;
            Width = 350;
            Margin = new Thickness(12, 3, 0, 0);
            Padding = new Thickness(5);
            BorderBrush = new SolidColorBrush(Colors.Black);
            BorderThickness = new Thickness(3);
            Background = new SolidColorBrush(Colors.Beige);

            title.Text = "Ingredients";
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
            mainGrid.RowDefinitions[0].MaxHeight = 30;
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions[1].MaxHeight = 30;
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions[2].MaxHeight = 30;
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions[3].MaxHeight = 30;
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions[4].MaxHeight = 30;

            TextBlock nameTitle = new TextBlock();
            nameTitle.Text = "Name:";
            nameTitle.SetValue(Grid.ColumnProperty, 0);
            nameTitle.SetValue(Grid.RowProperty, 0);
            mainGrid.Children.Add(nameTitle);

            name.SetValue(Grid.ColumnProperty, 1);
            name.SetValue(Grid.RowProperty, 0);
            name.Margin = new Thickness(0, 0, 0, 3);
            name.TextChanged += Name_TextChanged;
            name.IsEnabled = false;
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
            displayName.IsEnabled = false;
            mainGrid.Children.Add(displayName);

            TextBlock portionSizeTitle = new TextBlock();
            portionSizeTitle.Text = "Portion Size:";
            portionSizeTitle.SetValue(Grid.ColumnProperty, 0);
            portionSizeTitle.SetValue(Grid.RowProperty, 2);
            mainGrid.Children.Add(portionSizeTitle);

            StackPanel portionPanel = new StackPanel();
            portionPanel.Orientation = Orientation.Horizontal;
            portionPanel.SetValue(Grid.ColumnProperty, 1);
            portionPanel.SetValue(Grid.RowProperty, 2);

            portionSize.Width = 50;
            portionSize.Height = 19;
            portionSize.Margin = new Thickness(0, 0, 0, 3);
            portionSize.IsEnabled = false;

            portionPanel.Children.Add(portionSize);

            portionType.Items.Add("L");
            portionType.Items.Add("ml");
            portionType.Items.Add("g");
            portionType.IsEnabled = false;
            portionType.Height = 20;
            portionType.Margin = new Thickness(0, 0, 0, 3);

            portionPanel.Children.Add(portionType);

            portionType.SelectedIndex = 0;
            portionType.SelectionChanged += PortionType_SelectionChanged;

            portionSize.TextChanged += PortionSize_TextChanged;
            mainGrid.Children.Add(portionPanel);

            TextBlock reorderLevelTitle = new TextBlock();
            reorderLevelTitle.Text = "Reorder Level:";
            reorderLevelTitle.SetValue(Grid.ColumnProperty, 0);
            reorderLevelTitle.SetValue(Grid.RowProperty, 3);
            mainGrid.Children.Add(reorderLevelTitle);

            reorderLevel.SetValue(Grid.ColumnProperty, 1);
            reorderLevel.SetValue(Grid.RowProperty, 3);
            reorderLevel.Margin = new Thickness(0, 0, 0, 3);
            reorderLevel.TextChanged += ReorderLevel_TextChanged;
            reorderLevel.IsEnabled = false;
            reorderLevel.Width = 50;
            reorderLevel.HorizontalAlignment = HorizontalAlignment.Left;
            mainGrid.Children.Add(reorderLevel);

            TextBlock reorderQuantityTitle = new TextBlock();
            reorderQuantityTitle.Text = "Reorder Quantity:";
            reorderQuantityTitle.SetValue(Grid.ColumnProperty, 0);
            reorderQuantityTitle.SetValue(Grid.RowProperty, 4);
            mainGrid.Children.Add(reorderQuantityTitle);

            reorderQuantity.SetValue(Grid.ColumnProperty, 1);
            reorderQuantity.SetValue(Grid.RowProperty, 4);
            reorderQuantity.Margin = new Thickness(0, 0, 0, 3);
            reorderQuantity.TextChanged += ReorderQuantity_TextChanged; ;
            reorderQuantity.IsEnabled = false;
            reorderQuantity.Width = 50;
            reorderQuantity.HorizontalAlignment = HorizontalAlignment.Left;
            mainGrid.Children.Add(reorderQuantity);

            mainPanel.Children.Add(mainGrid);

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

            StackPanel stockPanel = new StackPanel();
            stockPanel.Margin = new Thickness(5, 5, 0, 0);
            
            TextBlock stockTitle = new TextBlock();
            stockTitle.FontSize = 20;
            stockTitle.Text = "Stock Details";

            stockPanel.Children.Add(stockTitle);

            StackPanel stockTitles = new StackPanel();
            stockTitles.Orientation = Orientation.Horizontal;

            TextBlock itemsTitle = new TextBlock();
            itemsTitle.Width = 60;
            itemsTitle.Margin = new Thickness(5, 0, 0, 0);
            itemsTitle.Text = "Items";

            TextBlock portionsTitle = new TextBlock();
            portionsTitle.Width = 65;
            portionsTitle.Text = "Portions";

            TextBlock portionsPerTitle = new TextBlock();
            portionsPerTitle.Width = 100;
            portionsPerTitle.Text = "Portions per item";

            TextBlock costTitle = new TextBlock();
            costTitle.Text = "Cost per Item (£)";

            stockTitles.Children.Add(itemsTitle);
            stockTitles.Children.Add(portionsTitle);
            stockTitles.Children.Add(portionsPerTitle);
            stockTitles.Children.Add(costTitle);

            stockPanel.Children.Add(stockTitles);

            stockGrid = new Grid();

            stockGrid.ColumnDefinitions.Add(new ColumnDefinition());
            stockGrid.ColumnDefinitions[0].MaxWidth = 65;
            stockGrid.ColumnDefinitions.Add(new ColumnDefinition());
            stockGrid.ColumnDefinitions[1].MaxWidth = 65;
            stockGrid.ColumnDefinitions.Add(new ColumnDefinition());
            stockGrid.ColumnDefinitions[2].MaxWidth = 100;
            stockGrid.ColumnDefinitions.Add(new ColumnDefinition());

            //stockGrid.ShowGridLines = true;
            //stockGrid.MinHeight = 100;

            UserControl bordered = new UserControl();
            bordered.BorderBrush = new SolidColorBrush(Colors.Black);
            bordered.BorderThickness = new Thickness(1);
            bordered.Margin = new Thickness(0, 0, 0, 5);

            stockGrid.Background = new SolidColorBrush(Colors.White);

            bordered.Content = stockGrid;

            stockPanel.Children.Add(bordered);

            stockPanel.MinHeight = 140;

            StackPanel addStockPanel = new StackPanel();

            Grid addStockGrid = new Grid();
            addStockGrid.Margin = new Thickness(5, 0, 0, 5);

            addStockGrid.ColumnDefinitions.Add(new ColumnDefinition());
            addStockGrid.ColumnDefinitions.Add(new ColumnDefinition());
            addStockGrid.ColumnDefinitions.Add(new ColumnDefinition());

            addStockGrid.RowDefinitions.Add(new RowDefinition());
            addStockGrid.RowDefinitions.Add(new RowDefinition());

            TextBlock addItemsTitle = new TextBlock();
            addItemsTitle.Text = "Items";
            addItemsTitle.SetValue(Grid.ColumnProperty, 0);
            addItemsTitle.SetValue(Grid.RowProperty, 0);

            addStockGrid.Children.Add(addItemsTitle);

            addItems = new TextBox();
            addItems.SetValue(Grid.ColumnProperty, 0);
            addItems.SetValue(Grid.RowProperty, 1);

            addStockGrid.Children.Add(addItems);

            TextBlock addPortionsPerItemTitle = new TextBlock();
            addPortionsPerItemTitle.Text = "Portions per Item";
            addPortionsPerItemTitle.SetValue(Grid.ColumnProperty, 1);
            addPortionsPerItemTitle.SetValue(Grid.RowProperty, 0);

            addStockGrid.Children.Add(addPortionsPerItemTitle);

            addPortionsPerItem = new TextBox();
            addPortionsPerItem.SetValue(Grid.ColumnProperty, 1);
            addPortionsPerItem.SetValue(Grid.RowProperty, 1);

            addStockGrid.Children.Add(addPortionsPerItem);

            TextBlock addCostPerItemTitle = new TextBlock();
            addCostPerItemTitle.Text = "Cost Per Item (£)";
            addCostPerItemTitle.SetValue(Grid.ColumnProperty, 2);
            addCostPerItemTitle.SetValue(Grid.RowProperty, 0);

            addStockGrid.Children.Add(addCostPerItemTitle);

            addCostPerItem = new TextBox();
            addCostPerItem.SetValue(Grid.ColumnProperty, 2);
            addCostPerItem.SetValue(Grid.RowProperty, 1);

            addStockGrid.Children.Add(addCostPerItem);

            addStockButton = new Button();
            addStockButton.Click += AddStockButton_Click;
            addStockButton.IsEnabled = false;
            addStockButton.Margin = new Thickness(5, 0, 0, 0);
            addStockButton.Content = "Add Stock";

            addStockPanel.Children.Add(addStockGrid);
            addStockPanel.Children.Add(addStockButton);

            //stockPanel.Children.Add(addStockPanel);

            mainPanel.Children.Add(stockPanel);
            mainPanel.Children.Add(addStockPanel);

            Content = mainPanel;
        }

        private void ReorderLevel_TextChanged(object sender, TextChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void ReorderQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void PortionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableButtons();
        }

        private void AddStockButton_Click(object sender, RoutedEventArgs e)
        {
            int items;
            int portionsPerItem;
            decimal costPerItem;

            if (!int.TryParse(addItems.Text, out items))
            {
                MessageBox.Show("Invalid Items Entered, please reenter.");
                return;
            }

            if (!int.TryParse(addPortionsPerItem.Text, out portionsPerItem))
            {
                MessageBox.Show("Invalid Portions Per Item Entered, please reenter.");
                return;
            }

            if (!decimal.TryParse(addCostPerItem.Text, out costPerItem))
            {
                MessageBox.Show("Invalid Cost Per Item Entered, please reenter.");
                return;
            }

            svc.AddStockItem(itemId, items, portionsPerItem, costPerItem);
            PopulateStock();
        }

        public void PopulateStock()
        {
            stockGrid.Children.Clear();
            stockGrid.RowDefinitions.Clear();

            bool stockFound = false;

            List<StockItemModel> models = svc.GetCurrentStock(itemId);

            foreach (StockItemModel model in models)
            {
                stockFound = true;

                stockGrid.RowDefinitions.Add(new RowDefinition());

                TextBlock currentItems = new TextBlock();
                currentItems.Text = model.CurrentItems.ToString();
                currentItems.Margin = new Thickness(5, 0, 0, 0);

                currentItems.SetValue(Grid.ColumnProperty, 0);
                currentItems.SetValue(Grid.RowProperty, stockGrid.RowDefinitions.Count - 1);

                stockGrid.Children.Add(currentItems);

                TextBlock currentPortions = new TextBlock();
                currentPortions.Text = model.CurrentPortions.ToString();

                currentPortions.SetValue(Grid.ColumnProperty, 1);
                currentPortions.SetValue(Grid.RowProperty, stockGrid.RowDefinitions.Count - 1);

                stockGrid.Children.Add(currentPortions);
                
                TextBlock portionsPerItem = new TextBlock();
                portionsPerItem.Text = model.PortionsPerItem.ToString();

                portionsPerItem.SetValue(Grid.ColumnProperty, 2);
                portionsPerItem.SetValue(Grid.RowProperty, stockGrid.RowDefinitions.Count - 1);

                stockGrid.Children.Add(portionsPerItem);

                TextBlock costPerItem = new TextBlock();
                costPerItem.Text = model.CostPerItem.ToString("#0.00");

                costPerItem.SetValue(Grid.ColumnProperty, 3);
                costPerItem.SetValue(Grid.RowProperty, stockGrid.RowDefinitions.Count - 1);

                stockGrid.Children.Add(costPerItem);
            }

            if(stockFound)
            {
                StockItemModel model = models[models.Count - 1];

                addItems.Text = model.OriginalItems.ToString();
                addPortionsPerItem.Text = model.PortionsPerItem.ToString();
                addCostPerItem.Text = model.CostPerItem.ToString("#0.00");
            }
            else
            {
                addItems.Text = "";
                addPortionsPerItem.Text = "";
                addCostPerItem.Text = "";
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            name.Text = oldName;
            displayName.Text = oldDisplayName;
            portionSize.Text = oldPortionSize;
            portionType.SelectedIndex = oldPortionType;

            saveButton.IsEnabled = false;
            cancelButton.IsEnabled = false;

            nameChanged = false;
            portionSizeChanged = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int result;

            if (int.TryParse(portionSize.Text, out result) == false)
            {
                MessageBox.Show("Invalid Portion Size Entered", "Invalid Portion Size", MessageBoxButton.OK);
                portionSize.Text = oldPortionSize;

            //    if(!nameChanged)
            //        saveButton.IsEnabled = false;

                return;
            }

            svc.UpdateIngredient(itemId, name.Text, displayName.Text, result, portionType.SelectedIndex);
            
            oldName = name.Text;
            oldDisplayName = displayName.Text;
            oldPortionSize = portionSize.Text;
            oldPortionType = portionType.SelectedIndex;

            saveButton.IsEnabled = false;
            cancelButton.IsEnabled = false;

            nameChanged = false;
            portionSizeChanged = false;

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

        private void PortionSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            portionSizeChanged = true;
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

        public int PortionSize
        {
            get
            {
                return int.Parse(portionSize.Text);
            }

            set
            {
                settingUp = true;
                portionSize.Text = value.ToString();
                oldPortionSize = portionSize.Text;
                settingUp = false;
            }
        }

        public int PortionType
        {
            get
            {
                return portionType.SelectedIndex;
            }

            set
            {
                {
                    settingUp = true;
                    portionType.SelectedIndex = value;
                    oldPortionType = value;
                    settingUp = false;
                }
            }

        }
        //public decimal Cost
        //{
        //    get
        //    {
        //        return Decimal.Parse(cost.Text);
        //    }

        //    set
        //    {
        //        settingUp = true;
        //        cost.Text = value.ToString("#0.00");
        //        oldCost = cost.Text;
        //        settingUp = false;
        //    }
        //}

        public void Set(int id)
        {
            name.IsEnabled = true;
            displayName.IsEnabled = true;
            portionSize.IsEnabled = true;
            portionType.IsEnabled = true;

            itemId = id;

            PopulateStock();

            addItems.IsEnabled = true;
            addPortionsPerItem.IsEnabled = true;
            addCostPerItem.IsEnabled = true;

            addStockButton.IsEnabled = true;

            nameChanged = false;
            //costChanged = false;
        }

        public void Reset()
        {
            settingUp = true;

            //title.Text = "Lakeside Lounge Cafe";
            nameChanged = false;

            name.IsEnabled = false;
            name.Text = "";

            displayName.IsEnabled = false;
            displayName.Text = "";

            portionSize.IsEnabled = false;
            portionSize.Text = "";
            portionType.IsEnabled = false;

            stockGrid.Children.Clear();
            stockGrid.RowDefinitions.Clear();

            addItems.IsEnabled = false;
            addItems.Text = "";

            addPortionsPerItem.IsEnabled = false;
            addPortionsPerItem.Text = "";

            addCostPerItem.IsEnabled = false;
            addCostPerItem.Text = "";

            addStockButton.IsEnabled = false;

            //cost.IsEnabled = false;
            //cost.Text = "";
            //costChanged = false;

            saveButton.IsEnabled = false;

            settingUp = false;
        }
    }
}
