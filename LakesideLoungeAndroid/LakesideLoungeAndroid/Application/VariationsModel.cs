using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAndroid.Domain;
using LakesideLoungeAndroid.Infrastructure;

namespace LakesideLoungeAndroid.Application
{
    public class VariationsModel
    {
        List<VariationModel> variationModels = new List<VariationModel>();

        public VariationsModel(int id)
        {
            Variation variation = Database.GetVariation(id);

            foreach (Variation subVariation in variation.Variations.Values)
            {
                VariationModel newVariationModel = new VariationModel(subVariation);
                variationModels.Add(newVariationModel);
            }

            variationModels.Sort();
        }

        public List<VariationModel> VariationModels
        {
            get
            {
                return variationModels;
            }
        }
    }
}
