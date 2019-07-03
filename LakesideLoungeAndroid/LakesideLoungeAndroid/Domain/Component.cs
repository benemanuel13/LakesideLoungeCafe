using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakesideLoungeAndroid.Domain
{
    public class Component
    {
        int id;
        int parentId;
        string name;
        string displayName;
        decimal cost;
        decimal price;
        bool isDefault;
        int portions;
        int group;
        float points;
        int position;

        Dictionary<int, Component> components = new Dictionary<int, Component>();

        public Component(int id, int parentId, string name, string displayName, decimal cost, decimal price, int portions, bool isDefault, int group, float points, int position)
        {
            this.id = id;
            this.parentId = parentId;
            this.name = name;
            this.displayName = displayName;
            this.cost = cost;
            this.price = price;
            this.portions = portions;
            this.isDefault = isDefault;
            this.group = group;
            this.points = points;
            this.position = position;
        }

        public Component AddComponent(int id, int parentId, string displayName, string name, decimal cost, decimal price, int portions, bool isDefault, int group, float points, int position)
        {
            Component newComponent = new Component(id, parentId, displayName, name, cost, price, portions, isDefault, group, points, position);
            components.Add(id, newComponent);

            return newComponent;
        }

        public Dictionary<int, Component> Components
        {
            get
            {
                return components;
            }
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

        public float Points
        {
            get
            {
                return points;
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
