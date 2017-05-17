using System.Web.Mvc;
using TMV.Data.Entities;

namespace Newspaper.FromtEnd.Controllers
{
    public class CustomerRVController : Controller
    {
         private bool _isClearCache = false;

         public CustomerRVController()
        {
            _isClearCache = System.Web.HttpContext.Current.Request.QueryString["ClearCache"] != null;
        }

        public ActionResult Index()
        {
            var objCategory = new CategoryController().GetCategoryBySlug("cam-nhan-hoc-vien", _isClearCache);

            ViewBag.ObjCategory = objCategory;

            var customerReviews = new CustomerReviewController().ListCustomerReviewByArticle(0, 1, 1000, _isClearCache);

            return View(customerReviews);
        }

    }
}
