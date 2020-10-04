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
    public class EventImageController : Controller
	{
        private void fullCombo()
        {
            string lang = FunServices.getLang("lang");
            IEnumerable<object> li;

            ////////////// Template For The Select List      ///////
            //ViewBag.EventImageV = new SelectList(new List<EventImageViewModel>());

            //li = GlobalVariable.fullCombo("AEventImage");

            //if (li != null)
            //{
            //    if (lang == "ar")
            //    {
            //        ViewBag.EventImageV = new SelectList(li, "countryID", "countryAr");
            //    }
            //    else
            //    {
            //        ViewBag.EventImageV = new SelectList(li, "countryID", "countryEn");
            //    }
            //}

        }

		int? userID = 1;

        //
        // GET: /Admin/ServicesCategory/
        public ActionResult Index(int? page, string eventID,string eventName, int? id = 0)
        {

            if (Session["userID"] == null)
            {

                TempData["error"] = "error";
                return RedirectToAction("Index", "Login");
            }
            ViewBag.eventName = eventName;
            EventImageAddViewModel obj = new EventImageAddViewModel();
            ViewBag.eventID = eventID;
            //GlobalVariable.addHeader();
            string query = string.Format("AEventImage?eventID={0}", eventID);
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
                    obj.EventImageAll = response.Content.ReadAsAsync<IEnumerable<EventImageViewModel>>().Result;
                  
                }
                else
                {
                    obj.EventImageAll = Enumerable.Empty<EventImageViewModel>();
                    
                }
                if (id == 0)
                {
                    var tt = new EventImage();
                    tt.eventID = FunServices.contvertToInt(eventID);
                    obj.EventImage = tt;
                }
                else
                {
                    GlobalVariable.addHeader();

                    HttpResponseMessage response2 = GlobalVariable.webApiClient.GetAsync("AEventImage/" + id.ToString()).Result;
                    obj.EventImage = response2.Content.ReadAsAsync<EventImage>().Result;
                }

                return View(obj);
            }
        }

        public ActionResult EventImageAddEdit(string eventID,int? id = 0)
		{
			int? sID = null;
			if (id !=0 )
			{
				sID = id; 
			}
			return RedirectToAction("Index", new { eventID = eventID,id = sID });
		}

        [HttpPost]
        public ActionResult EventImageAddEdit(string eventName, int eventID,   IEnumerable<HttpPostedFileBase> files)
        {

            try
            {
                if (files.Count() == 0 || files.FirstOrDefault() == null)
                {
                    TempData["error"] = "اختر الصور وحاول مرة اخرى ";
                    //return RedirectToAction("Index", new { eventID  = MembershipID, salesID = salesID });
                    return RedirectToAction("Index", new { eventID = eventID, eventName = eventName, id = 0 });

                }


                foreach (var file in files)
                {
                    if (file.ContentLength == 0) continue;
                    int? id = null;
                    int rowID = 0;
                    EventImage result = null;

                    EventImage obj = new EventImage();
                    obj.statusID = 1;
                    obj.eventID = eventID;
                    HttpResponseMessage response;

                    GlobalVariable.addHeader();
                    if (obj.eventImageID == 0)
                    {
                        obj.statusID = 1;
                        response = GlobalVariable.webApiClient.PostAsJsonAsync<EventImage>("AEventImage", obj).Result;
                    }
                    else
                    {
                        response = GlobalVariable.webApiClient.PutAsJsonAsync<EventImage>("AEventImage", obj).Result;
                        id = obj.eventImageID;
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                       
                        {
                            result = response.Content.ReadAsAsync<EventImage>().Result;
                            rowID = result != null ? result.eventImageID : 0;
                        }




                        TempData["saveOk"] = MasterWebApp.Resource.CommonMessage.saveOk;
                        TempData["error"] = null;
                        if (file != null)
                        {
                            string tt = "IE" + rowID + ".png";
                            UploadImage(file, tt, "EventImage");
                            UpdateEvent(eventID);
                        }



                    }
                    else
                    {
                        TempData["saveOk"] = null;
                        TempData["error"] = MasterWebApp.Resource.CommonMessage.erroWhenSave;
                        
                    }
                  

                }

                return RedirectToAction("Index", new { eventID = eventID, eventName = eventName });



            }
            catch (Exception ex)
            {
                TempData["saveOk"] = null;
                TempData["error"] = ex.Message;
                return RedirectToAction("Index", new { eventID = eventID, eventName = eventName});
            }
        }


        private void UpdateEvent(int eventID)
        {
            EventInfo obj = null;

            IEnumerable< EventImageViewModel> objImage = Enumerable.Empty<EventImageViewModel>(); ;
            string imageName = "";
            string query = string.Format("AEventImage?eventID={0}", eventID);
            HttpResponseMessage response = GlobalVariable.webApiClient.GetAsync(query).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                objImage = response.Content.ReadAsAsync<IEnumerable<EventImageViewModel>>().Result;
                
            }
            if (objImage.Count() > 0)
            {
                imageName = "IE" + objImage.FirstOrDefault().eventImageID + ".png";
              

            }
            GlobalVariable.addHeader();

            HttpResponseMessage response2 = GlobalVariable.webApiClient.GetAsync("AEventInfo/" + eventID.ToString()).Result;
            obj = response2.Content.ReadAsAsync<EventInfo>().Result;
            if (obj != null)
            {
                obj.eventImage = imageName;
              var  response3 = GlobalVariable.webApiClient.PutAsJsonAsync<EventInfo>("AEventInfo", obj).Result;



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


        public ActionResult ChangeStatus(string eventName ,int id, int statusID,string eventID)
		{
			GlobalVariable.addHeader();
            string query = string.Format("AEventImage?id={0}&statusID={1}", id, statusID);
			HttpResponseMessage response = GlobalVariable.webApiClient.GetAsync(query).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{

                string imageName = "IE" + id + ".png";
                string filePath = Path.Combine(Server.MapPath("~/Images/EventImage"), imageName);
                ImagesServices.deleteFile(filePath);
                UpdateEvent((int)FunServices .contvertToInt(eventID));
                TempData["saveOk"] = MasterWebApp.Resource.CommonMessage.saveOk;
				TempData["error"] = null;
                return RedirectToAction("Index", new { eventID = eventID, eventName = eventName });
            }
            else
			{
				TempData["saveOk"] = null;
				TempData["error"] = MasterWebApp.Resource.CommonMessage.erroWhenSave;
                return RedirectToAction("Index", new { eventID = eventID, eventName = eventName});
            }
        }



        

    }
}
