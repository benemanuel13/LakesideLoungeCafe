using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

using LakesideLoungeAdmin.Infrastructure;

namespace LakesideLoungeAdmin.Application
{
    public class DetailsPanelService
    {
        public bool UpdateModel(int parentType, bool priceChanged, int parentId, int itemId, int itemType, string name, string displayName, string price, IngredientsModel ingredients, int level, int vatStatus, float points = 0, decimal pointPrice = 0.0M, int portions = 0, int group = 0)
        {
            if (itemType == 0)
                Database.UpdateVariation(itemId, parentId, name, displayName, points, pointPrice, vatStatus);
            else
            {
                if (parentType == 0)
                {
                    Database.UpdateComponent(itemId, name, displayName);
                    Database.SaveUpdate("UPDATE_COMPONENT," + itemId + "," + name + "," + displayName);
                    Database.UpdateVariationComponent(parentId, itemId, portions, group, points);
                    Database.SaveUpdate("UPDATE_VARIATION_COMPONENT," + parentId + "," + itemId + "," + portions + "," + group + "," + points);
                }
                else if (level == 1)
                {
                    Database.UpdateComponent(itemId, name, displayName);
                    Database.SaveUpdate("UPDATE_COMPONENT," + itemId + "," + name + "," + displayName);
                }
                else if(level == 2)
                {
                    Database.UpdateComponent(itemId, name, displayName);
                    Database.SaveUpdate("UPDATE_COMPONENT," + itemId + "," + name + "," + displayName);
                    Database.UpdateComponentComponent(parentId, itemId, group);
                    Database.SaveUpdate("UPDATE_COMPONENT_COMPONENT," + parentId + "," + itemId + "," + group);
                }
            }

            Database.DeleteItemIngredients(itemId, itemType);
            Database.InsertItemIngredients(itemId, itemType, ingredients);

            if(priceChanged)
            {
                if (!Database.PriceExists(itemId, itemType))
                {
                    AddPrice(itemId, itemType, DateTime.Now, decimal.Parse(price));
                    return false;
                }

                MessageBoxResult result = MessageBox.Show("You have already added a price today, overwrite old value?", "Price Exists",MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    Database.DeletePrice(itemId, itemType, DateTime.Now);
                    AddPrice(itemId, itemType, DateTime.Now, decimal.Parse(price));
                    return false;
                }
            }
            return true;
        }

        public void AddPrice(int parentId, int parentType, DateTime startDate, decimal price)
        {
            Database.AddPrice(parentId, parentType, startDate, price);
            Database.SaveUpdate("UPDATE_PRICE," + parentId + "," + parentType + "," + startDate.ToShortDateString() + "," + price);
        }
    }
}
