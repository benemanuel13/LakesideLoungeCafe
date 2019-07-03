using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using LakesideLoungeAndroid.Domain;

namespace LakesideLoungeAndroid.Application
{
    public class OrderItemComponentModel
    {
        int id = 0;
        int orderItemId;
        int componentId;
        string name;
        string displayName;
        decimal cost;
        decimal price;
        int portions = 1;
        int position = 0;

        List<OrderItemComponentComponentModel> components = new List<OrderItemComponentComponentModel>();

        public OrderItemComponentModel(int id, int orderItemId, int componentId, string name, string displayName, decimal cost, decimal price, int portions, int position)
        {
            this.id = id;
            this.orderItemId = orderItemId;
            this.componentId = componentId;
            this.name = name;
            this.displayName = displayName;
            this.cost = cost;
            this.price = price;
            this.portions = portions;
            this.position = position;
        }

        public OrderItemComponentModel(OrderItemComponent component)
        {
            this.id = component.Id;
            this.orderItemId = component.OrderItemId;
            this.componentId = component.ComponentId;
            this.name = component.Name;
            this.displayName = component.DisplayName;
            this.cost = component.Cost;
            this.price = component.Price;
            this.portions = component.Portions;
            this.position = component.Position;
            //foreach (OrderItemComponentComponent subComponent in component.Components)
            //    components.Add(subComponent.Id, new OrderItemComponentComponentModel(subComponent));
        }

        public OrderItemComponentModel Clone()
        {
            OrderItemComponentModel model = new OrderItemComponentModel(this.id, this.orderItemId, this.componentId, this.name, this.displayName, this.cost, this.price, this.portions, this.position);

            foreach(OrderItemComponentComponentModel component in components)
                model.AddComponent(component);

            return model;
        }

        public void AddComponent(OrderItemComponentComponentModel component)
        {
            components.Add(component);
        }

        public bool HasComponents
        {
            get
            {
                return components.Count > 0;
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
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        public decimal Price
        {
            get
            {
                return price;
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

        public int Position
        {
            get
            {
                return position;
            }
        }
        public List<OrderItemComponentComponentModel> Components
        {
            get
            {
                return components;
            }
        }
    }
}