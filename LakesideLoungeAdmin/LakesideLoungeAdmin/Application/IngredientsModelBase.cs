using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAdmin.Interfaces;

namespace LakesideLoungeAdmin.Application
{
    public class IngredientsModelBase : IListAble<IngredientsModelBase>
    {
        protected List<IngredientsModelBase> children = new List<IngredientsModelBase>();

        public virtual int Id
        {
            get
            {
                return -1;
            }
        }

        public int ParentId
        {
            get
            {
                return -1;
            }
        }

        public virtual string Description
        {
            get
            {
                return null;
            }
        }

        public bool Subselected
        {
            get
            {
                return false;
            }
        }

        public bool HasChildren
        {
            get
            {
                return children.Count > 0;
            }
        }

        public virtual List<IngredientsModelBase> Children
        {
            get
            {
                return children;
            }

            set
            { }
        }

        public virtual bool ShowIcon
        {
            get
            {
                return false;
            }

            set
            { }
        }
    }
}
