using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAndroid.Domain;
using LakesideLoungeAndroid.Infrastructure;

namespace LakesideLoungeAndroid.Application
{
    public class VariationModel : IComparable<VariationModel>
    {
        int id;
        int parentId;
        string name;
        int position;

        public VariationModel(int id)
        {
            Variation variation = Database.GetVariation(id);

            this.id = variation.Id;
            this.parentId = variation.ParentId;
            this.name = variation.Name;
            this.position = variation.Position;
        }

        public VariationModel(Variation variation)
        {
            this.id = variation.Id;
            this.parentId = variation.ParentId;
            this.name = variation.Name;
            this.position = variation.Position;
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public int ParentId
        {
            get
            {
                return parentId;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public int Position
        {
            get
            {
                return position;
            }
        }

        public int CompareTo(VariationModel other)
        {
            if (position > other.Position)
                return 1;
            else if (position < other.Position)
                return -1;

            return 0;
        }
    }
}
