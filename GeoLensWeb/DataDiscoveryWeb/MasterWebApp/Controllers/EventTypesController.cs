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
    public class EventTypesController : Controller
	{
        private void fullCombo()
        {
            string lang = FunServices.getLang("lang");
            IEnumerable<object> li;

            ////////////// Template For The Select List      ///////
            //ViewBag.EventTypesV = new SelectList(new List<EventTypesViewModel>());

            //li = GlobalVariable.fullCombo("AEventTypes");

            //if (li != null)
            //{
            //    if (lang == "ar")
            //    {
            //        ViewBag.EventTypesV = new SelectList(li, "countryID", "countryAr");
            //    }
            //    else
            //    {
            //        ViewBag.EventTypesV = new SelectList(li, "countryID", "countryEn");
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
            EventTypesAddViewModel obj = new EventTypesAddViewModel();

            //GlobalVariable.addHeader();
            string query = string.Format("AEventTypes?search={0}", search);
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
                    obj.EventTypesAll = response.Content.ReadAsAsync<IEnumerable<EventTypesViewModel>>().Result.ToPagedList(page ?? 1, 20);

                }
                else
                {
                    obj.EventTypesAll = Enumerable.Empty<EventTypesViewModel>().ToPagedList(page ?? 1, 20);
                   
                }
                if (id == 0)
                {
                    obj.EventTypes = new EventTypes();
                }
                else
                {
                    GlobalVariable.addHeader();

                    HttpResponseMessage response2 = GlobalVariable.webApiClient.GetAsync("AEventTypes/" + id.ToString()).Result;
                    obj.EventTypes = response2.Content.ReadAsAsync<EventTypes>().Result;
                }

                return View(obj);
            }
        }

        public ActionResult EventTypesAddEdit(int? id = 0)
		{
			int? sID = null;
			if (id !=0 )
			{
				sID = id; 
			}
			return RedirectToAction("Index", new { id = sID });
		}

        [HttpPost]
        public ActionResult EventTypesAddEdit(EventTypesAddViewModel obj, HttpPostedFileBase file)
        {
            try
            {
                int? id = null;
                int rowID = 0;
                EventTypes result = null; 
                if (string.IsNullOrEmpty(obj.EventTypes.eventTypeNameAr))
                {
                    TempData["saveOk"] = null;
                    TempData["error"] = MasterWebApp.Resource.CommonMessage.msgEmpty;
                    return RedirectToAction("Index", new { id = id });
                }

                HttpResponseMessage response;

                GlobalVariable.addHeader();
                if (obj.EventTypes.eventTypeID == 0)
                {
                    obj.EventTypes.statusID = 1;
                    response = GlobalVariable.webApiClient.PostAsJsonAsync<EventTypes>("AEventTypes", obj.EventTypes).Result;
                }
                else
                {
                    response = GlobalVariable.webApiClient.PutAsJsonAsync<EventTypes>("AEventTypes", obj.EventTypes).Result;
                    id = obj.EventTypeID;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if(obj.EventTypes.eventTypeID !=0 )
                    {
                        rowID = obj.EventTypes.eventTypeID; 
                    }
                    else
                    {
                        result = response.Content.ReadAsAsync<EventTypes>().Result;
                        rowID = result != null ? result.eventTypeID : 0;
                    }
                   


                    TempData["saveOk"] = MasterWebApp.Resource.CommonMessage.saveOk;
                    TempData["error"] = null;
                    if(file != null)
                    {
                        string tt = "ET" + rowID + ".png";
                        UploadImage(file, tt, "EventType");
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
        private void UploadImage(HttpPostedFileBase image, string fileName, string folder)
        {
            try
            {

                ImageInfoViewModel fullData = new ImageInfoViewModel();

                fullData.data = null;
                using (var binaryReader = new BinaryReader(image.InputStream))
                {
                    fullData.data = binaryReader.ReadBytes(image.ContentLength);
                }

                fullData.name = fileName;
                fullData.folder = folder;
                GlobalVariable.addHeader();
                HttpResponseMessage response;
                response = GlobalVariable.webApiClient.PostAsJsonAsync<ImageInfoViewModel>("AImages", fullData).Result;

            }
            catch (Exception ex)
            {
                TempData["saveOk"] = null;
                TempData["error"] = "خطأ اثناء حفظ الصورة";

            }

        }

        public ActionResult ChangeStatus(int id, int statusID)
		{
			GlobalVariable.addHeader();
            string query = string.Format("AEventTypes?id={0}&statusID={1}", id, statusID);
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
