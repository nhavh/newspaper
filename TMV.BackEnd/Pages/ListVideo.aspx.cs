using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMV.Data.Entities;
using TMV.Framework;
using TMV.Utilities;
using TMV.WebControls;

namespace TMV.BackEnd.Pages
{
    public partial class ListVideo : AdminPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridViewManager1.LoadData();
        }
    }
}