using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAndroid.Domain;
using LakesideLoungeAndroid.Infrastructure;

namespace LakesideLoungeAndroid.Application
{
    public class OrderModel
    {
        int id = 0;
        //int orderNumber = 0;
        bool live;
        string name = "";
        int customerType;
        DateTime date;
        bool isVoid = false;
        int orderNumber = 0;

        List<OrderItemModel> items = new List<OrderItemModel>();

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
            this.orderNumber = order.OrderNumber;

            foreach (OrderItem item in order.OrderItems)
            {
                OrderItemModel newItemModel = new OrderItemModel(item.Id, id, item.VariationId, item.InOutStatus, item.DiscountId, item.State);
                items.Add(newItemModel);

                foreach (OrderItemComponent component in item.Components)
                {
                    OrderItemComponentModel componentModel = new OrderItemComponentModel(component);
                    newItemModel.AddComponentModel(componentModel);

                    foreach (OrderItemComponentComponent subComponent in component.Components)
                        componentModel.AddComponent(new OrderItemComponentComponentModel(subComponent));
                }
            }
        }

        public OrderModel(int id, bool live, int orderNumber)
        {
            this.id = id;
            this.orderNumber = orderNumber;
            this.live = live;
            this.customerType = 1;
            this.date = DateTime.Now;
        }

        public OrderModel(int id, string name, int customerType, DateTime date, int orderNumber)
        {
            this.id = id;
            this.name = name;
            this.customerType = customerType;
            this.date = date;
            this.orderNumber = orderNumber;
        }

        public void AddOrderItemModel(OrderItemModel model)
        {
            items.Add(model);
        }

        public void UpdateOrderItemModel(int position, OrderItemModel model)
        {
            items.RemoveAt(position);
            items.Insert(position, model);
        }

        public void RemoveOrderItem(int position)
        {
            items.RemoveAt(position);
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
                    return "Order #" + orderNumber;
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

        public bool Void
        {
            get
            {
                return isVoid;
            }

            set
            {
                isVoid = value;
            }
        }

        public List<OrderItemModel> OrderItems
        {
            get
            {
                return items;
            }
        }

        public static OrderModel GetCurrentOrderModel()
        {
            return new OrderModel(Database.GetCurrentOrderId());
        }

        public static OrderModel GetFirstLiveOrderModel()
        {
            return new OrderModel(Database.GetFirstLiveOrderModelId());
        }

        public decimal TotalPrice()
        {
            decimal totalPrice = 0;

            foreach (OrderItemModel model in items)
                totalPrice += model.Price;

            return totalPrice;
        }
    }
}
