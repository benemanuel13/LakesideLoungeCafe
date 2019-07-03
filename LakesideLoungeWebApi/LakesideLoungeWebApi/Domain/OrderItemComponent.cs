using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LakesideLoungeWebApi.Domain
{
    public class OrderItemComponent
    {
        int id;
        int variationId;
        string name;
        string displayName;
        int orderItemId;
        int componentId;
        decimal cost;
        decimal price;
        bool isDefault;
        int portions = 1;

        List<OrderItemComponentComponent> components = new List<OrderItemComponentComponent>();

        public OrderItemComponent()
        {

        }

        public OrderItemComponent(int id, int variationId, string name, string displayName, int orderItemId, int componentId, decimal cost, decimal price, int portions, bool isDefault)
        {
            this.id = id;
            this.variationId = variationId;
            this.name = name;
            this.displayName = displayName;
            this.orderItemId = orderItemId;
            this.componentId = componentId;
            this.cost = cost;
            this.price = price;
            this.isDefault = isDefault;
            this.portions = portions;
        }

        public void AddComponent(OrderItemComponentComponent component)
        {
            components.Add(component);
        }

        public List<OrderItemComponentComponent> Components
        {
            get
            {
                return components;
            }

            set
            {
                components = value;
            }
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
                return displayName;
            }

            set
            {
                displayName = value;
            }
        }
        public int OrderItemId
        {
            get
            {
                return orderItemId;
            }

            set
            {
                orderItemId = value;
            }
        }

        public int ComponentId
        {
            get
            {
                return componentId;
            }

            set
            {
                componentId = value;
            }
        }

        public decimal Cost
        {
            get
            {
                return cost;
            }

            set
            {
                cost = value;
            }
        }

        public decimal Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }

        public int Portions
        {
            get
            {
                return portions;
            }

            set
            {
                portions = value;
            }
        }

        public bool IsDefault
        {
            get
            {
                return isDefault;
            }

            set
            {
                isDefault = value;
            }
        }
    }
}