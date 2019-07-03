using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;

namespace LakesideLoungeAdmin.Presentation.Controls
{
    public class LabelWithId : Label
    {
        private int id;

        public LabelWithId(int id)
        {
            this.id = id;
        }

        public int Id
        {
            get
            {
                return id;
            }
        }
    }
}
