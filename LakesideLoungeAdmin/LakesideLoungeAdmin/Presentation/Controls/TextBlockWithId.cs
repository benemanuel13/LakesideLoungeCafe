﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;

namespace LakesideLoungeAdmin.Presentation.Controls
{
    public class TextBlockWithId : TextBlock
    {
        private int id;
        private int position;

        public TextBlockWithId(int id, int position)
        {
            this.id = id;
            this.position = position;
        }

        public int Id
        {
            get
            {
                return id;
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
