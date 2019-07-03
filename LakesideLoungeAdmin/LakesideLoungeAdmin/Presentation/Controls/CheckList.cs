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

namespace LakesideLoungeAdmin.Presentation.Controls
{
    public class CheckList : ListView
    {
        public event EventHandler<ItemCheckedEventArgs<ItemModelBase>> ItemChecked;
        public event EventHandler<PortionChosenEventArgs> PortionChosen;

        private Dictionary<int, CheckBoxWithId> boxes = new Dictionary<int, CheckBoxWithId>();
        private List<ComboBoxWithId> portions = new List<ComboBoxWithId>();

        IngredientsModel allModel;
        IngredientsModel model = new IngredientsModel(false);

        private bool stopPortions = false;

        public CheckList()
        {
            allModel = new IngredientsModel();

            int position = 0;

            foreach (IngredientModel ingredientModel in allModel.Children)
            {
                CheckBoxWithId newBox = new CheckBoxWithId(ingredientModel.Id, position) { Content = ingredientModel.Description };
                newBox.Width = 255;
                newBox.Click += NewBox_Click;
                boxes.Add(newBox.Id, newBox);

                ComboBoxWithId portions = new ComboBoxWithId(newBox.Id, position);
                portions.Text = ingredientModel.Portions.ToString();
                portions.FontWeight = FontWeights.Bold;
                portions.Visibility = Visibility.Hidden;
                portions.Items.Add(new TextBlock() { Text = "1" });
                portions.Items.Add(new TextBlock() { Text = "2" });
                portions.Items.Add(new TextBlock() { Text = "3" });
                portions.SelectedIndex = 0;
                portions.SelectionChanged += Portions_SelectionChanged;
                this.portions.Add(portions);

                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;

                panel.Children.Add(newBox);
                panel.Children.Add(portions);

                this.AddChild(panel);

                ++position;
            }
        }

        private void Portions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (stopPortions)
                return;

            ComboBoxWithId combo = (ComboBoxWithId)sender;

            IngredientModel ingredient = (IngredientModel)model.Children.Where(ing => ing.Id == combo.Id).Single();
            ingredient.Portions = combo.SelectedIndex + 1;

            PortionChosen(this, new PortionChosenEventArgs());
        }

        private void NewBox_Click(object sender, RoutedEventArgs e)
        {
            if (stopPortions)
                return;

            CheckBoxWithId box = (CheckBoxWithId)sender;

            ComboBoxWithId portions = this.portions[box.Position];

            if ((bool)box.IsChecked)
            {
                if (!model.Children.Contains(allModel.Children[box.Position]))
                    model.Children.Add(allModel.Children[box.Position]);

                portions.Visibility = Visibility.Visible;
            }
            else
            {
                if (model.Children.Contains(allModel.Children[box.Position]))
                    model.Children.Remove(allModel.Children[box.Position]);

                portions.Visibility = Visibility.Hidden;
            }

            ItemChecked(this, new ItemCheckedEventArgs<ItemModelBase>());
        }

        public void CheckItems(int id, int parentType)
        {
            IngredientsModel selectionModel = new IngredientsModel(id, parentType);

            ClearItems();

            foreach (IngredientModel ingredientModel in selectionModel.Children)
            {
                model.Children.Add(allModel.Children.Where(m => m.Id == ingredientModel.Id).Single());

                stopPortions = true;
                boxes[ingredientModel.Id].IsChecked = true;
                portions[boxes[ingredientModel.Id].Position].SelectedIndex = ingredientModel.Portions - 1;
                portions[boxes[ingredientModel.Id].Position].Visibility = Visibility.Visible;
                stopPortions = false;
            }
        }

        public void ClearItems()
        {
            model.Children.Clear();

            foreach (CheckBoxWithId box in boxes.Values)
            {
                stopPortions = true;
                box.IsChecked = false;                
                portions[box.Position].SelectedIndex = 0;
                portions[box.Position].Visibility = Visibility.Hidden;
                stopPortions = false;
            }
        }

        public IngredientsModel SelectedIngredients
        {
            get
            {
                return model;
            }
        }
    }
}
