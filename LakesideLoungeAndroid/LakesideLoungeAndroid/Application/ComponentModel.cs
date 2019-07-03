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
using LakesideLoungeAndroid.Infrastructure;

namespace LakesideLoungeAndroid.Application
{
    public class ComponentModel : IComparable<ComponentModel>
    {
        int id;
        string name;
        string displayName;
        decimal cost;
        decimal price;
        int portions = 1;
        bool isDefault;
        int group = 0;
        int position = 1;

        Dictionary<int, ComponentModel> components = new Dictionary<int, ComponentModel>();

        public ComponentModel(Component component)
        {
            this.id = component.Id;
            this.name = component.Name;
            this.displayName = component.DisplayName;
            this.cost = component.Cost;
            this.price = component.Price;
            this.portions = component.Portions;
            this.isDefault = component.IsDefault;
            this.group = component.Group;
            this.position = component.Position;

            foreach (Component componentItem in component.Components.Values)
                components.Add(componentItem.Id, new ComponentModel(componentItem));
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
        }

        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        public decimal Cost
        {
            get
            {
                return cost;
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

        public bool IsDefault
        {
            get
            {
                return isDefault;
            }
        }

        public int Group
        {
            get
            {
                return group;
            }
        }

        public bool HasComponents
        {
            get
            {
                return components.Count > 0;
            }
        }

        public Dictionary<int, ComponentModel> Components
        {
            get
            {
                return components;
            }
        }

        public int Position
        {
            get
            {
                return position;
            }
        }

        public int CompareTo(ComponentModel other)
        {
            if (position > other.Position)
                return 1;
            else if (position < other.Position)
                return -1;

            return 0;
        }
    }
}