using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAdmin.Infrastructure;

namespace LakesideLoungeAdmin.Domain
{
    public class Component : ItemBase
    {
        private bool isDefault = false;
        private int portions;
        private int group;
        private float points;
        //private int position;

        public Component(int id, int portions)
        {
            this.id = id;
            this.portions = portions;
            ingredients = Database.GetIngredients(id, 1);
        }

        public Component(int id, string name, string displayName, decimal price, bool isDefault, float points, int position, int portions = 0, int group = 0, bool removed = false)
        {
            this.id = id;
            this.name = name;
            this.displayName = displayName;
            //this.cost = cost;
            this.price = price;
            this.isDefault = isDefault;
            this.points = points;
            this.position = position;
            this.portions = portions;
            this.group = group;
            this.removed = removed;
        }

        public void AddComponent(Component component)
        {
            children.Add(component);
        }

        public bool IsDefault
        {
            get
            {
                return isDefault;
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
    }
}
