using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LakesideLoungeWebApi.Domain;

namespace LakesideLoungeWebApi.Application
{
    public class ComponentModel
    {
        int id;
        string name;
        string displayName;
        decimal cost;
        decimal price;
        int portions = 1;
        bool isDefault;

        public ComponentModel(Component component)
        {
            this.id = component.Id;
            this.name = component.Name;
            this.displayName = component.DisplayName;
            this.cost = component.Cost;
            this.price = component.Price;
            this.portions = component.Portions;
            this.isDefault = component.IsDefault;
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        public decimal Price
        {
            get
            {
                return price;
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

        public bool IsDefault
        {
            get
            {
                return isDefault;
            }
        }
    }
}