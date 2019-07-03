using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeWebApi.Application;

namespace LakesideLoungeWebApi.Domain
{
    public class OrderItem
    {
        int id;
        int orderId;
        int variationId;
        string displayName;
        int inOutStatus;
        int discountId = 1;

        State state = State.None;

        List<OrderItemComponent> components = new List<OrderItemComponent>();

        public OrderItem()
        {
        }

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

            set
            {
                id = value;
            }
        }

        public int OrderId
        {
            get
            {
                return orderId;
            }

            set
            {
                orderId = value;
            }
        }

        public int VariationId
        {
            get
            {
                return variationId;
            }

            set
            {
                variationId = value;
            }
        }

        public int InOutStatus
        {
            get
            {
                return inOutStatus;
            }

            set
            {
                inOutStatus = value;
            }
        }

        public int DiscountId
        {
            get
            {
                return discountId;
            }

            set
            {
                discountId = value;
            }
        }

        public string DisplayName
        {
            get
            {
                return displayName;
            }

            set
            {
                displayName = value;
            }
        }

        public List<OrderItemComponent> Components
        {
            get
            {
                return components;
            }

            set
            {
                components = value;
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
    }
}
