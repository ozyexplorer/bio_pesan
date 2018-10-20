using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Timers;
using System.Data;
using PesanMakan.Business;


namespace PesanMakan.Handler
{
    /// <summary>
    /// Summary description for HandlerImage
    /// </summary>
    public class HandlerImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                string dirFullPath = HttpContext.Current.Server.MapPath("~/Images/");
                string tgl = context.Request["id"];
                string[] files;
                string gambar;
                int numFiles;
                files = System.IO.Directory.GetFiles(dirFullPath);
                numFiles = files.Length;
                numFiles = numFiles + 1;
                string str_image = "";
                string stat = "0";

                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];
                    string fileName = file.FileName;
                    string fileExtension = file.ContentType;
                    if (!string.IsNullOrEmpty(fileName))
                        {
                            fileExtension = Path.GetExtension(fileName);
                            str_image = "Gambar_" + numFiles.ToString() + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                            string pathToSave_100 = HttpContext.Current.Server.MapPath("~/Images/") + str_image;
                        
                            gambar = DataLogic.getGambar(tgl);

                            if(gambar != ""){
                                var pathToDelete = HttpContext.Current.Server.MapPath("~/Images/") + gambar;
                                File.Delete(pathToDelete);
                                file.SaveAs(pathToSave_100);
                            }else{
                                file.SaveAs(pathToSave_100);  
                            }
                            DataLogic.InsertGambar(str_image, tgl);
                            stat = "1";
                        
                        }
                }
                //  database record update logic here  ()

                //context.Response.Write(str_image);
                context.Response.Clear();
                context.Response.ContentType = "application/text";
                context.Response.Write(stat);
            }
            catch (Exception ac)
            {

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}