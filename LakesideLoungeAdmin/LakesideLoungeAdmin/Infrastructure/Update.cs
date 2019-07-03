﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakesideLoungeAdmin.Infrastructure
{
    public class Update
    {
        int id;
        string updateText;

        public Update()
        { }

        public Update(int id, string updateText)
        {
            this.id = id;
            this.updateText = updateText;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string UpdateText
        {
            get
            {
                return updateText;
            }

            set
            {
                updateText = value;
            }
        }
    }
}
