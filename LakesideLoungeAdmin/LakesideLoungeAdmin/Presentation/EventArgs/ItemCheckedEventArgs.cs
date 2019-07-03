using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAdmin.Application;

namespace LakesideLoungeAdmin.Presentation.EventArgs
{
    public class ItemCheckedEventArgs<T> : System.EventArgs
    {
        private int id;
        private int parentId;
        private bool isChecked;
        private T child;

        public ItemCheckedEventArgs()
        { }

        public ItemCheckedEventArgs(int id, int parentId, bool isChecked, T child)
        {
            this.id = id;
            this.parentId = parentId;
            this.isChecked = isChecked;
            this.child = child;
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

        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
        }

        public T Child
        {
            get
            {
                return child;
            }
        }
    }
}
