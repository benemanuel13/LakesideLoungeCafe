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

using LakesideLoungeKitchenAndroid.Domain;

namespace LakesideLoungeKitchenAndroid.Application
{
    public class OrderItemComponentComponentModel
    {
        int parentId;
        int componentId;
        string name;
        string displayName;
        int portions = 1;

        public OrderItemComponentComponentModel(OrderItemComponentComponent component)
        {
            parentId = component.ParentId;
            componentId = component.ComponentId;
            name = component.Name;
            displayName = component.DisplayName;
            portions = component.Portions;
        }

        public OrderItemComponentComponentModel(int parentId, int componentId, string name, string displayName, int portions)
        {
            this.parentId = parentId;
            this.componentId = componentId;
            this.name = name;
            this.displayName = displayName;
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

        //public bool IsDefault
        //{
        //    get
        //    {
        //        return isDefault;
        //    }
        //}
    }
}