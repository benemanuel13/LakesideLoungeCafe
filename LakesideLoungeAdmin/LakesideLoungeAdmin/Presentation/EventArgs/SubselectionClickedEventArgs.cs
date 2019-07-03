using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakesideLoungeAdmin.Presentation.EventArgs
{
    public class SubselectionClickedEventArgs : System.EventArgs
    {
        private int id;
        private int parentId;
        private bool isChecked;

        public SubselectionClickedEventArgs(int id, int parentId, bool isChecked)
        {
            this.id = id;
            this.parentId = parentId;
            this.isChecked = isChecked;
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
    }
}
