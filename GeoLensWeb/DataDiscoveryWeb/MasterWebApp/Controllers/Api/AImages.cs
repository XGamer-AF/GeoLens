using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using DataLayer;
using Data;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Data.ViewModel;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Drawing.Imaging;
using System.Web.Hosting;
using System.Web;

namespace MasterWebApp.Controllers.Api
{

    public class AImagesController : ApiController
    {


        [HttpGet]

        public HttpResponseMessage GetImage(string folder, string imageName)
        {

            {

                string path = "~/Images/" + folder + "/" + imageName;
                // "~/Images/123.jpg"
                String filePath = HostingEnvironment.MapPath(path);

                var result = new HttpResponseMessage(HttpStatusCode.OK);

                if (!fileExists(filePath))
                {
                    imageName = "noImage.jpg";
                    path = "~/Images/" + folder + "/" + imageName;
                    filePath = HostingEnvironment.MapPath(path);
                }

                if (fileExists(filePath))
                {
                    FileStream fileStream = new FileStream(filePath, FileMode.Open);
                    Image image = Image.FromStream(fileStream);
                    MemoryStream memoryStream = new MemoryStream();
                    image.Save(memoryStream, ImageFormat.Jpeg);

                    result.Content = new ByteArrayContent(memoryStream.ToArray());
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

                    fileStream.Close();
                    memoryStream.Close();
                }


                return result;
            }

        }


         [HttpGet]
        public async Task<String> DeleteImage(string folder, string deleteImage)
        {

            {

                string path = "~/Images/" + folder + "/" + deleteImage;
                // "~/Images/123.jpg"
                String filePath = HostingEnvironment.MapPath(path);

                var result = new HttpResponseMessage(HttpStatusCode.OK);

                

                if (fileExists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }


                return "ok";
            }

        }

         [HttpGet]
         public async Task<String> checkImage(string folder, string checkImage)
         {
             {
                 string path = "~/Images/" + folder + "/" + checkImage;
                 // "~/Images/123.jpg"
                 String filePath = HostingEnvironment.MapPath(path);

                 var result = new HttpResponseMessage(HttpStatusCode.OK);



                 if (fileExists(filePath))
                 {
                     return "true";
                 }
                 else
                 {
                     return "false";
                 }


                 
             }

         }

        private Boolean fileExists(string imagePath)
        {
            return File.Exists((imagePath));


        }


        [HttpPost]
        public async Task<HttpResponseMessage> PostUserImage(ImageInfoViewModel obj)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                if (obj.data != null && !string.IsNullOrEmpty(obj.name ) && !string.IsNullOrEmpty(obj.folder))
                {
                    String path = HttpContext.Current.Server.MapPath("~/images/" + obj.folder + "/" + obj.name); //Path

                    File.WriteAllBytes(path, obj.data);
                   
                }
                else
                {
                    response = Request.CreateErrorResponse(HttpStatusCode.BadRequest,"error name or folder");
                }

                return response;

               
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }
    }
}


