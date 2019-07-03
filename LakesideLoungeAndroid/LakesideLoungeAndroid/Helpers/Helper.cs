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

namespace LakesideLoungeAndroid.Helpers
{
    public static class Helper
    {
        public static string ConvertStringFromChars(char[] chars)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in chars)
                sb.Append(char.ToString(c));

            return sb.ToString();
        }

        public static string FormatDate(DateTime? date)
        {
            string thisDate = date.ToString();
            return thisDate.Substring(0, thisDate.IndexOf(" "));
        }
    }
}