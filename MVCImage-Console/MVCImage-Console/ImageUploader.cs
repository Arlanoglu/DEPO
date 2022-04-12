using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVCImage_Console
{
    public class ImageUploader
    {
        ////////////////////////////METOT
        /*
            0=> dosya boş
            1=> "bu görsel zaten eklenmiş"
            2=> "uymayan format"
            3=> "ekleme başarılı"
         */

        public static string UploadImage(string serverPath, HttpPostedFileBase file)
        {


            /*
               i-need-more-space.jpg
             */

            string fileName = "";

            if (file != null)
            {
                var uniqueName = Guid.NewGuid();
                serverPath = serverPath.Replace("~", "");
                var fileArray = file.FileName.Split('.');
                string extension = fileArray[fileArray.Length - 1].ToLower();
                fileName = uniqueName + "." + extension;

                //png - jpg - jpeg - gif - bmp
                if (extension == "png" || extension == "jpg" || extension == "jpeg" || extension == "gif" || extension == "bmp")
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath(serverPath + fileName)))
                    {
                        return "1";
                    }
                    else
                    {
                        try
                        {
                            var filePath = HttpContext.Current.Server.MapPath(serverPath + fileName);
                            file.SaveAs(filePath);
                            return fileName;
                        }
                        catch (Exception ex)
                        {

                            return ex.Message;
                        }



                    }
                }
                else
                {
                    return "2";
                }

            }
            else
            {
                return "0";
            }


        }
		
		////////////////////////////Form
		
		//<form method = "post"  enctype="multipart/form-data">
		
		////////////////////////////Controller
		
		//product.ImagePath = ImageUploader.UploadImage("~/Content/dist/product/", ImagePath);
    }
}
