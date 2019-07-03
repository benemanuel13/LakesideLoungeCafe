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

namespace LakesideLoungeKitchenAndroid.Domain
{
    public class OrderItemComponentComponent
    {
        int parentId;
        int componentId;
        string name;
        string displayName;
        int portions = 1;

        public OrderItemComponentComponent(int parentId, int componentId, string name, string displayName, int portions)
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
    }
}