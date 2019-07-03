using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAndroid.Domain;
using LakesideLoungeAndroid.Infrastructure;

namespace LakesideLoungeAndroid.Application
{
    public enum FragmentRoute
    {
        Variations,
        Components,
        None
    }

    public class VariationsFragmentApplicationService
    {
        public FragmentRoute SelectVariation(int id)
        {
            Variation selectedVariation = Database.GetVariation(id);

            if (selectedVariation.HasVariations())
                return FragmentRoute.Variations;
            else if (selectedVariation.HasComponents())
                return FragmentRoute.Components;
            else
                return FragmentRoute.None;
        }

        //public int CreateNewOrderItem(int orderId, int variationId, int inOutStatus, int discount)
        //{
        //    int newId = Database.CreateNewOrderItem(orderId, variationId, inOutStatus, discount, true);
        //    return newId;
        //}

        public int GetNewOrderItemId()
        {
            return Database.GetNewOrderItemId();
        }

        //public OrderItemModel GetOrderItemModel(int id)
        //{
        //    return Database.GetOrderItemModel(id);
        //}

        public ComponentModel GetComponentModel(int id)
        {
            return Database.GetComponentModel(id);
        }

        public int GetNewOrderItemComponentId()
        {
            return Database.GetNewOrderItemComponentId();
        }

        //public void SaveComponents(OrderItemModel model)
        //{
        //    Database.SaveComponents(model, true);
        //}

        public DiscountModel GetDiscountModel(int id)
        {
            return Database.GetDiscountModel(id);
        }
    }
}
