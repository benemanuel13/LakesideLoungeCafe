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
    public class ComponentModel
    {
        int id;
        int orderItemId;
        string name;
        string displayName;
        int portions = 1;

        public ComponentModel(int id, int orderItemId, string name, string displayName, int portions)
        {
            this.id = id;
            this.orderItemId = orderItemId;
            this.name = name;
            this.displayName = displayName;
            this.portions = portions;
        }

        public ComponentModel(Component component)
        {
            this.id = component.Id;
            this.name = component.Name;
            this.displayName = component.DisplayName;
            this.portions = component.Portions;
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