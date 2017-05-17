using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMV.BackEnd
{
    public partial class SaveFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Response.Write(new TMV.IO.DiskFileStore().SaveUploadedFile(this.Request.Files[0]));

        }
    }
}
