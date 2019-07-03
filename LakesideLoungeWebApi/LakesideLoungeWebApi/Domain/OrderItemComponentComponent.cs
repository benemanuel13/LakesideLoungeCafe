using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LakesideLoungeWebApi.Domain
{
    public class OrderItemComponentComponent
    {
        //int id;
        int parentId;
        int componentId;
        string name;
        string displayName;
        decimal cost;
        decimal price;
        int portions = 1;

        public OrderItemComponentComponent()
        {
        }

        public OrderItemComponentComponent(int parentId, int componentId, string name, string displayName, decimal cost, decimal price, int portions)
        {
            //this.id = id;
            this.parentId = parentId;
            this.componentId = componentId;
            this.name = name;
            this.displayName = displayName;
            this.cost = cost;
            this.price = price;
            this.portions = portions;
        }

        public int ParentId
        {
            get
            {
                return parentId;
            }

            set
            {
                parentId = value;
            }
        }

        public int ComponentId
        {
            get
            {
                return componentId;
            }

            set
            {
                componentId = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string DisplayName
        {
            get
            {
                return displayName;
            }

            set
            {
                displayName = value;
            }
        }

        public decimal Cost
        {
            get
            {
                return cost;
            }

            set
            {
                cost = value;
            }
        }

        public decimal Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
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