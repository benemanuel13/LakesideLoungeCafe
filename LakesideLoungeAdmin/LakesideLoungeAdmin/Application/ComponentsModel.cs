using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAdmin.Domain;
using LakesideLoungeAdmin.Infrastructure;
using LakesideLoungeAdmin.Interfaces;

namespace LakesideLoungeAdmin.Application
{
    public class ComponentsModel : ItemModelBase
    {
        public ComponentsModel(int filterId = 0, bool filter = false)
        {
            children = Database.GetAllComponents(0, filterId);
            SortByName();
        }

        public ComponentsModel(int id, int parentId)
        {
            this.id = parentId;
            this.parentId = parentId;
            children = Database.GetAllComponents(parentId);
            SortByName();
        }

        public ComponentsModel(int id) : this(Database.GetVariation(id, true))
        { }

        public ComponentsModel(Variation variation)
        {
            this.id = variation.Id;
            this.parentId = variation.ParentId;
            this.name = variation.Name;
            this.displayName = variation.DisplayName;
            //this.cost = variation.Cost;
            this.price = variation.Price;

            children = new List<ItemModelBase>();

            if (variation.HasChildren() && variation.Children[0] is Component)
            {
                foreach (Component component in variation.Children)
                    children.Add(new ComponentModel(parentId, component));
            }

            SortByName();
        }

        public override bool HasChildren
        {
            get
            {
                return children.Count > 0;
            }
        }

        public void SortByPosition()
        {
            children.Sort();
        }

        public void SortByName()
        {
            children.Sort(CompareByName);
        }

        private static int CompareByName(ItemModelBase one, ItemModelBase two)
        {
            return string.Compare(one.Name, two.Name);
        }

        public override List<ItemModelBase> Children
        {
            get
            {
                return children;
            }
        }

        public override string ToString()
        {
            return name;
        }
    }
}
