using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakesideLoungeAndroid.Domain
{
    public class Variation
    {
        int id;
        int parentId;
        string name;
        string displayName;
        decimal cost;
        decimal price;
        float points;
        decimal pointPrice;
        int position;

        Dictionary<int, Variation> variations = new Dictionary<int, Variation>();
        Dictionary<int, Component> components = new Dictionary<int, Component>();

        public Variation(int id, int parentId, string name, string displayName, decimal cost, decimal price, float points, decimal pointPrice, int position)
        {
            this.id = id;
            this.parentId = parentId;
            this.name = name;
            this.displayName = displayName;
            this.cost = cost;
            this.price = price;
            this.points = points;
            this.pointPrice = pointPrice;
            this.position = position;
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

        public float Points
        {
            get
            {
                return points;
            }
        }

        public decimal PointPrice
        {
            get
            {
                return pointPrice;
            }
        }

        public int Position
        {
            get
            {
                return position;
            }
        }

        public void AddVariation(int id, int parentId, string name, string displayName, decimal cost, decimal price, float points, decimal pointPrice, int position)
        {
            variations.Add(id, new Variation(id, parentId, name, displayName, cost, price, points, pointPrice, position));
        }

        public Component AddComponent(int id, int parentId, string displayName, string name, decimal cost, decimal price, int portions, bool isDefault, int group, float points, int position)
        {
            Component newComponent = new Component(id, parentId, displayName, name, cost, price, portions, isDefault, group, points, position);
            components.Add(id, newComponent);

            return newComponent;
        }

        public bool HasVariations()
        {
            return variations.Count > 0;
        }

        public bool HasComponents()
        {
            return components.Count > 0;
        }

        public Dictionary<int, Variation> Variations
        {
            get { return variations; }
        }

        public Dictionary<int, Component> Components
        {
            get { return components; }
        }
    }
}
