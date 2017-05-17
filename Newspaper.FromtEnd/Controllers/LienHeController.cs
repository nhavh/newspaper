using System.Web.Mvc;
using TMV.Data.Entities;

namespace Newspaper.FromtEnd.Controllers
{
    public class LienHeController : Controller
    {
        private bool _isClearCache = false;

        public LienHeController()
        {
            _isClearCache = System.Web.HttpContext.Current.Request.QueryString["ClearCache"] != null;
        }


        [WhitespaceFilter]
        public ActionResult Index()
        {
            var objCategory = new CategoryController().GetCategoryBySlug("lien-he", _isClearCache);

            ViewBag.ObjCategory = objCategory;
            ViewBag.LineTitle = "Liên hệ";
            return View();
        }


    }
}
