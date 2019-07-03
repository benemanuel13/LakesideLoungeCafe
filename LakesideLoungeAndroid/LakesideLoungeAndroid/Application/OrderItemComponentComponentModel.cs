using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using LakesideLoungeAndroid.Domain;

namespace LakesideLoungeAndroid.Application
{
    public class OrderItemComponentComponentModel
    {
        //int id;
        int parentId;
        int componentId;
        string name;
        string displayName;
        decimal cost;
        decimal price;
        int portions = 1;
        bool isDefault;

        public OrderItemComponentComponentModel(OrderItemComponentComponent component)
        {
            //id = component.Id;
            parentId = component.ParentId;
            componentId = component.ComponentId;
            name = component.Name;
            displayName = component.DisplayName;
            cost = component.Cost;
            price = component.Price;
            portions = component.Portions;
        }

        public OrderItemComponentComponentModel(int parentId, int componentId, string name, string displayName, decimal cost, decimal price, int portions, bool isDefault)
        {
            //this.id = id;
            this.parentId = parentId;
            this.componentId = componentId;
            this.name = name;
            this.displayName = displayName;
            this.cost = cost;
            this.price = price;
            this.portions = portions;
            this.isDefault = isDefault;
        }

        //public int Id
        //{
        //    get
        //    {
        //        return id;
        //    }

        //    set
        //    {
        //        id = value;
        //    }
        //}

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