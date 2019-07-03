using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAndroid.Domain;
using LakesideLoungeAndroid.Infrastructure;

namespace LakesideLoungeAndroid.Application
{
    public class OrderItemModel
    {
        int id;
        int orderId;
        int variationId;
        int inOutStatus;
        int discountId = 1;

        State state = State.None;

        List<OrderItemComponentModel> components = new List<OrderItemComponentModel>();

        public OrderItemModel(OrderItem model)
        {
            this.id = model.Id;
            this.orderId = model.OrderId;
            this.variationId = model.VariationId;
            this.inOutStatus = model.InOutStatus;
            this.discountId = model.DiscountId;
            this.state = model.State;

            foreach (OrderItemComponent component in model.Components)
            {
                OrderItemComponentModel newModel = new OrderItemComponentModel(component);
                components.Add(newModel);
            }
        }

        public OrderItemModel(int id, int orderId, int variationId, int inOutStatus, int discountId, State state)
        {
            this.id = id;
            this.orderId = orderId;
            this.variationId = variationId;
            this.inOutStatus = inOutStatus;
            this.discountId = discountId;
            this.state = state;
        }

        public OrderItemModel(int id, int orderId, int variationId)
        {
            this.id = id;
            this.orderId = orderId;
            this.variationId = variationId;
        }

        public OrderItemModel Clone()
        {
            OrderItemModel newModel = new OrderItemModel(this.id, this.orderId, this.variationId, this.inOutStatus, this.discountId, this.state);

            foreach(OrderItemComponentModel model in components)
                newModel.AddComponentModel(model.Clone());

            return newModel;
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
                Variation lastVariation = Database.GetVariation(variationId);
                return lastVariation.DisplayName;
            }
        }

        public decimal Price
        {
            get
            {
                decimal total = 0;
                Variation variation;

                if (ItemIsPointBased(out variation))
                {
                    float totalPoints = 0;
                    float variationPoints = variation.Points;
                    decimal pointPrice = variation.PointPrice;

                    Variation lastVariation = Database.GetVariation(variationId);

                    decimal variationTotal = 0;

                    while (lastVariation.Id != 1)
                    {
                        variationTotal = variationTotal + lastVariation.Price;
                        lastVariation = Database.GetVariation(lastVariation.ParentId);
                    }

                    Variation thisVariation = Database.GetVariation(variationId);

                    foreach (OrderItemComponentModel component in components)
                    {
                        float thesePoints = thisVariation.Components[component.ComponentId].Points;
                        float thisTotalPoints = thesePoints * component.Portions;
                        totalPoints += thisTotalPoints;
                    }

                    if (totalPoints <= variationPoints)
                        total = variationTotal;
                    else
                        total = variationTotal + (Convert.ToDecimal((totalPoints - variationPoints)) * pointPrice);
                }
                else
                {
                    decimal componentsTotalPrice = 0;

                    foreach (OrderItemComponentModel component in components)
                    {
                        for (int i = 0; i < component.Portions; i++)
                            componentsTotalPrice += component.Price;

                        foreach (OrderItemComponentComponentModel subComponent in component.Components)
                        {
                            for (int i = 0; i < subComponent.Portions; i++)
                                componentsTotalPrice += subComponent.Price;
                        }
                    }

                    Variation lastVariation = Database.GetVariation(variationId);

                    decimal variationTotal = 0;

                    while (lastVariation.Id != 1)
                    {
                        variationTotal = variationTotal + lastVariation.Price;
                        lastVariation = Database.GetVariation(lastVariation.ParentId);
                    }

                    total = variationTotal + componentsTotalPrice;
                }
                
                DiscountModel model = Database.GetDiscountModel(discountId);
                decimal deduction = 0;

                if (model.DiscountType == 0)
                {
                    decimal discountAmount = model.Discount;

                    if (discountAmount == 0)
                        return total;

                    decimal percentage = (decimal)discountAmount / 100;

                    deduction = total * percentage;
                }
                else
                    deduction = model.Discount;

                decimal finalValue = total - deduction;

                decimal workingValue = finalValue * 10;
                int intWorking = (int)workingValue;
                decimal partOfPence = workingValue - intWorking;

                if(partOfPence >= 0.50M)
                    return ((decimal)intWorking / 10.0M) + 0.05M;

                return (decimal)intWorking / 10.0M;
            }
        }

        private bool ItemIsPointBased(out Variation variation)
        {
            Variation lastVariation = Database.GetVariation(variationId);

            while (lastVariation.Id != 1)
            {
                if (lastVariation.Points > 0)
                {
                    variation = lastVariation;
                    return true;
                }
                
                lastVariation = Database.GetVariation(lastVariation.ParentId);
            }

            variation = null;
            return false;
        }

        public List<OrderItemComponentModel> ComponentModels
        {
            get
            {
                return components;
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
