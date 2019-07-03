﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAdmin.Interfaces;

namespace LakesideLoungeAdmin.Presentation.EventArgs
{
    public class ItemRightClickedEventArgs<T> : System.EventArgs where T : IListAble<T>
    {
        T child;

        public ItemRightClickedEventArgs(T child)
        {
            this.child = child;
        }

        public int Id
        {
            get
            {
                return child.Id;
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
