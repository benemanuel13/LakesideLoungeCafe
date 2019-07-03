using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakesideLoungeAdmin.Domain
{
    public class Ingredient
    {
        private int id;
        private string description;
        private string displayName;
        //private decimal cost;
        private int portionSize;
        private int portionType;
        private int portions;

        public Ingredient(int id)
        {
            this.id = id;
        }
        
        public Ingredient(int id, string description, string displayName, int portionSize, int portionType, int portions)
        {
            this.id = id;
            this.description = description;
            this.displayName = displayName;
            this.portionSize = portionSize;
            this.portionType = portionType;
            this.portions = portions;
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
        }

        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        public int PortionSize
        {
            get
            {
                return portionSize;
            }
        }

        public int PortionType
        {
            get
            {
                return portionType;
            }
        }

        public int Portions
        {
            get
            {
                return portions;
            }

            set
            {
                portions = value;
            }
        }
    }
}
