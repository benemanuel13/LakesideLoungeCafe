using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAdmin.Application;
using LakesideLoungeAdmin.Infrastructure;

namespace LakesideLoungeAdmin.Domain
{
    public class OrderItem
    {
        int id;
        int orderId;
        int variationId;
        int inOutStatus;
        int discountId = 1;

        Variation variation;

        List<OrderItemComponent> components = new List<OrderItemComponent>();

        public OrderItem(int id, int orderId, int variationId, int inOutStatus, int discountId)
        {
            this.id = id;
            this.orderId = orderId;
            this.variationId = variationId;
            this.inOutStatus = inOutStatus;
            this.discountId = discountId;

            variation = Database.GetVariation(id, false);
        }

        public List<Ingredient> IngredientsUsed()
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            foreach (Ingredient ingredient in variation.Ingredients)
                ingredients.Add(ingredient);

            Variation parentVariation = Database.GetVariation(variation.ParentId, false);

            while (parentVariation.Id != 1)
            {
                foreach(Ingredient ingredient in parentVariation.Ingredients)
                    ingredients.Add(ingredient);

                parentVariation = Database.GetVariation(parentVariation.ParentId, false);
            }

            foreach (OrderItemComponent component in components)
                foreach (Ingredient ingredient in component.IngredientsUsed())
                    ingredients.Add(ingredient);

            return ingredients;
        }

        public void AddComponent(OrderItemComponent component)
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

        public int OrderId
        {
            get
            {
                return orderId;
            }
        }

        public int VariationId
        {
            get
            {
                return variationId;
            }
        }

        public int InOutStatus
        {
            get
            {
                return inOutStatus;
            }
        }

        public int DiscountId
        {
            get
            {
                return discountId;
            }
        }

        //public string DisplayName
        //{
        //    get
        //    {
        //        return displayName;
        //    }
        //}

        public List<OrderItemComponent> Components
        {
            get
            {
                return components;
            }
        }

        //public State State
        //{
        //    get
        //    {
        //        return state;
        //    }
        //}
    }
}
