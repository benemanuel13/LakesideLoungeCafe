using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAdmin.Interfaces;

namespace LakesideLoungeAdmin.Presentation.Adapters
{
    public class IconViewAdapter<T> where T : IListAble<T>
    {
        private T content;
        private T selectionContent;
        private bool hasSelections = false;
        private bool subSelectable = false;

        public IconViewAdapter(T content, T selectionContent)
        {
            this.content = content;
            this.selectionContent = selectionContent;
        }

        public List<T> Items
        {
            get
            {
                return content.Children;
            }
        }

        public List<T> SelectedItems
        {
            get
            {
                return selectionContent.Children;
            }
        }

        public bool HasSelections
        {
            get
            {
                return hasSelections;
            }

            set
            {
                hasSelections = value;
            }
        }

        public bool Subselectable
        {
            get
            {
                return subSelectable;
            }

            set
            {
                subSelectable = value;
            }
        }
    }
}
