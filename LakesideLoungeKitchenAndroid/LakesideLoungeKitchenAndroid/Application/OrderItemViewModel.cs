using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LakesideLoungeKitchenAndroid.Application
{
    public class OrderItemViewModel : IComparable<OrderItemViewModel>
    {
        private int id;
        private int orderId;
        private string name;
        private string description;
        private State state;
        private bool containsComponents;
        private int inOutStatus;

        public OrderItemViewModel(int id, int orderId, string name, string description, State state, bool containsComponents, int inOutStatus)
        {
            this.id = id;
            this.orderId = orderId;
            this.name = name;
            this.description = description;
            this.state = state;
            this.containsComponents = containsComponents;
            this.inOutStatus = inOutStatus;
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

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
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

        public bool ContainsComponents
        {
            get
            {
                return containsComponents;
            }
        }

        public int CompareTo(OrderItemViewModel other)
        {
            if (id > other.Id)
                return 1;
            else if (id < other.Id)
                return -1;

            return 0;
        }

        public int InOutStatus
        {
            get
            {
                return inOutStatus;
            }
        }
    }
}