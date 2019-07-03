using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakesideLoungeAdmin.Helpers
{
    public static class Helper
    {
        public static string StripSpaces(string inString)
        {
            if (inString.IndexOf("  ") > -1)
                return inString.Substring(0, inString.IndexOf("  "));
            else
                return inString;
        }

        public static string GetDate(DateTime date)
        {
            string shortDate = date.ToShortDateString();
            int firstSlash = shortDate.IndexOf("/");
            int secondSlash = shortDate.IndexOf("/", firstSlash + 1);

            string day = shortDate.Substring(0, firstSlash);
            string month = shortDate.Substring(firstSlash + 1, secondSlash - firstSlash - 1);
            string year = shortDate.Substring(secondSlash + 1, shortDate.Length - secondSlash - 1);

            return month + "/" + day + "/ " + year;
        }

        public static string FormatDate(DateTime? date)
        {
            string thisDate = date.ToString();
            return thisDate.Substring(0, thisDate.IndexOf(" "));
        }
    }
}
