using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using TMV.Data.Entities;
using TMV.Library.Utilities;

namespace TMV.Services.HttpHandlers
{
    public class AjaxHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            #region Upload Image Ajax
            //if (!String.IsNullOrEmpty(context.Request["function"]) && context.Request["function"].Equals("UploadImageAjax"))
            //{
            //    var imageEncode = context.Request["imageData"];
            //    var imageData = ImageUpload.Base64ToImage(imageEncode);
            //    var imageSize = int.Parse(context.Request["imageSize"]);
            //    UploadImageAjax(context, imageData, imageSize);
            //}
            #endregion
        }
        public bool IsReusable
        {
            get { return true; }
        }
    }
}
