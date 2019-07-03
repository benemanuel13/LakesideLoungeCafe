using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LakesideLoungeWebApi.Helpers
{
    public static class Helper
    {
        public static string StripSpaces(string text)
        {
            int pos = text.Length - 1;

            while(text.Substring(pos, 1)==" ")
                pos--;

            return text.Substring(0, pos + 1);
        }
    }
}