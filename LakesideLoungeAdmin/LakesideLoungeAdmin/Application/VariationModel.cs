using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAdmin.Interfaces;
using LakesideLoungeAdmin.Domain;
using LakesideLoungeAdmin.Infrastructure;

namespace LakesideLoungeAdmin.Application
{
    public class VariationModel : ItemModelBase, IListAble<ItemModelBase>
    {
        private float points;
        private decimal pointPrice;
        private int vatStatus;

        public VariationModel(int id) : this(Database.GetVariation(id, true), true, false)
        {
        }

        public VariationModel(int id, bool reallyDeep) : this(Database.GetVariation(id, true), true, true)
        {

        }

        public VariationModel(Variation variation, bool deep, bool reallyDeep)
        {
            this.id = variation.Id;
            this.parentId = variation.ParentId;
            this.name = variation.Name;
            this.displayName = variation.DisplayName;
            //this.cost = variation.Cost;
            this.price = variation.Price;
            this.points = variation.Points;
            this.pointPrice = variation.PointPrice;
            this.showIcon = variation.Removed;
            this.position = variation.Position;
            this.vatStatus = variation.VATStatus;

            children = new List<ItemModelBase>();

            if (deep)
            {
                if (variation.HasChildren() && variation.Children[0] is Variation)
                {
                    foreach (Variation subVariation in variation.Children)
                    {
                        if (!reallyDeep)
                            children.Add(new VariationModel(subVariation, false, false));
                        else
                            children.Add(new VariationModel(subVariation.Id, true));
                    }
                    SortByPosition();
                }
                else if (variation.HasChildren() && variation.Children[0] is Component)
                {
                    ComponentsModel model = new ComponentsModel(variation);
                    children = model.Children;
                }
            }
        }

        public override bool HasChildren
        {
            get
            {
                return children.Count > 0;
            }
        }

        public bool HasVariationChildren
        {
            get
            {
                if(children.Count > 0)
                    return children[0] is VariationModel;

                return false;
            }
        }

        public bool HasComponentChildren
        {
            get
            {
                if (children.Count > 0)
                    return children[0] is ComponentModel;

                return false;
            }
        }

        public void SortByPosition()
        {
            children.Sort();
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

        public decimal PointPrice
        {
            get
            {
                return pointPrice;
            }
        }

        public int VATStatus
        {
            get
            {
                return vatStatus;
            }
        }

        public override string ToString()
        {
            return name;
        }
    }
}
