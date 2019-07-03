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
    public class IngredientModel : IngredientsModelBase, IListAble<IngredientsModelBase>
    {
        int id;
        string description;
        string displayName;
        int portionSize;
        int portionType;
        int portions;
        bool showIcon = false;

        public IngredientModel(int id) : this(Database.GetIngredient(id))
        { }

        public IngredientModel(Ingredient ingredient) : this(ingredient.Id, ingredient.Description, ingredient.DisplayName, ingredient.PortionSize, ingredient.PortionType ,ingredient.Portions)
        { }

        public IngredientModel(int id, string description, string displayName, int portionSize, int portionType, int portions)
        {
            this.id = id;
            this.description = description;
            this.displayName = displayName;
            this.portionSize = portionSize;
            this.portionType = portionType;
            this.portions = portions;
        }

        public override int Id
        {
            get
            {
                return id;
            }
        }
        public override string Description
        {
            get
            {
                return description;
            }
        }

        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        public int PortionSize
        {
            get
            {
                return portionSize;
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

        public int PortionType
        {
            get
            {
                return portionType;
            }
        }

        public override bool ShowIcon
        {
            get
            {
                return showIcon;
            }

            set
            {
                showIcon = value;
            }
        }
    }
}
