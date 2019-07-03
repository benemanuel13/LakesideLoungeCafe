using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakesideLoungeAdmin.Domain
{
    public class OrderItemComponentComponent
    {
        private int orderItemComponentId;
        private int componentId;
        private int portions;

        private Component component;

        public OrderItemComponentComponent(int orderItemComponentId, int componentId, int portions)
        {
            this.orderItemComponentId = orderItemComponentId;
            this.componentId = componentId;
            this.portions = portions;

            component = new Component(componentId, portions);
        }

        public List<Ingredient> IngredientsUsed()
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            foreach(Ingredient ingredient in component.Ingredients)
            {
                Ingredient newIngredient = ingredient;
                newIngredient.Portions = newIngredient.Portions * portions;

                ingredients.Add(newIngredient);
            }

            return ingredients;
        }
    }
}
