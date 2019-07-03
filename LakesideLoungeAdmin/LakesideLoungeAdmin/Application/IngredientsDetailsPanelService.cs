using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

using LakesideLoungeAdmin.Infrastructure;

namespace LakesideLoungeAdmin.Application
{
    public class IngredientsDetailsPanelService
    {
        public void UpdateIngredient(int ingredientId, string name, string displayName, int portionSize, int portionType)
        {
            Database.UpdateIngredient(ingredientId, name, displayName, portionSize, portionType);
        }

        public void AddCost(int ingredientId, DateTime startDate, decimal price)
        {
            Database.AddCost(ingredientId, startDate, price);
        }

        public void AddStockItem(int ingredientId, int items, int portionsPerItem, decimal cost)
        {
            Database.AddStockItem(ingredientId, items, portionsPerItem, cost);
        }

        public List<StockItemModel> GetCurrentStock(int ingredientId)
        {
            List<StockItemModel> stockItems = Database.GetCurrentStock(ingredientId);

            return stockItems;
        }
    }
}
