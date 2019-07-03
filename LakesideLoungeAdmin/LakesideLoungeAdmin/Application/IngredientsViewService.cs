using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAdmin.Infrastructure;

namespace LakesideLoungeAdmin.Application
{
    public class IngredientsViewService
    {
        public void AddIngredient()
        {
            int newId = Database.GetNewIngredientId();
            Database.AddIngredient(newId);
        }
    }
}
