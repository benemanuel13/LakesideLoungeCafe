using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakesideLoungeKitchenAndroid.Domain
{
    public class Component
    {
        int id;
        string name;
        string displayName;
        int portions;

        public Component(int id, string name, string displayName, int portions)
        {
            this.id = id;
            this.name = name;
            this.displayName = displayName;
            this.portions = portions;
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string DisplayName
        {
            get
            {
                return displayName;
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
