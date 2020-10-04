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
    public class EventInfoController : Controller
    {
        private void fullCombo()
        {
            string lang = FunServices.getLang("lang");
            IEnumerable<object> li;

            ////////////// Countries List       ///////

            ViewBag.CountriesV = new SelectList(new List<CountriesViewModel>());

            li = GlobalVariable.fullCombo("ACountries");

            if (li != null)
            {
                if (lang == "ar")
                {
                    ViewBag.CountriesV = new SelectList(li, "countryID", "countryNameAr");
                }
                else
                {
                    ViewBag.CountriesV = new SelectList(li, "countryID", "countryNameEn");
                }
            }

            ////////////// Event Type List       ///////

            ViewBag.EventTypesV = new SelectList(new List<EventTypesViewModel>());

            li = GlobalVariable.fullCombo("AEventTypes");

            if (li != null)
            {
                if (lang == "ar")
                {
                    ViewBag.EventTypesV = new SelectList(li, "eventTypeID", "eventTypeNameEn");
                }
                else
                {
                    ViewBag.EventTypesV = new SelectList(li, "eventTypeID", "eventTypeNameAr");
                }
            }



        }

        int? userID = 1;

        //
        // GET: /Admin/ServicesCategory/
        public ActionResult Index(int? page, string search, string countryID, string eventTypeID, string dateFrom, string dateTo)
        {
            if (Session["userID"] == null)
            {

                TempData["error"] = "error";
                return RedirectToAction("Index", "Login");
            }
            fullCombo();
            IEnumerable<EventInfoViewModel> obj;

            //GlobalVariable.addHeader();
            string query = string.Format("AEventInfo?search={0}&countryID={1}&eventTypeID={2}&dateFrom={3}&dateTo={4}", search, countryID, eventTypeID, dateFrom, dateTo);
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
                    obj = response.Content.ReadAsAsync<IEnumerable<EventInfoViewModel>>().Result.ToPagedList(page ?? 1, 20);

                }
                else
                {
                    obj = Enumerable.Empty<EventInfoViewModel>().ToPagedList(page ?? 1, 20);

                }
                //if (id == 0)
                //{
                //    obj.EventInfo = new EventInfo();
                //}
                //else
                //{
                //   

                return View(obj);
            }
        }

        public ActionResult EventInfoAddEdit(int? id = 0)
        {
            fullCombo();
            EventInfo obj;
            if (id == 0)
            {
                obj = new EventInfo();
            }
            else
            {
                GlobalVariable.addHeader();

                HttpResponseMessage response2 = GlobalVariable.webApiClient.GetAsync("AEventInfo/" + id.ToString()).Result;
                obj = response2.Content.ReadAsAsync<EventInfo>().Result;
            

            }

            return View(obj);
        }

        [HttpPost]
        public ActionResult EventInfoAddEdit(EventInfo obj, HttpPostedFileBase file)
        {
            try
            {
                fullCombo();
                int? id = null;
                int rowID = 0;
                EventInfo result = null;
                if (string.IsNullOrEmpty(obj.eventNameAr))
                {
                    TempData["saveOk"] = null;
                    TempData["error"] = MasterWebApp.Resource.CommonMessage.msgEmpty;
                    return RedirectToAction("Index", new { id = id });
                }

                HttpResponseMessage response;

                GlobalVariable.addHeader();
                if (obj.eventID == 0)
                {
                    obj.dateAdd = DateTime.Now;
                    obj.statusID = 1;
                    response = GlobalVariable.webApiClient.PostAsJsonAsync<EventInfo>("AEventInfo", obj).Result;
                }
                else
                {
                    response = GlobalVariable.webApiClient.PutAsJsonAsync<EventInfo>("AEventInfo", obj).Result;
                    id = obj.eventID;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (obj.eventID != 0)
                    {
                        rowID = obj.eventID;
                    }
                    else
                    {
                        result = response.Content.ReadAsAsync<EventInfo>().Result;
                        rowID = result != null ? result.eventID : 0;
                    }




                    TempData["saveOk"] = MasterWebApp.Resource.CommonMessage.saveOk;
                    TempData["error"] = null;
                    if (file != null)
                    {
                        string tt = "CO" + rowID + ".png";
                        //UploadImage(file, tt, "EventInfo");
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
            string query = string.Format("AEventInfo?id={0}&statusID={1}", id, statusID);
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
