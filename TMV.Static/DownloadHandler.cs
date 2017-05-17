using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using TMV.Utilities;

namespace TMV.Static
{
    public class DownLoadHandler : IHttpHandler
    {
        private const string FILE_PARAM = "File";
        private const string DOC_PARAM = "Doc";
        private const string FILE_PARAM_NEW = "Files";
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString[FILE_PARAM] != null)
            {
                string file = context.Request.QueryString[FILE_PARAM].Trim().ToLower().Replace("\\", "/");
                if (file.IndexOf("~/") != 0)
                    file = "~/" + file;

                file = context.Server.MapPath(file);
                if (File.Exists(file))
                    DownloadFile(file);
                else
                {
                    context.Response.Write("File Not Found");
                    context.Response.End();
                }
            }
            else if (context.Request.QueryString[FILE_PARAM_NEW] != null)
            {
                var file = context.Request.QueryString[FILE_PARAM_NEW].Trim().ToLower().Replace("\\", "/");

                var fullPath = Globals.StaticPathUploadFile + file;
                if (fullPath.IndexOf("~/") != 0)
                    fullPath = "~/" + fullPath;

                file = context.Server.MapPath(fullPath);
                if (File.Exists(file))
                    DownloadFile(file);
                else
                {
                    context.Response.Write("File Not Found");
                    context.Response.End();
                }
            }
            else if (context.Request.QueryString[DOC_PARAM] != null)
            {
                var file = context.Request.QueryString[DOC_PARAM].Trim().ToLower().Replace("\\", "/");

                var fullPath = Globals.StaticPathUploadDocument + file;
                if (fullPath.IndexOf("~/") != 0)
                    fullPath = "~/" + fullPath;

                file = context.Server.MapPath(fullPath);
                if (File.Exists(file))
                    DownloadFile(file);
                else
                {
                    context.Response.Write("Document Not Found");
                    context.Response.End();
                }
            }
        }

        private static void DownloadFile(string FileLoc)
        {
            FileInfo objFile = new FileInfo(FileLoc);
            System.Web.HttpResponse objResponse = System.Web.HttpContext.Current.Response;
            string filename = objFile.Name;
            if (HttpContext.Current.Request.UserAgent.IndexOf("; MSIE ") > 0)
                filename = HttpUtility.UrlEncode(filename);

            if (objFile.Exists)
            {
                objResponse.ClearContent();
                objResponse.ClearHeaders();
                objResponse.AppendHeader("Content-Length", objFile.Length.ToString());
                objResponse.ContentType = GetContentType(objFile.Extension.Replace(".", ""));
                if (objResponse.ContentType.Contains("x-shockwave-flash") || objResponse.ContentType.Contains("video"))
                    objResponse.AddHeader("Content-Disposition", "inline;filename=\"" + filename + "\"");
                else
                    objResponse.AppendHeader("content-disposition", "attachment; filename=\"" + filename + "\"");

                WriteFile(objFile.FullName);

                objResponse.Flush();
                objResponse.End();
            }
        }

        private static string GetContentType(string extension)
        {
            string contentType;
            switch (extension.ToLower())
            {
                case "txt":
                    contentType = "text/plain"; break;
                case "htm":
                case "html":
                    contentType = "text/html"; break;
                case "rtf":
                    contentType = "text/richtext"; break;
                case "jpg":
                case "jpeg":
                    contentType = "image/jpeg"; break;
                case "gif": contentType = "image/gif"; break;
                case "bmp": contentType = "image/bmp"; break;
                case "mpg":
                case "mpeg": contentType = "video/mpeg"; break;
                case "avi": contentType = "video/avi"; break;
                case "flv": contentType = "video/x-flv"; break;
                case "swf": contentType = "application/x-shockwave-flash"; break;
                case "pdf": contentType = "application/pdf"; break;
                case "doc":
                case "dot": contentType = "application/msword"; break;
                case "docx": contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"; break;
                case "csv":
                case "xls":
                case "xlt": contentType = "application/x-msexcel"; break;
                case "xlsx": contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; break;
                default: contentType = "application/octet-stream"; break;
            }
            return contentType;
        }

        private static void WriteFile(string strFileName)
        {
            System.Web.HttpResponse objResponse = System.Web.HttpContext.Current.Response;
            System.IO.Stream objStream = null;

            try
            {
                objStream = new System.IO.FileStream(strFileName, System.IO.FileMode.Open, FileAccess.Read, FileShare.Read);

                WriteStream(objResponse, objStream);
            }
            catch (Exception ex)
            {
                objResponse.Write("Error : " + ex.Message);
            }
            finally
            {
                if (objStream != null)
                    objStream.Close();
            }
        }

        private static void WriteStream(HttpResponse objResponse, Stream objStream)
        {
            // Buffer to read 10K bytes in chunk:
            byte[] bytBuffer = new byte[10000];

            //Length of the file:
            int intLength;

            // Total bytes to read:
            long lngDataToRead;

            try
            {
                //Total bytes to read:
                lngDataToRead = objStream.Length;

                // Read the bytes.
                while (lngDataToRead > 0)
                {
                    //Verify that the client is connected.
                    if (objResponse.IsClientConnected)
                    {
                        //Read the data in buffer
                        intLength = objStream.Read(bytBuffer, 0, 10000);

                        // Write the data to the current output stream.
                        objResponse.OutputStream.Write(bytBuffer, 0, intLength);

                        // Flush the data to the HTML output.
                        objResponse.Flush();

                        bytBuffer = new byte[10000];      // Clear the buffer
                        lngDataToRead = lngDataToRead - intLength;
                    }
                    else
                    {
                        //prevent infinite loop if user disconnects
                        lngDataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                // Trap the error, if any.
                objResponse.Write("Error : " + ex.Message);
            }
            finally
            {
                if (objStream != null)
                    objStream.Close();
            }
        }
    }        
}
