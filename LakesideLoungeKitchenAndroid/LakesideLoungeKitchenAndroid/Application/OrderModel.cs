using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeKitchenAndroid.Domain;
using LakesideLoungeKitchenAndroid.Infrastructure;

namespace LakesideLoungeKitchenAndroid.Application
{
    public class OrderModel
    {
        int id = 0;
        bool live;
        string name = "";
        int customerType;
        DateTime date;
        int orderNumber;

        Dictionary<int, OrderItemModel> items = new Dictionary<int, OrderItemModel>();

        public OrderModel()
        {
        }

        public OrderModel(int id)
        {
            if (id == 0)
                return;

            Order order = Database.GetOrder(id, true);
            this.id = id;
            this.name = order.Name;
            this.customerType = order.CustomerType;
            this.date = order.Date;
            this.OrderNumber = order.OrderNumber;

            foreach (OrderItem item in order.OrderItems)
            {
                OrderItemModel newItemModel = new OrderItemModel(item.Id, id, item.Id, item.DisplayName, item.InOutStatus, item.DiscountId, item.State);
                items.Add(item.Id, newItemModel);

                foreach (OrderItemComponent component in item.Components)
                {
                    OrderItemComponentModel componentModel = new OrderItemComponentModel(component);
                    newItemModel.AddComponentModel(componentModel);

                    foreach(OrderItemComponentComponent subComponent in component.Components)
                    {
                        OrderItemComponentComponentModel subComponentModel = new OrderItemComponentComponentModel(subComponent);
                        componentModel.AddComponent(subComponentModel);
                    }
                }
            }
        }
        
        public OrderModel(int id, bool live)
        {
            this.id = id;
            this.live = live;
            this.customerType = 1;
        }

        public void AddOrderItemModel(OrderItemModel model)
        {
            items.Add(model.Id, model);
        }

        public void UpdateOrderItemModel(int key, OrderItemModel model)
        {
            items.Remove(key);
            items.Add(key, model);
        }

        public void RemoveOrderItem(int key)
        {
            items.Remove(key);
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

        public string DisplayName
        {
            get
            {
                if (this.name == "")
                    return "Order #" + id.ToString();
                else
                    return name;
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

        public Dictionary<int, OrderItemModel> OrderItems
        {
            get
            {
                return items;
            }
        }
    }
}
