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

using LakesideLoungeKitchenAndroid.Infrastructure;

namespace LakesideLoungeKitchenAndroid.Application
{
    public class OverallLayoutService
    {
        public void InitializeDatabase(Context context)
        {
            Database.InitializeDatabase(context);
        }

        public ComponentModel GetComponentModel(int id)
        {
            return Database.GetComponentModel(id);
        }

        public void SaveOrder(OrderModel model)
        {
            Database.SaveOrder(model);
        }

        public void AddVariation(int id, int parentId, string name, string displayName, decimal price, float points, decimal pointPrice)
        {
            Database.AddVariation(id, parentId, name, displayName, price, points, pointPrice);
        }

        public void UpdateVariation(int id, int parentId, string name, string displayName, float points, decimal pointPrice)
        {
            Database.UpdateVariation(id, parentId, name, displayName, points, pointPrice);
        }

        public void AddComponent(int id, string name, string displayName)
        {
            Database.AddComponent(id, name, displayName);
        }

        public void UpdateComponent(int id, string name, string displayName)
        {
            Database.UpdateComponent(id, name, displayName);
        }
    }
}