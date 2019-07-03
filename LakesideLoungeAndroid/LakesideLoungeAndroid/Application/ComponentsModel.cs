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

using LakesideLoungeAndroid.Presentation.Fragments;
using LakesideLoungeAndroid.Domain;
using LakesideLoungeAndroid.Infrastructure;

namespace LakesideLoungeAndroid.Application
{
    public class ComponentsModel
    {
        List<ComponentModel> componentModels = new List<ComponentModel>();

        public ComponentsModel(int id, ComponentListMode mode)
        {
            if (mode == ComponentListMode.Variation)
            {
                Variation variation = Database.GetVariation(id);

                foreach (Component component in variation.Components.Values)
                {
                    ComponentModel newComponentModel = new ComponentModel(component);
                    componentModels.Add(newComponentModel);
                }

                componentModels.Sort();
            }
            else
            {
                Component component = Database.GetComponent(id);

                foreach (Component subComponent in component.Components.Values)
                {
                    ComponentModel newComponentModel = new ComponentModel(subComponent);
                    componentModels.Add(newComponentModel);
                }

                componentModels.Sort();
            }
        }

        public List<ComponentModel> ComponentModels
        {
            get
            {
                return componentModels;
            }
        }
    }
}