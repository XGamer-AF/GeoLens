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
    public class EventRefrencesController : Controller
	{
        private void fullCombo()
        {
            string lang = FunServices.getLang("lang");
            IEnumerable<object> li;

            ////////////// Template For The Select List      ///////
            //ViewBag.EventRefrencesV = new SelectList(new List<EventRefrencesViewModel>());

            //li = GlobalVariable.fullCombo("AEventRefrences");

            //if (li != null)
            //{
            //    if (lang == "ar")
            //    {
            //        ViewBag.EventRefrencesV = new SelectList(li, "countryID", "countryAr");
            //    }
            //    else
            //    {
            //        ViewBag.EventRefrencesV = new SelectList(li, "countryID", "countryEn");
            //    }
            //}

        }

		int? userID = 1;

        //
        // GET: /Admin/ServicesCategory/
        public ActionResult Index(int? page, int eventID,string eventName , int? id = 0)
        {
            if (Session["userID"] == null)
            {

                TempData["error"] = "error";
                return RedirectToAction("Index", "Login");
            }
            EventRefrencesAddViewModel obj = new EventRefrencesAddViewModel();
            ViewBag.eventName = eventName;
            obj.eventID = eventID;
            //GlobalVariable.addHeader();
            string query = string.Format("AEventRefrences?eventID={0}", eventID);
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
                    obj.EventRefrencesAll = response.Content.ReadAsAsync<IEnumerable<EventRefrencesViewModel>>().Result.ToPagedList(page ?? 1, 20);

                }
                else
                {
                    obj.EventRefrencesAll = Enumerable.Empty<EventRefrencesViewModel>().ToPagedList(page ?? 1, 20);
                   
                }
                if (id == 0)
                {

                    EventRefrences tt = new EventRefrences();
                    tt.eventID = eventID;
                    obj.EventRefrences = tt;
                }
                else
                {
                    GlobalVariable.addHeader();

                    HttpResponseMessage response2 = GlobalVariable.webApiClient.GetAsync("AEventRefrences/" + id.ToString()).Result;
                    obj.EventRefrences = response2.Content.ReadAsAsync<EventRefrences>().Result;
                }

                return View(obj);
            }
        }

        public ActionResult EventRefrencesAddEdit(int eventID,string eventName ,  int? id = 0)
		{
			int? sID = null;
			if (id !=0 )
			{
				sID = id; 
			}
			return RedirectToAction("Index", new {eventID = eventID ,eventName, id = sID });
		}

        [HttpPost]
        public ActionResult EventRefrencesAddEdit(EventRefrencesAddViewModel obj, string eventName,HttpPostedFileBase file)
        {
            try
            {
                int? id = null;
                int rowID = 0;
                EventRefrences result = null;
                obj.EventRefrences.eventID = obj.eventID;
                if (string.IsNullOrEmpty(obj.EventRefrences.eventURL))
                {
                    TempData["saveOk"] = null;
                    TempData["error"] = MasterWebApp.Resource.CommonMessage.msgEmpty;
                    return RedirectToAction("Index", new { id = id });
                }

                HttpResponseMessage response;

                GlobalVariable.addHeader();
                if (obj.EventRefrences.eventRefrencesID == 0)
                {
                    obj.EventRefrences.statusID = 1;
                    response = GlobalVariable.webApiClient.PostAsJsonAsync<EventRefrences>("AEventRefrences", obj.EventRefrences).Result;
                }
                else
                {
                    response = GlobalVariable.webApiClient.PutAsJsonAsync<EventRefrences>("AEventRefrences", obj.EventRefrences).Result;
                    id = obj.EventRefrencesID;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if(obj.EventRefrences.eventRefrencesID !=0 )
                    {
                        rowID = obj.EventRefrences.eventRefrencesID; 
                    }
                    else
                    {
                        result = response.Content.ReadAsAsync<EventRefrences>().Result;
                        rowID = result != null ? result.eventRefrencesID : 0;
                    }




                    TempData["saveOk"] = MasterWebApp.Resource.CommonMessage.saveOk;
                    TempData["error"] = null;
                    if(file != null)
                    {
                        string tt = "CO" + rowID + ".png";
                        //UploadImage(file, tt, "EventRefrences");
                    }
                  


                    return RedirectToAction("Index", new {eventID = obj.eventID,eventName = eventName,  id = id });
                }
                else
                {
                    TempData["saveOk"] = null;
                    TempData["error"] = MasterWebApp.Resource.CommonMessage.erroWhenSave;
                    return RedirectToAction("Index", new { eventID = obj.eventID, eventName = eventName, id = id });
                }

            }
            catch (Exception ex)
            {
                TempData["saveOk"] = null;
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult ChangeStatus(int id, int statusID , string eventName , int eventID)
		{
			GlobalVariable.addHeader();
            string query = string.Format("AEventRefrences?id={0}&statusID={1}", id, statusID);
			HttpResponseMessage response = GlobalVariable.webApiClient.GetAsync(query).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				TempData["saveOk"] = MasterWebApp.Resource.CommonMessage.saveOk;
				TempData["error"] = null;
				return RedirectToAction("Index",new {eventName = eventName , eventID = eventID });
			}
			else
			{
				TempData["saveOk"] = null;
				TempData["error"] = MasterWebApp.Resource.CommonMessage.erroWhenSave;
				return RedirectToAction("Index" , new { eventName = eventName, eventID = eventID });
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
