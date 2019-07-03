using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeKitchenAndroid.Application;

namespace LakesideLoungeKitchenAndroid.Domain
{
    public class OrderItem
    {
        int id;
        int orderId;
        int variationId;
        string displayName;
        int inOutStatus;
        int discountId = 1;
        State state;

        List<OrderItemComponent> components = new List<OrderItemComponent>();

        public OrderItem(int id, int orderId, int variationId, string displayName, int inOutStatus, int discountId, State state)
        {
            this.id = id;
            this.orderId = orderId;
            this.variationId = variationId;
            this.displayName = displayName;
            this.inOutStatus = inOutStatus;
            this.discountId = discountId;
            this.state = state;
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

        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        public State State
        {
            get
            {
                return state;
            }
        }

        public List<OrderItemComponent> Components
        {
            get
            {
                return components;
            }
        }
    }
}
