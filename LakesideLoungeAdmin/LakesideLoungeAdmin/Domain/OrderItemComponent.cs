using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakesideLoungeAdmin.Domain
{
    public class OrderItemComponent
    {
        private int id;
        private int orderItemId;
        private int variationId;
        private int componentId;
        private int portions;

        private Component component;

        private List<OrderItemComponentComponent> components = new List<OrderItemComponentComponent>();

        public OrderItemComponent(int id, int orderItemId, int variationId, int componentId, int portions)
        {
            this.id = id;
            this.orderItemId = orderItemId;
            this.variationId = variationId;
            this.componentId = componentId;
            this.portions = portions;

            component = new Component(componentId, portions);
        }

        public List<Ingredient> IngredientsUsed()
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            foreach (Ingredient ingredient in component.Ingredients)
            {
                Ingredient newIngredient = ingredient;
                newIngredient.Portions = newIngredient.Portions * portions;

                ingredients.Add(newIngredient);
            }

            foreach(OrderItemComponentComponent subComponent in components)
                foreach(Ingredient ingredient in subComponent.IngredientsUsed())
                    ingredients.Add(ingredient);

            return ingredients;
        }

        public void AddSubComponent(OrderItemComponentComponent component)
        {
            components.Add(component);
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public int OrderItemId
        {
            get
            {
                return orderItemId;
            }
        }

        public int VariationId
        {
            get
            {
                return variationId;
            }
        }

        public int ComponentId
        {
            get
            {
                return componentId;
            }
        }

        public int Portions
        {
            get
            {
                return portions;
            }
        }
    }
}
