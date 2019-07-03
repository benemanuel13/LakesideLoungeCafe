using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAdmin.Domain;
using LakesideLoungeAdmin.Infrastructure;

namespace LakesideLoungeAdmin.Application
{
    public class ItemsViewService
    {
        //public VariationModel GetVariationModel(int id)
        //{
        //    return new VariationModel(id);
        //}

        public int AddNewVariation(int parentId, string name, string displayName, int position, int vatStatus)
        {
            int id = Database.GetNewVariationId();
            Database.SaveVariation(id, parentId, name, displayName, position, vatStatus);

            return id;
        }

        public void AddUpdate(string updateText)
        {
            Database.SaveUpdate(updateText);
        }
        
        public void SetComponentSelected(int parentId, int componentId, int position)
        {
            Database.SetComponentSelected("Variation", parentId, componentId, position);
            Log.AddVariationComponent(parentId, componentId);
        }

        public void SetComponentDeselected(int parentId, int componentId, int position)
        {
            Database.SetComponentDeselected("Variation", parentId, componentId, position);
            Log.RemoveVariationComponent(parentId, componentId);

            Database.SetComponentUnDefault("Variation", parentId, componentId);
        }

        public void SetComponentDefault(int parentId, int componentId)
        {
            Database.SetComponentDefault("Variation", parentId, componentId);
            Log.UpdateVariationComponent(parentId, componentId, true);
        }

        public void SetComponentUnDefault(int parentId, int componentId)
        {
            Database.SetComponentUnDefault("Variation", parentId, componentId);
            Log.UpdateVariationComponent(parentId, componentId, false);
        }

        public ComponentModel GetComponentModel(int parentId, int childId, out bool found)
        {
            ComponentModel model;

            Variation varModel = Database.GetVariation(parentId, true);

            if (varModel.Children.Where(c => c.Id == childId).Count() > 0)
            {
                model = new ComponentModel(parentId, (Component)varModel.Children.Where(c => c.Id == childId).First());
                found = true;
            }
            else
            {
                model = new ComponentModel(parentId, childId);
                found = false;
            }

            return model;
        }

        public void RemoveItem(int id)
        {
            Database.AddRemoveVariation(id, true);
        }

        public void ReinstateItem(int id)
        {
            Database.AddRemoveVariation(id, false);
        }

        public void DeleteVariation(VariationModel model)
        {
            Database.DeleteVariation(model);
        }

        public void SwapComponentPositions(int parentId, ComponentModel model1, ComponentModel model2)
        {
            Database.SwapComponents("Variation", parentId, model1, model2);
            Database.SaveUpdate("SWAP_VARIATION_COMPONENT_POSITIONS," + parentId + "," + model1.Id + "," + model1.Position + "," + model2.Id + "," + model2.Position);
        }

        public void SwapVariationPositions(VariationModel model1, VariationModel model2)
        {
            Database.SwapVariationPositions(model1, model2);
            Database.SaveUpdate("SWAP_VARIATION_POSITIONS," + model1.Id + "," + model1.Position + "," + model2.Id + "," + model2.Position);
        }
    }
}
