using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Web;
using System.Text.RegularExpressions;


namespace DataLayer
{
    public static class FunServices 
    {
      
        public static int? contvertToInt  (string n1)
        {
            if(string .IsNullOrEmpty(n1))
            {
                return null; 
            }
            else
            {
                int i = 0;
                bool result = int.TryParse(n1, out i);
                if (result)
                {
                    return i;
                }
                else
                {
                    return null;
                }
            }
           

        }

        public static string getLang(string type)
        {

            bool isArabic = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft;

            switch (type)
            {
                case "float":
                    return isArabic ? "right" : "left";
                case "floatx":
                    return isArabic ? "left" : "right";
                case "dir":
                    return isArabic ? "rtl" : "ltr";
                case "class":
                    return isArabic ? "pull-left" : "pull-right";
                case "lang":
                    return isArabic ? "ar" : "en";
                case "drp":
                    return isArabic ? "dropdown-menu-right" : "dropdown-menu-left";
                default:
                   return "";

            }


        }


        public static DateTime? contvertToDate(string date)
        {
            DateTime currentDate = DateTime.Today;
            DateTime df = DateTime.Today;
            DateTime? returnDate;

            if(string.IsNullOrEmpty(date))
            {
                returnDate = null; 
            }

            string t1 = string.Format("{0:yyyy/M/d}", date);

           
                if (!DateTime.TryParseExact(t1, "yyyy/M/d", new CultureInfo("en-US"), DateTimeStyles.None, out df))
                {

                    returnDate = null;
                   
                }
                else
                {
                    returnDate  = df;
                    
                }



                return returnDate;
        }

        public static string IsRtl(string input)
        {
            var tt = Regex.IsMatch(input, @"\p{IsArabic}");
            if (tt)
                return "rtl";
            return "ltr";
        }

        public static string IsRtlPadding(string input)
        {
            var tt = Regex.IsMatch(input, @"\p{IsArabic}");
            if (tt)
                return "padding-right:25px;";
            return "padding-left:25px;";
        }


     

    }
}
