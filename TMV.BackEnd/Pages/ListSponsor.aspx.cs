using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMV.Framework;

namespace TMV.BackEnd.Pages
{
    public partial class ListSponsor : AdminPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridViewManager1.LoadData();
        }
    }
}