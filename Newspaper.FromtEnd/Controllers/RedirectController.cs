using System.Web.Mvc;

namespace Newspaper.FromtEnd.Controllers
{
    public class RedirectController : Controller
    {
        //
        // GET: /Redirect/

        public ActionResult RedirectKienThuc()
        {
            return Redirect("/kien-thuc/");
        }

        public ActionResult RedirectKienThucDetail(string slug)
        {
            return Redirect("/kien-thuc/" + slug + "/");
        }

        public ActionResult RedirectKienThucDetail100()
        {
            return Redirect("/kien-thuc/cham-soc-da-theo-do-tuoi/");
        }

        public ActionResult RedirectKienThucDetail101()
        {
            return Redirect("/kien-thuc/hoc-cham-soc-da-o-dau/");
        }

        public ActionResult RedirectKienThucDetail102()
        {
            return Redirect("/kien-thuc/truong-hop-nao-nen-cat-mi-mat-o-nguoi-lon-tuoi/");
        }
        public ActionResult RedirectKienThucDetail103()
        {
            return Redirect("/kien-thuc/truong-hop-nao-nen-ap-dung-phuong-phap-cat-mi-mat/");
        }
        public ActionResult RedirectLienHe()
        {
            return Redirect("/lien-he/");
        }
        public ActionResult RedirectKienThucDetail105()
        {
            return Redirect("/bang-gia/bang-gia-tham-my-vung-mat/");
        }
        public ActionResult RedirectKienThucDetail106()
        {
            return Redirect("/bang-gia/bang-gia-phau-thuat-nang-nguc/");
        }
        public ActionResult RedirectKienThucDetail107()
        {
            return Redirect("/bang-gia/bang-gia-hut-mo-bung/");
        }
        public ActionResult RedirectKienThucDetail108()
        {
            return Redirect("/bang-gia/bang-gia-dieu-tri-da/");
        }
        public ActionResult RedirectKienThucDetail109()
        {
            return Redirect("/bang-gia/bang-gia-khoa-hoc-phun-theu/");
        }
        public ActionResult RedirectKienThucDetail110()
        {
            return Redirect("/bang-gia/bang-gia-cham-soc-da/");
        }
        public ActionResult RedirectKienThucDetail111()
        {
            return Redirect("/bung-dui/");
        }
        public ActionResult RedirectKienThucDetail112()
        {
            return Redirect("/bac-si/bac-sy-kim/");
        }
        public ActionResult RedirectKienThucDetail113()
        {
            return Redirect("/bac-si/bac-sy-hoang/");
        }
        public ActionResult RedirectKienThucDetail114()
        {
            return Redirect("/phau-thuat-tham-my/");
        }
        public ActionResult RedirectKienThucDetail115()
        {
            return Redirect("/tham-my-noi-khoa/");
        }
        public ActionResult RedirectKienThucDetail116()
        {
            return Redirect("/dao-tao-hoc-vien/");
        }
        public ActionResult RedirectKienThucDetail117()
        {
            return Redirect("/phun-theu-tham-my/");
        }
        public ActionResult RedirectKienThucDetail118()
        {
            return Redirect("/spa-cham-soc-da/");
        }
        public ActionResult RedirectKienThucDetail119()
        {
            return Redirect("/bam-mi-mat/");
        }
        public ActionResult RedirectKienThucDetail120()
        {
            return Redirect("/triet-long-vinh-vien/");
        }
    }
}
