using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakesideLoungeKitchenAndroid.Domain
{
    public class Variation
    {
        int id;
        int parentId;
        string name;
        string displayName;
        //decimal cost;
        //decimal price;

        Dictionary<int, Variation> variations = new Dictionary<int, Variation>();
        Dictionary<int, Component> components = new Dictionary<int, Component>();

        public Variation(int id, int parentId, string name, string displayName)
        {
            this.id = id;
            this.parentId = parentId;
            this.name = name;
            this.displayName = displayName;
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

        //public void AddVariation(int id, int parentId, string name, string displayName, decimal cost, decimal price)
        //{
        //    variations.Add(id, new Variation(id, parentId, name, displayName, cost, price));
        //}

        public void AddComponent(int id, int parentId, string displayName, string name, int portions)
        {
            components.Add(id, new Component(id, displayName, name, portions));
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
