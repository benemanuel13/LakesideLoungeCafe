using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakesideLoungeAdmin.Application
{
    public class StockItemModel
    {
        private int ingredientId;
        private int originalItems;
        private int currentItems;
        private int currentPortions;
        private int portionsPerItem;
        private decimal costPerItem;

        public StockItemModel(int ingredientId, int originalItems, int currentItems, int currentPortions, int portionsPerItem, decimal costPerItem)
        {
            this.ingredientId = ingredientId;
            this.originalItems = originalItems;
            this.currentItems = currentItems;
            this.currentPortions = currentPortions;
            this.portionsPerItem = portionsPerItem;
            this.costPerItem = costPerItem;
        }

        public int OriginalItems
        {
            get
            {
                return originalItems;
            }
        }

        public int CurrentItems
        {
            get
            {
                return currentItems;
            }

            set
            {
                currentItems = value;
            }
        }

        public int CurrentPortions
        {
            get
            {
                return currentPortions;
            }

            set
            {
                currentPortions = value;
            }
        }

        public int PortionsPerItem
        {
            get
            {
                return portionsPerItem;
            }
        }

        public decimal CostPerItem
        {
            get
            {
                return costPerItem;
            }
        }
    }
}
