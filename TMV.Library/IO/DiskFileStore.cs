using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMV.Utilities;
using System.Web;
using System.IO;

namespace TMV.IO
{
    public interface IFileStore
    {
        string SaveUploadedFile(HttpPostedFile fileBase);
    }

    public class DiskFileStore : IFileStore
    {
        FileUpload _wsFileUpload = null;

        private string GetFilePath()
        {
            return Globals.PathUploadFile;
        }


        byte[] GetByteArray(Stream inputStream)
        {
            int length = Convert.ToInt32(inputStream.Length);
            byte[] b = new byte[length];
            inputStream.Read(b, 0, length);
            inputStream.Close();
            return b;
        }

        public string SaveUploadedFile(HttpPostedFile fileBase)
        {
            try
            {
                _wsFileUpload = new FileUpload();
                string path =  _wsFileUpload.WSStartNewFile(fileBase.FileName, GetFilePath());
                if (path.Equals("Error"))
                    throw new Exception("Error: webservice  || raw: WSStartNewFile function");

                string _fullFileName = path.Substring(0, path.LastIndexOf(";"));
                string _fileName = path.Substring(path.LastIndexOf(";") + 1);
                if (_wsFileUpload.WSWriteFile(GetByteArray(fileBase.InputStream), _fullFileName))
                {
                    return "{ \"image_handler\" :\"" + Globals.StaticUrl + "\", \"filename\" :\"" + _fileName + "\"}";
                }
                else
                    throw new Exception("Error: webservice  || raw: WSWriteFile function");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}
