using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMV.Data.Entities;
using TMV.Utilities;

namespace Newspaper.FromtEnd.Controllers
{
    public class AjaxController : Controller
    {
        private bool _isClearCache = false;
        public AjaxController()
        {
            _isClearCache = System.Web.HttpContext.Current.Request.QueryString["ClearCache"] != null;
        }

        public ActionResult CustomerReview(int articleId)
        {
            var customerReviews = new CustomerReviewController().ListCustomerReviewByArticle(articleId, 0, -1, _isClearCache);
            return PartialView(customerReviews);
        }
        public ActionResult QuestionAnswer(int articleId, int questionAnswerId = 0)
        {
            var questionAnswers = new QuestionAnswerController().ListQuestionAnswerByArticle(articleId, 0, -1, _isClearCache);

            ViewBag.QuestionAnswerId = questionAnswerId;
            return PartialView(questionAnswers);
        }
        public ActionResult AddRating(int itemId, int itemType, int price, int quality, int doctor, int attitude, int facility, int customer)
        {
            var objRating = new RatingInfo() { 
                UserId = -1,
                ItemId = itemId,
                ItemType = itemType,
                Price = price,
                Quality = quality,
                Doctor = doctor, 
                Attitude = attitude,
                Facility = facility,
                Customer = customer
            };
            var result = new RatingController().InsertRating(objRating);
            return PartialView(result);
        }
        public string ViewPrice(int bannerId)
        {
            var objBanner = new BannerController().GetBanner(bannerId);
            return Globals.ImageUrlNoCDN(objBanner.ImagePath, 800, true);
        }
        public ActionResult BoxMap()
        {
            return PartialView();
        }

        #region Đăng ký tư vấn dịch vụ
        public ActionResult PopupRegister()
        {
            return PartialView();
        }
        public ActionResult KhoaHocInsert(FormCollection fCol)
        {
            var fullName = fCol["txtFullNameRG"] ?? "";
            var phoneNumber = fCol["txtPhoneNumberRG"] ?? "";
            var email = fCol["txtEmailRG"] ?? "";
            var noidung = fCol["txtContentRG"] ?? "";

            if (string.IsNullOrEmpty(fullName)) return ReturnJson("Quý khách vui lòng nhập họ tên.");
            if (!TMV.Utilities.HtmlHelper.ValidatePhoneNumber(phoneNumber))
                return ReturnJson("Số điện thoại trống/không đúng định dạng.");

            var objRegister = new RegisterInfo()
            {
                FullName = fullName,
                PhoneNumber = phoneNumber,
                Email = email,
                Content = noidung
            };

            var flagInsert = new RegisterController().InsertRegister(objRegister);
            if (flagInsert <= 0) return ReturnJson("Chức năng đang bảo trì. Quý khách vui lòng thử lại.");

            #region Send mail
            var to = Globals.EmailTo;
            //var to = "tien.lam.3000@gmail.com";
            var bcc = "";
            var cc = "";
            var subject = "Đăng ký tư vấn từ website TMVHoangAnh.Com";
            var body = string.Format("Họ tên: {0}<br />Số điện thoại: {1}<br />Email: {2}<br />Nội dung: {3}",
                fullName,
                phoneNumber,
                email,
                noidung
            );

            var isSend = MailHelper.SendEmailDirect(to, bcc, cc, subject, body);
            #endregion

            return ReturnJson("Success", 1);
        }

        public ActionResult KhoaHocServiceInsert(FormCollection fCol)
        {
            var fullName = fCol["txtFullNameSV"] ?? "";
            var phoneNumber = fCol["txtPhoneNumberSV"] ?? "";
            var email = fCol["txtEmailSV"] ?? "";
            var noidung = fCol["txtContentSV"] ?? "";

            if (string.IsNullOrEmpty(fullName)) return ReturnJson("Quý khách vui lòng nhập họ tên.");
            if (!TMV.Utilities.HtmlHelper.ValidatePhoneNumber(phoneNumber))
                return ReturnJson("Số điện thoại trống/không đúng định dạng.");

            var objRegister = new RegisterInfo()
            {
                FullName = fullName,
                PhoneNumber = phoneNumber,
                Email = email,
                Content = noidung
            };

            var flagInsert = new RegisterController().InsertRegister(objRegister);
            if (flagInsert <= 0) return ReturnJson("Chức năng đang bảo trì. Quý khách vui lòng thử lại.");

            #region Send mail
            var to = Globals.EmailTo;
            //var to = "tien.lam.3000@gmail.com";
            var bcc = "";
            var cc = "";
            var subject = "Đăng ký dịch vụ từ website TMVHoangAnh.Com";
            var body = string.Format("Họ tên: {0}<br />Số điện thoại: {1}<br />Email: {2}<br />Nội dung: {3}",
                fullName,
                phoneNumber,
                email,
                noidung
            );

            var isSend = MailHelper.SendEmailDirect(to, bcc, cc, subject, body);
            #endregion

            return ReturnJson("Success", 1);
        }
        public ActionResult RegisterKhoaHocInsert(FormCollection fCol)
        {
            var fullName = fCol["txtFullNameRG"] ?? "";
            var phoneNumber = fCol["txtPhoneNumberRG"] ?? "";
            var khoahoc = fCol["txtKhoaHocRG"] ?? "";
            var ngaydk = fCol["txtNgayDangKyRG"] ?? "";

            if (string.IsNullOrEmpty(fullName)) return ReturnJson("Quý khách vui lòng nhập họ tên.");
            if (!TMV.Utilities.HtmlHelper.ValidatePhoneNumber(phoneNumber))
                return ReturnJson("Số điện thoại trống/không đúng định dạng.");
            if (string.IsNullOrEmpty(khoahoc)) return ReturnJson("Quý khách vui lòng chọn khóa học.");
            if (string.IsNullOrEmpty(ngaydk)) return ReturnJson("Quý khách vui lòng chọn ngày đăng ký học.");
            
            var objRegister = new RegisterInfo()
            {
                FullName = fullName,
                PhoneNumber = phoneNumber,
                KhoaHoc = khoahoc,
                NgayHoc= Convert.ToDateTime(ngaydk)
            };

            var flagInsert = new RegisterController().InsertRegister(objRegister);
            if (flagInsert <= 0) return ReturnJson("Chức năng đang bảo trì. Quý khách vui lòng thử lại.");

            #region Send mail
            var to = Globals.EmailTo;
            //var to = "tien.lam.3000@gmail.com";
            var bcc = "";
            var cc = "";
            var subject = "Đăng ký dịch vụ từ website NÂNG CHÂN MÀY";
            var body = string.Format("Họ tên: {0}<br />Số điện thoại: {1}<br />Khóa Học: {2}<br />Ngày học: {3}",
                fullName,
                phoneNumber,
                khoahoc,
                ngaydk
            );

            var isSend = MailHelper.SendEmailDirect(to, bcc, cc, subject, body);
            #endregion

            return ReturnJson("Success", 1);
        }

        public ActionResult LienHeInsert(FormCollection fCol)
        {
            var fullName = fCol["txtFullName"] ?? "";
            var phone = fCol["txtPhone"] ?? "";
            var email = fCol["txtEmail"] ?? "";
            var content = fCol["txtContent"] ?? "";

            if (string.IsNullOrEmpty(fullName)) return ReturnJson("Quý khách vui lòng nhập họ tên.");
            if (!TMV.Utilities.HtmlHelper.ValidatePhoneNumber(phone))
                return ReturnJson("Số điện thoại trống/không đúng định dạng.");
            if (!TMV.Utilities.HtmlHelper.ValidateEmail(email))
                return ReturnJson("Email trống/không đúng định dạng.");
            if (string.IsNullOrEmpty(content)) return ReturnJson("Quý khách vui lòng nhập nội dung yêu cầu.");

            //var objRegister = new RegisterInfo()
            //{
            //    Gender = gender == 1,
            //    FullName = fullName,
            //    PhoneNumber = phoneNumber,
            //    Email = email,
            //    Content = content
            //};
            //var flagInsert = new RegisterController().InsertRegister(objRegister);
            //if (flagInsert <= 0) return ReturnJson("Chức năng đang bảo trì. Quý khách vui lòng thử lại.");

            #region Send mail
            var to = Globals.EmailTo;
            //var to = "tien.lam.3000@gmail.com";
            var bcc = "";
            var cc = "";
            var subject = "Liên hệ từ website ĐÀO TẠO PHUN XĂM";
            var body = string.Format("Họ tên: {0} <br />Email: {1}<br />Số điện thoại: {2}<br />Nội dung: {3}",
                fullName,
                email,
                phone,
                content
            );
            var isSend = MailHelper.SendEmailDirect(to, bcc, cc, subject, body);
            #endregion

            return ReturnJson("Success", 1);
        }
        #endregion
        

        private JsonResult ReturnJson(string error, int status = -1)
        {
            return Json(new
            {
                status = status,
                error = error
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
