using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;

namespace TMV.Static
{
   /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://static.tmvhoanganh.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class FileUpload : System.Web.Services.WebService
    {
        FileStream fs;

        [WebMethod]
        public string WSStartNewFile(string fileName, string _outputPath)
        {
            try
            {
                string _fileName = String.Empty;
                string _fullFileName = string.Empty;

                string path = Utilities.HtmlHelper.FolderPath(_outputPath);
                string originalPath = HttpContext.Current.Server.MapPath(path);

                if (Directory.Exists(originalPath) == false)
                    Directory.CreateDirectory(originalPath);

                _fileName = Utilities.HtmlHelper.StandardizeFileName(originalPath, fileName);
                _fullFileName = originalPath + _fileName;

                fs = new FileStream(_fullFileName, FileMode.Create);
                fs.Flush();
                fs.Close();

                string refpath = path + _fileName;
                return (_fullFileName + ";" + refpath);
            }
            catch 
            {               
                return "Error";
            }
        }

        [WebMethod]
        public bool WSWriteFile(byte[] buffer, string fullname)
        {
            try
            {
                fs = new FileStream(fullname, FileMode.Append);
                fs.Write(buffer, 0, buffer.Length);
                fs.Flush();                
                return true;
            }
            catch
            {
                return false;
            }
            finally {
                fs.Close();
            }
        }        
    }
}
