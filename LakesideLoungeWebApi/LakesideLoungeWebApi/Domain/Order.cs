using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakesideLoungeWebApi.Domain
{
    public class Order
    {
        int id;
        string name = "";
        int customerType;
        DateTime date;
        int orderNumber;

        List<OrderItem> items = new List<OrderItem>();

        public Order()
        {
        }

        public Order(int id)
        {
            this.id = id; 
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public int OrderNumber
        {
            get
            {
                return orderNumber;
            }

            set
            {
                orderNumber = value;
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
