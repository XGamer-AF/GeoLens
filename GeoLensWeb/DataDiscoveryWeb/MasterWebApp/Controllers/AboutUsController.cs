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
    public class AboutUsController : Controller
	{
        private void fullCombo()
        {
            string lang = FunServices.getLang("lang");
            IEnumerable<object> li;

            ////////////// Template For The Select List      ///////
            //ViewBag.AboutUsV = new SelectList(new List<AboutUsViewModel>());

            //li = GlobalVariable.fullCombo("AAboutUs");

            //if (li != null)
            //{
            //    if (lang == "ar")
            //    {
            //        ViewBag.AboutUsV = new SelectList(li, "countryID", "countryAr");
            //    }
            //    else
            //    {
            //        ViewBag.AboutUsV = new SelectList(li, "countryID", "countryEn");
            //    }
            //}

        }

		int? userID = 1;

        //
        // GET: /Admin/ServicesCategory/
        public ActionResult Index(int? page, string search, int? id = 0)
        {
            if (Session["userID"] == null)
            {

                TempData["error"] = "error";
                return RedirectToAction("Index", "Login");
            }
            AboutUsAddViewModel obj = new AboutUsAddViewModel();

            //GlobalVariable.addHeader();
            string query = string.Format("AAboutUs?search={0}", search);
            HttpResponseMessage response = GlobalVariable.webApiClient.GetAsync(query).Result;


           

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {

                TempData["error"] = "error";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    obj.AboutUsAll = response.Content.ReadAsAsync<IEnumerable<AboutUsViewModel>>().Result.ToPagedList(page ?? 1, 20);

                }
                else
                {
                    obj.AboutUsAll = Enumerable.Empty<AboutUsViewModel>().ToPagedList(page ?? 1, 20);
                   
                }
                if (id == 0)
                {
                    obj.AboutUs = new AboutUs();
                }
                else
                {
                    GlobalVariable.addHeader();

                    HttpResponseMessage response2 = GlobalVariable.webApiClient.GetAsync("AAboutUs/" + id.ToString()).Result;
                    obj.AboutUs = response2.Content.ReadAsAsync<AboutUs>().Result;
                }

                return View(obj);
            }
        }

        public ActionResult AboutUsAddEdit(int? id = 0)
		{
			int? sID = null;
			if (id !=0 )
			{
				sID = id; 
			}
			return RedirectToAction("Index", new { id = sID });
		}

        [HttpPost]
        public ActionResult AboutUsAddEdit(AboutUsAddViewModel obj, HttpPostedFileBase file)
        {
            try
            {
                int? id = null;
                int rowID = 0;
                AboutUs result = null; 
                if (string.IsNullOrEmpty(obj.AboutUs.aboutUsDescAr))
                {
                    TempData["saveOk"] = null;
                    TempData["error"] = MasterWebApp.Resource.CommonMessage.msgEmpty;
                    return RedirectToAction("Index", new { id = id });
                }

                HttpResponseMessage response;

                GlobalVariable.addHeader();
                if (obj.AboutUs.aboutUsID == 0)
                {
                    obj.AboutUs.statusID = 1;
                    response = GlobalVariable.webApiClient.PostAsJsonAsync<AboutUs>("AAboutUs", obj.AboutUs).Result;
                }
                else
                {
                    response = GlobalVariable.webApiClient.PutAsJsonAsync<AboutUs>("AAboutUs", obj.AboutUs).Result;
                    id = obj.AboutUsID;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if(obj.AboutUs.aboutUsID !=0 )
                    {
                        rowID = obj.AboutUs.aboutUsID; 
                    }
                    else
                    {
                        result = response.Content.ReadAsAsync<AboutUs>().Result;
                        rowID = result != null ? result.aboutUsID : 0;
                    }




                    TempData["saveOk"] = MasterWebApp.Resource.CommonMessage.saveOk;
                    TempData["error"] = null;
                    if(file != null)
                    {
                        string tt = "CO" + rowID + ".png";
                        //UploadImage(file, tt, "AboutUs");
                    }
                  


                    return RedirectToAction("Index", new { id = id });
                }
                else
                {
                    TempData["saveOk"] = null;
                    TempData["error"] = MasterWebApp.Resource.CommonMessage.erroWhenSave;
                    return RedirectToAction("Index", new { id = id });
                }

            }
            catch (Exception ex)
            {
                TempData["saveOk"] = null;
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult ChangeStatus(int id, int statusID)
		{
			GlobalVariable.addHeader();
            string query = string.Format("AAboutUs?id={0}&statusID={1}", id, statusID);
			HttpResponseMessage response = GlobalVariable.webApiClient.GetAsync(query).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				TempData["saveOk"] = MasterWebApp.Resource.CommonMessage.saveOk;
				TempData["error"] = null;
				return RedirectToAction("Index");
			}
			else
			{
				TempData["saveOk"] = null;
				TempData["error"] = MasterWebApp.Resource.CommonMessage.erroWhenSave;
				return RedirectToAction("Index");
			}
		}



        //private void UploadImage(HttpPostedFileBase image, string fileName, string folder)
        //{
        //    try
        //    {

        //        ImageInfoViewModel fullData = new ImageInfoViewModel();

        //        fullData.data = null;
        //        using (var binaryReader = new BinaryReader(image.InputStream))
        //        {
        //            fullData.data = binaryReader.ReadBytes(image.ContentLength);
        //        }

        //        fullData.name = fileName;
        //        fullData.folder = folder;
        //        GlobalVariable.addHeader();
        //        HttpResponseMessage response;
        //        response = GlobalVariable.webApiClient.PostAsJsonAsync<ImageInfoViewModel>("AImages", fullData).Result;

        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["saveOk"] = null;
        //        TempData["error"] = "خطأ اثناء حفظ الصورة";

        //    }

        //}

    }
}
