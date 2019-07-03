using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakesideLoungeWebApi.Domain
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

        public Component(int id, int parentId, string name, string displayName, decimal cost, decimal price, int portions, bool isDefault = false)
        {
            this.id = id;
            this.parentId = parentId;
            this.name = name;
            this.displayName = displayName;
            this.cost = cost;
            this.price = price;
            this.portions = portions;
            this.isDefault = isDefault;
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
    }
}
