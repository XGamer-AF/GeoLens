using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasterWebApp.Controllers
{
    [AllowAnonymous]
    public class LanguageController : Controller
    {
        //
        // GET: /Common/Language/
        public ActionResult SetLanguage(string lang)
        {
            if(lang!= null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(lang);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar");


            }


            CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat = (new System.Globalization.CultureInfo("en")).DateTimeFormat;
            newCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            newCulture.DateTimeFormat.DateSeparator = "/";
            System.Threading.Thread.CurrentThread.CurrentCulture = newCulture;

            HttpCookie cookie = new HttpCookie("lang");
            cookie.Value = lang;
            cookie.Expires = DateTime.Now.AddDays(10);
            Response.Cookies.Add(cookie);

            string result = "t";
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
	}
}