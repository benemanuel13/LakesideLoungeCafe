using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAdmin.Infrastructure;

namespace LakesideLoungeAdmin.Application
{
    public class ComponentsViewService
    {
        public void SetComponentSelected(int parentId, int componentId, int position)
        {
            Database.SetComponentSelected("Component", parentId, componentId, position);
            //Log.AddVariationComponent(parentId, componentId);
        }

        public void SetComponentDeselected(int parentId, int componentId, int position)
        {
            Database.SetComponentDeselected("Component", parentId, componentId, position);
            //Log.RemoveVariationComponent(parentId, componentId);
            Database.SetComponentUnDefault("Component", parentId, componentId);
        }

        public void SetComponentDefault(int parentId, int componentId)
        {
            Database.SetComponentDefault("Component", parentId, componentId);
            //Log.UpdateVariationComponent(parentId, componentId, true);
        }

        public void SetComponentUnDefault(int parentId, int componentId)
        {
            Database.SetComponentUnDefault("Component", parentId, componentId);
            //Log.UpdateVariationComponent(parentId, componentId, false);
        }

        public int AddNewComponent(string name, string displayName)
        {
            int id = Database.GetNewComponentId();
            Database.SaveComponent(id, name, displayName);

            return id;
        }

        public void AddUpdate(string updateText)
        {
            Database.SaveUpdate(updateText);
        }

        public void RemoveItem(int id)
        {
            Database.AddRemoveComponent(id, true);
        }

        public void ReinstateItem(int id)
        {
            Database.AddRemoveComponent(id, false);
        }

        public void SwapComponentPositions(int parentId, ComponentModel model1, ComponentModel model2)
        {
            Database.SwapComponents("Component", parentId, model1, model2);
            Database.SaveUpdate("SWAP_COMPONENT_COMPONENT_POSITIONS," + parentId + "," + model1.Id + "," + model1.Position + "," + model2.Id + "," + model2.Position);
        }
    }
}
