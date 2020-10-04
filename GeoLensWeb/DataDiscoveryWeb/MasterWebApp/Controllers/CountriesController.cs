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
    public class CountriesController : Controller
	{
        private void fullCombo()
        {
            string lang = FunServices.getLang("lang");
            IEnumerable<object> li;

            ////////////// Template For The Select List      ///////
            //ViewBag.CountriesV = new SelectList(new List<CountriesViewModel>());

            //li = GlobalVariable.fullCombo("ACountries");

            //if (li != null)
            //{
            //    if (lang == "ar")
            //    {
            //        ViewBag.CountriesV = new SelectList(li, "countryID", "countryAr");
            //    }
            //    else
            //    {
            //        ViewBag.CountriesV = new SelectList(li, "countryID", "countryEn");
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
            CountriesAddViewModel obj = new CountriesAddViewModel();

            //GlobalVariable.addHeader();
            string query = string.Format("ACountries?search={0}", search);
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
                    obj.CountriesAll = response.Content.ReadAsAsync<IEnumerable<CountriesViewModel>>().Result.ToPagedList(page ?? 1, 20);

                }
                else
                {
                    obj.CountriesAll = Enumerable.Empty<CountriesViewModel>().ToPagedList(page ?? 1, 20);
                   
                }
                if (id == 0)
                {
                    obj.Countries = new Countries();
                }
                else
                {
                    GlobalVariable.addHeader();

                    HttpResponseMessage response2 = GlobalVariable.webApiClient.GetAsync("ACountries/" + id.ToString()).Result;
                    obj.Countries = response2.Content.ReadAsAsync<Countries>().Result;
                }

                return View(obj);
            }
        }

        public ActionResult CountriesAddEdit(int? id = 0)
		{
			int? sID = null;
			if (id !=0 )
			{
				sID = id; 
			}
			return RedirectToAction("Index", new { id = sID });
		}

        [HttpPost]
        public ActionResult CountriesAddEdit(CountriesAddViewModel obj, HttpPostedFileBase file)
        {
            try
            {
                int? id = null;
                int rowID = 0;
                Countries result = null; 
                if (string.IsNullOrEmpty(obj.Countries.countryNameAr))
                {
                    TempData["saveOk"] = null;
                    TempData["error"] = MasterWebApp.Resource.CommonMessage.msgEmpty;
                    return RedirectToAction("Index", new { id = id });
                }

                HttpResponseMessage response;

                GlobalVariable.addHeader();
                if (obj.Countries.countryID == 0)
                {
                    obj.Countries.statusID = 1;
                    response = GlobalVariable.webApiClient.PostAsJsonAsync<Countries>("ACountries", obj.Countries).Result;
                }
                else
                {
                    response = GlobalVariable.webApiClient.PutAsJsonAsync<Countries>("ACountries", obj.Countries).Result;
                    id = obj.countryID;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if(obj.Countries.countryID !=0 )
                    {
                        rowID = obj.Countries.countryID; 
                    }
                    else
                    {
                        result = response.Content.ReadAsAsync<Countries>().Result;
                        rowID = result != null ? result.countryID : 0;
                    }




                    TempData["saveOk"] = MasterWebApp.Resource.CommonMessage.saveOk;
                    TempData["error"] = null;
                    if(file != null)
                    {
                        string tt = "CO" + rowID + ".png";
                        //UploadImage(file, tt, "Countries");
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
            string query = string.Format("ACountries?id={0}&statusID={1}", id, statusID);
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
