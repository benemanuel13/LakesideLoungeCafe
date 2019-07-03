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
using LakesideLoungeAndroid.Infrastructure;

namespace LakesideLoungeAndroid.Application
{
    public class VariationEditModel
    {
        int id;
        int parentId;
        string name;
        string displayName;

        PricesModel model;

        public VariationEditModel(int id)
        {
            Variation variation = Database.GetVariation(id);

            this.id = id;
            this.parentId = variation.ParentId;
            this.name = variation.Name;
            this.displayName = variation.DisplayName;

            model = new PricesModel(id);
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

        public PricesModel Model
        {
            get
            {
                return model;
            }

            set
            {
                model = value;
            }
        }

        public List<PriceModel> Prices
        {
            get
            {
                return model.PriceModels;
            }
        }

        public static void SaveVariationEditModel(VariationEditModel model)
        {
            Database.SaveVariationEditModel(model);
        }
    }
}