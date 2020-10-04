using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.ViewModel;
using MasterWebApp.Models;
using System.Web.Security;
using Data;

namespace MasterWebApp.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Common/Login/




        public ActionResult Index()
        {
            //fullCombo(); 

            
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username , string password)
        {
            try
            {

                {
                    using (DiscoveryEventEntities DB = new DiscoveryEventEntities())
                    {

                        var obj1 = DB.Users.SingleOrDefault(x => x.username == username.Trim() && x.password == password.Trim());
                        if (obj1 != null)
                        {

                            Session["userID"] = obj1.userID;
                            Session["username"] = obj1.username;
                            Session["fullName"] = obj1.userNameEn;
                            TempData["saveOk"] = "تم الدخول بنجاح";
                            TempData["error"] = null;

                            return RedirectToAction("Index", "MainAdmin");
                        }
                        else
                        {
                            TempData["saveOk"] = null;
                            TempData["error"] = "خطأ اثناء الدخول";

                            return RedirectToAction("Index");

                        }
                    }


                }


            }
            catch (Exception ex)
            {
                TempData["error"] = "خطأ اثناء الدخول";
                //TempData["error"] = MasterWebApp.Resource.CommonMessage.msgWrongUserOrPass;
                return RedirectToAction("Index");
            }

        }


        public ActionResult Logout()
        {
            Response.Cookies.Clear();
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index");

        }

    }
}