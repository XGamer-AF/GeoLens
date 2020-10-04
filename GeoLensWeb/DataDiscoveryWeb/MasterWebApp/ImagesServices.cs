using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Drawing;

namespace MasterWebApp
{
    public class ImagesServices
    {
        public static Boolean fileExists(string imagePath)
        {
            return File.Exists((imagePath));
        }


        public static void savefileLogo(string memberID)
        {

            string filePath = HttpContext.Current.Server.MapPath("~/Images/UploadImages/noImage.jpg");
            String des = HttpContext.Current.Server.MapPath("~/Images/UploadImages/PR" + memberID + ".jpg");
            System.IO.File.Copy(filePath, des);


        }

        public static Boolean deleteFile(string imagePath)
        {
            try
            {
                if (fileExists(imagePath))
                {
                    File.Delete(imagePath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static string getPath(string folder)
        {
            return HttpContext.Current.Server.MapPath("~/Images/" + folder);

        }
        public static string getImagePath()
        {
            return HttpContext.Current.Server.MapPath("~/Images/");
        }

        public static string getPdfPath(string folder)
        {
            return HttpContext.Current.Server.MapPath("~/Pdf/" + folder);

        }

    }
}
