using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakesideLoungeAdmin.Presentation.EventArgs
{
    public class ProgressChangedEventArgs
    {
        private int newValue = 0;

        public ProgressChangedEventArgs(int newValue)
        {
            this.newValue = newValue;
        }

        public int NewValue
        {
            get
            {
                return newValue;
            }
        }
    }
}
