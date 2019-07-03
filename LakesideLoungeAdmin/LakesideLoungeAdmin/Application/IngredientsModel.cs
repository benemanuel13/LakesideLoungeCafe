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
    public class IngredientsModel : IngredientsModelBase, IListAble<IngredientsModelBase>
    {
        public IngredientsModel()
        {
            List<Ingredient> ingredients = Database.GetIngredients();

            foreach (Ingredient ingredient in ingredients)
                children.Add(new IngredientModel(ingredient));
        }

        public IngredientsModel(bool popuplate)
        { }

        public IngredientsModel(int id, int parentType)
        {
            List<Ingredient> ingredients = Database.GetIngredients(id, parentType);

            foreach (Ingredient ingredient in ingredients)
                children.Add(new IngredientModel(ingredient));
        }

        public override List<IngredientsModelBase> Children
        {
            set
            {
                children = value;
            }
        }
    }
}
