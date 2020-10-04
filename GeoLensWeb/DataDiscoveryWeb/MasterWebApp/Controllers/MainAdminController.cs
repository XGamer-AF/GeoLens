using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Data.ViewModel;
using System.Net.Http;
using DataLayer;
using Data;
using System.Web;
using System.IO;
using System.Web.Helpers;
using PagedList;

namespace MasterWebApp.Controllers
{
    public class MainAdminController : Controller
	{


        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {

                TempData["error"] = "error";
                return RedirectToAction("Index", "Login");
            }
            ViewBag.fullName = Session["fullName"];

                return View();
            
        }


    }
}
