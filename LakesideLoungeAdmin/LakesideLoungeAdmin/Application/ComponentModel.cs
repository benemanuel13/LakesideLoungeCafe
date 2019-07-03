using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAdmin.Domain;
using LakesideLoungeAdmin.Interfaces;
using LakesideLoungeAdmin.Infrastructure;

namespace LakesideLoungeAdmin.Application
{
    public class ComponentModel : ItemModelBase, IListAble<ItemModelBase>
    {
        private bool isDefault = false;
        private int portions;
        private int group;
        private float points;

        public ComponentModel(int parentId, int id, bool getComponentComponent)
        {
            Component component = Database.GetSubComponent(parentId, id);
            group = component.Group;
            position = component.Position;
        }

        public ComponentModel(int parentId, int id) : this(parentId, Database.GetComponent(id))
        { }

        public ComponentModel(int parentId, Component component)
        {
            this.id = component.Id;
            this.parentId = parentId;
            this.name = component.Name;
            this.displayName = component.DisplayName;
            this.isDefault = component.IsDefault;
            this.price = component.Price;
            this.portions = component.Portions;
            this.group = component.Group;
            this.points = component.Points;
            this.showIcon = component.Removed;
            this.position = component.Position;

            children = new List<ItemModelBase>();

            foreach (Component subComponent in component.Children)
                children.Add(new ComponentModel(component.Id, subComponent));
        }

        public bool IsDefault
        {
            get
            {
                return isDefault;
            }

            set
            {
                isDefault = value;
            }
        }

        public override bool Subselected
        {
            get
            {
                return isDefault;
            }
        }
        public override bool HasChildren
        {
            get
            {
                return children.Count > 0;
            }
        }

        public override List<ItemModelBase> Children
        {
            get
            {
                return children;
            }
        }

        public float Points
        {
            get
            {
                return points;
            }
        }

        public int Portions
        {
            get
            {
                return portions;
            }
        }

        public int Group
        {
            get
            {
                return group;
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
    }
}
