using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using LakesideLoungeAndroid.Infrastructure;

namespace LakesideLoungeAndroid.Application
{
    public class LiveOrdersFragmentApplicationService
    {
        public void SetCurrentOrder(int id)
        {
            Database.SetCurrentOrder(id, true);
        }

        public void ClearCurrentOrder()
        {
            Database.ClearCurrentOrder(true);
        }
    }
}