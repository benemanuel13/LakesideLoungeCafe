using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakesideLoungeAdmin.Presentation.EventArgs
{
    public class ItemUpdatedEventArgs : System.EventArgs
    {
        private string text;

        public ItemUpdatedEventArgs(string text)
        {
            this.text = text;
        }

        public string Text
        {
            get
            {
                return text;
            }
        }
    }
}
