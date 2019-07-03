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

using LakesideLoungeKitchenAndroid.Domain;

namespace LakesideLoungeKitchenAndroid.Application
{
    public class OrderItemComponentModel : IComparable<OrderItemComponentModel>
    {
        int id;
        int orderItemId;
        int componentId;
        string name;
        string displayName;
        int portions;
        int position;

        List<OrderItemComponentComponentModel> components = new List<OrderItemComponentComponentModel>();

        public OrderItemComponentModel(OrderItemComponent component)
        {
            this.id = component.Id;
            this.name = component.Name;
            this.displayName = component.DisplayName;
            this.orderItemId = component.ParentId;
            this.componentId = component.ComponentId;
            this.portions = component.Portions;
            this.position = component.Position;
        }

        public OrderItemComponentModel(int id, string name, string displayName, int orderItemId, int componentId, int portions, int position)
        {
            this.id = id;
            this.name = name;
            this.displayName = displayName;
            this.orderItemId = orderItemId;
            this.componentId = componentId;
            this.portions = portions;
            this.position = position;
        }

        public void AddComponent(OrderItemComponentComponentModel component)
        {
            components.Add(component);
        }

        public int CompareTo(OrderItemComponentModel other)
        {
            if (position > other.Position)
                return 1;
            else if (position < other.Position)
                return -1;

            return 0;
        }

        public List<OrderItemComponentComponentModel> Components
        {
            get
            {
                return components;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public int OrderItemId
        {
            get
            {
                return orderItemId;
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

        public int Portions
        {
            get
            {
                return portions;
            }
        }

        public int Position
        {
            get
            {
                return position;
            }
        }
    }
}