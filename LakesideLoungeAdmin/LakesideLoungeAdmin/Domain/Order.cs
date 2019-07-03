using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakesideLoungeAdmin.Domain
{
    public class Order
    {
        int id;
        string name = "";
        int customerType;
        DateTime date;

        List<OrderItem> items = new List<OrderItem>();

        public Order(int id, int customerType, DateTime date)
        {
            this.id = id;
            this.customerType = customerType;
            this.date = date;
        }

        public List<Ingredient> IngredientsUsed()
        {
            List<Ingredient> ingredientsUsed = new List<Ingredient>();

            foreach(OrderItem item in items)
                foreach (Ingredient ingredient in item.IngredientsUsed())
                    ingredientsUsed.Add(ingredient);

            return ingredientsUsed;
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public int CustomerType
        {
            get
            {
                return customerType;
            }

            set
            {
                customerType = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public List<OrderItem> OrderItems
        {
            get
            {
                return items;
            }
        }

        public void AddOrderItem(OrderItem item)
        {
            items.Add(item);
        }
    }
}
