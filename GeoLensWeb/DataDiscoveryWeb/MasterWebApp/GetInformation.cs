using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterWebApp
{
    public static class GetInformation
    {
        public static bool getAuthorInfo(out int authorID, out string authorAr, out string authorEn)
        {
            authorID = 0;
            authorAr = "";
            authorEn = "";
            if (HttpContext.Current.Session["authorID"] != null)
            {
                authorID = (int)HttpContext.Current.Session["authorID"];
                authorAr = (string)HttpContext.Current.Session["authorAr"];
                authorEn = (string)HttpContext.Current.Session["authorEn"];
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool getUserInfo(out int userID, out string userFullName)
        {
            userID = 0;
            userFullName = "";
            if (HttpContext.Current.Session["userID"] != null)
            {
                userID = (int)HttpContext.Current.Session["userID"];
                userFullName = (string)HttpContext.Current.Session["fullName"];

                return true;
            }
            else
            {
                return false;
            }
        }

    }
}