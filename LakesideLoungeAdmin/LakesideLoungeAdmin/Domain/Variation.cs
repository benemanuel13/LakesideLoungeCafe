using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAdmin.Infrastructure;

namespace LakesideLoungeAdmin.Domain
{
    public class Variation : ItemBase
    {
        private float points = 0.0F;
        private decimal pointPrice = 0.00M;
        private int vatStatus = 1;

        public Variation(int id)
        {
            this.id = id;
            ingredients = Database.GetIngredients(id, 0);
        }
        
        public Variation(int id, int parentId,string name, string displayName, decimal price, float points, decimal pointPrice, bool removed, int position, int vatStatus)
        {
            this.id = id;
            this.parentId = parentId;
            this.name = name;
            this.displayName = displayName;
            //this.cost = cost;
            this.price = price;
            this.removed = removed;
            this.position = position;

            this.points = points;
            this.pointPrice = pointPrice;
            this.vatStatus = vatStatus;
        }

        public Variation AddVariation(int id, int parentId, string name, string displayName, decimal price, float points, decimal pointPrice, bool removed, int position, int vatStatus)
        {
            Variation newVariation = new Variation(id, parentId, name, displayName, price, points, pointPrice, removed, position, vatStatus);

            children.Add(newVariation);

            return newVariation;
        }

        public void AddComponent(int id, string name, string displayName, decimal price, bool isDefault, float points, int position, int portions = 0, int group = 0, bool removed= false)
        {
            children.Add(new Component(id, name, displayName, price, isDefault, points, position, portions, group, removed));
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

        public int VATStatus
        {
            get
            {
                return vatStatus;
            }
        }
    }
}
