using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeKitchenAndroid.Domain;
using LakesideLoungeKitchenAndroid.Infrastructure;

namespace LakesideLoungeKitchenAndroid.Application
{
    public class OrderItemModel
    {
        int id;
        int orderId;
        int variationId;
        string variationDisplayName;
        int inOutStatus;
        int discountId = 1;
        State state;

        List<OrderItemComponentModel> components = new List<OrderItemComponentModel>();

        public OrderItemModel(OrderItem model)
        {
            this.id = model.Id;
            this.orderId = model.OrderId;
            this.variationId = model.VariationId;
            this.variationDisplayName = model.DisplayName;
            this.inOutStatus = model.InOutStatus;
            this.discountId = model.DiscountId;
            this.state = model.State;
            
            foreach (OrderItemComponent component in model.Components)
            {
                OrderItemComponentModel newComponent = new OrderItemComponentModel(component);
                components.Add(newComponent);

                foreach(OrderItemComponentComponent subComponent in component.Components)
                {
                    OrderItemComponentComponentModel subModel = new OrderItemComponentComponentModel(subComponent);
                    newComponent.AddComponent(subModel);
                }
            }
        }

        public OrderItemModel(int id, int orderId, int variationId, string variationDisplayName, int inOutStatus, int discountId, State state)
        {
            this.id = id;
            this.orderId = orderId;
            this.variationId = variationId;
            this.variationDisplayName = variationDisplayName;
            this.inOutStatus = inOutStatus;
            this.discountId = discountId;
            this.state = state;
        }

        public void AddComponentModel(OrderItemComponentModel model)
        {
            components.Add(model);
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

        public string DisplayName
        {
            get
            {
                //Variation lastVariation = Database.GetVariation(variationId);
                //return lastVariation.DisplayName;
                return variationDisplayName;
            }
        }

        public State State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }

        public List<OrderItemComponentModel> ComponentModels
        {
            get
            {
                return components;
            }
        }
    }
}
