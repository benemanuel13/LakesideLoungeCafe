using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakesideLoungeAdmin.Domain
{
    public abstract class ItemBase
    {
        protected int id;
        protected int parentId;
        protected string name;
        protected string displayName;
        //protected decimal cost;
        protected decimal price;
        protected bool removed;
        protected int position;

        protected List<ItemBase> children = new List<ItemBase>();
        protected List<Ingredient> ingredients;

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

        //public decimal Cost
        //{
        //    get
        //    {
        //        return cost;
        //    }
        //}

        public decimal Price
        {
            get
            {
                return price;
            }
        }

        public bool HasChildren()
        {
            return children.Count > 0;
        }

        public List<ItemBase> Children
        {
            get
            {
                return children;
            }
        }

        public List<Ingredient> Ingredients
        {
            get
            {
                return ingredients;
            }

            set
            {
                ingredients = value;
            }
        }

        public bool Removed
        {
            get
            {
                return removed;
            }

            set
            {
                removed = value;
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
