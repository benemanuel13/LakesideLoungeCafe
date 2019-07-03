using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAdmin.Interfaces;

namespace LakesideLoungeAdmin.Application
{
    public abstract class ItemModelBase : IListAble<ItemModelBase>, IComparable<ItemModelBase>
    {
        protected int id;
        protected int parentId;
        protected string name;
        protected string displayName;
        protected decimal cost;
        protected decimal price;
        protected bool showIcon = true;
        protected int position;

        protected List<ItemModelBase> children;

        public virtual int Id
        {
            get
            {
                return id;
            }
        }

        public virtual int ParentId
        {
            get
            {
                return parentId;
            }
        }

        public string Description
        {
            get
            {
                return name;
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

        public decimal Price
        {
            get
            {
                return price;
            }
        }

        public virtual bool Subselected
        {
            get
            {
                return false;
            }
        }
        public virtual bool HasChildren
        {
            get
            {
                return false;
            }
        }

        public virtual List<ItemModelBase> Children
        {
            get
            {
                return null;
            }
        }

        public bool ShowIcon
        {
            get
            {
                return showIcon;
            }

            set
            {
                showIcon = value;
            }
        }

        public int Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public int CompareTo(ItemModelBase other)
        {
            if (position > other.Position)
                return 1;
            else if (position < other.Position)
                return -1;

            return 0;
        }
    }
}
