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

namespace LakesideLoungeKitchenAndroid.Domain
{
    public class OrderItemComponent
    {
        int id;
        int componentId;
        int parentId;
        string name;
        string displayName;
        int portions;
        int position;

        List<OrderItemComponentComponent> components = new List<OrderItemComponentComponent>();

        public OrderItemComponent (int id, int componentId, int parentId, string name, string displayName, int portions, int position)
        {
            this.id = id;
            this.componentId = componentId;
            this.parentId = parentId;
            this.name = name;
            this.displayName = displayName;
            this.portions = portions;
            this.position = position;
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
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public int ComponentId
        {
            get
            {
                return componentId;
            }
        }

        public int ParentId
        {
            get
            {
                return parentId;
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