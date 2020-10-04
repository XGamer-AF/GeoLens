using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MasterWebApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["lang"];
            if (cookie != null && cookie.Value != null)
            {
                if (!string.IsNullOrEmpty(cookie.Value))
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie.Value);
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie.Value);

                }
                else
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar");
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar");

                    HttpCookie tt = new HttpCookie("lang");
                    tt.Value = "ar";
                    tt.Expires = DateTime.Now.AddDays(10);
                    HttpContext.Current.Response.Cookies.Add(tt);

                }

            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar");

                HttpCookie tt = new HttpCookie("lang");
                tt.Value = "ar";
                tt.Expires = DateTime.Now.AddDays(10);
                HttpContext.Current.Response.Cookies.Add(tt);
            }

            CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat = (new System.Globalization.CultureInfo("en")).DateTimeFormat;
            newCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            newCulture.DateTimeFormat.DateSeparator = "/";
            System.Threading.Thread.CurrentThread.CurrentCulture = newCulture;




        }

    }
}
