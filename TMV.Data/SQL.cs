using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using TMV.Utilities;
using Microsoft.ApplicationBlocks.Data;

namespace TMV.Data
{
    public class SQL
    {
        public static string ConnectString
        {
            get { return ConfigurationManager.ConnectionStrings["SiteDataServer"].ConnectionString; }
        }
        public static object GetNull(object field)
        {
            return Null.GetNull(field, DBNull.Value);
        }

        #region General
        public static void ExecuteSql(string sqlQueryString)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "ExecuteSql", sqlQueryString);
        }
        #endregion

        #region AdminPage
        public static int InsertAdminPage(int parentId, string name, string source, int orderView, bool visible, bool locked)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectString, "AdminPageInsert", GetNull(parentId), name, source, orderView, visible, locked));
        }

        public static void UpdateAdminPage(int adminPageId, int parentId, string name, string source, int orderView, bool visible, bool locked)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "AdminPageUpdate", adminPageId, GetNull(parentId), name, source, orderView, visible, locked);
        }

        public static void DeleteAdminPage(int adminPageId)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "AdminPageDelete", adminPageId);
        }

        public static IDataReader GetAdminPage(int adminPageId)
        {
            return SqlHelper.ExecuteReader(ConnectString, "AdminPageGet", adminPageId);
        }

        public static IDataReader ListAdminPage()
        {
            return SqlHelper.ExecuteReader(ConnectString, "AdminPageList");
        }
        #endregion

        #region AdminUser
        public static int InsertAdminUser(string username, string password, bool isAdministrator, bool deleted)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectString, "AdminUserInsert", username, password, isAdministrator, deleted));
        }

        public static int UpdateAdminUser(int userId, string username, string password, bool isAdministrator, bool deleted)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectString, "AdminUserUpdate", userId, username, password, isAdministrator, deleted));
        }

        public static void DeleteAdminUser(int userId)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "AdminUserDelete", userId);
        }

        public static IDataReader GetAdminUser(int userId)
        {
            return SqlHelper.ExecuteReader(ConnectString, "AdminUserGet", userId);
        }

        public static IDataReader GetAdminUser(string username)
        {
            return SqlHelper.ExecuteReader(ConnectString, "AdminUserGetByUsername", username);
        }

        public static IDataReader GetAdminUser(string username, string password)
        {
            return SqlHelper.ExecuteReader(ConnectString, "AdminUserLogin", username, password);
        }

        public static IDataReader ListAdminUser()
        {
            return SqlHelper.ExecuteReader(ConnectString, "AdminUserList");
        }
        public static IDataReader ListAdminUserByReturn()
        {
            return SqlHelper.ExecuteReader(ConnectString, "AdminUserListReturn");
        }

        #endregion

        #region AdminWorkFlow
        public static int InsertAdminWorkFlow(int fromId, int toId)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectString, "AdminWorkFlowInsert", fromId, toId));
        }

        public static int UpdateAdminWorkFlow(int workFlowId, int fromId, int toId)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectString, "AdminWorkFlowUpdate", workFlowId, fromId, toId));
        }

        public static void DeleteAdminWorkFlow(int workFlowId)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "AdminWorkFlowDelete", workFlowId);
        }

        public static IDataReader GetAdminWorkFlow(int workFlowId)
        {
            return SqlHelper.ExecuteReader(ConnectString, "AdminWorkFlowGet", workFlowId);
        }

        public static IDataReader ListAdminWorkFlow()
        {
            return SqlHelper.ExecuteReader(ConnectString, "AdminWorkFlowList");
        }
        #endregion

        #region AdminUserRole
        public static int InsertAdminUserRole(int userId, int roleId)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectString, "AdminUserRoleInsert", userId, roleId));
        }

        public static int UpdateAdminUserRole(int userRoleId, int userId, int roleId)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectString, "AdminUserRoleUpdate", userRoleId, userId, roleId));
        }

        public static void DeleteAdminUserRole(int userRoleId)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "AdminUserRoleDelete", userRoleId);
        }

        public static IDataReader GetAdminUserRole(int userRoleId)
        {
            return SqlHelper.ExecuteReader(ConnectString, "AdminUserRoleGet", userRoleId);
        }

        public static IDataReader ListAdminUserRole()
        {
            return SqlHelper.ExecuteReader(ConnectString, "AdminUserRoleList");
        }
        #endregion

        #region AdminRole
        /**Thêm mới một dòng trong bảng  AdminRole gồm 1 tham số truyền vào:Name */
        public static int InsertAdminRole(string roleName)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectString, "AdminRoleInsert", GetNull(roleName)));
        }
        /** Cập nhật bảng AdminRole gồm 1 tham số truyền vào:ID */
        public static void UpdateAdminRole(int roleId, string roleName)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "AdminRoleUpdate", GetNull(roleId), GetNull(roleName));
        }
        /** Xóa một dòng trong bảng AdminRole có 1 tham số: ID */
        public static void DeleteAdminRole(int roleId)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "AdminRoleDelete", GetNull(roleId));
        }
        /** Lấy một dòng trong bảng AdminRole theo ID */
        public static IDataReader GetAdminRole(int roleId)
        {
            return SqlHelper.ExecuteReader(ConnectString, "AdminRoleGet", GetNull(roleId));
        }
        /** Lấy dữ liệu trong bảng AdminRole */
        public static IDataReader ListAdminRole()
        {
            return SqlHelper.ExecuteReader(ConnectString, "AdminRoleList");
        }

        public static IDataReader ListAdminRoleToByUser(int userId)
        {
            return SqlHelper.ExecuteReader(ConnectString, "AdminRoleToByUser", userId);
        }

        /** Lấy danh sách Role của một userID*/
        public static IDataReader ListAdminUserRoleByUserID(int userid)
        {
            return SqlHelper.ExecuteReader(ConnectString, "AdminUserRoleListByUserID", GetNull(userid));
        }
        #endregion

        #region AdminPageRole
        public static int InsertAdminPageRole(int pageid, int roleid)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectString, "AdminPageRoleInsert", pageid, roleid));
        }
        public static void UpdateAdminPageRole(int id, int pageid, int roleid)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "AdminPageRoleUpdate", id, pageid, roleid);
        }
        public static void DeleteAdminPageRole(int id)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "AdminPageRoleDelete", id);
        }
        public static IDataReader GetAdminPageRole(int id)
        {
            return SqlHelper.ExecuteReader(ConnectString, "AdminPageRoleGet", id);
        }
        public static IDataReader ListAdminPageRole()
        {
            return SqlHelper.ExecuteReader(ConnectString, "AdminPageRoleList");
        }
        // lấy danh sách PageID theo các RoleID của 1 user id
        public static IDataReader ListAdminPageByRole(int userid)
        {
            return SqlHelper.ExecuteReader(ConnectString, "AdminPageRoleListByUserID", userid);
        }
        #endregion

        #region Article
        public static int InsertArticle(int categoryId, string title, string slug, string avatar, string description, string content, DateTime publishDate, int roleId, int authorId, int editorId, int approverId, string adminNote, string tags, string seoTitle, string seoDescription, string intro, string advantage, string process, string qa, string frontBack, string video, string review, string result, bool isTab)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectString, "ArticleInsert", GetNull(categoryId), GetNull(title), slug, GetNull(avatar), GetNull(description), GetNull(content), GetNull(publishDate), GetNull(roleId), GetNull(authorId), GetNull(editorId), GetNull(approverId), GetNull(adminNote), GetNull(tags), seoTitle, seoDescription, intro, advantage, process, qa, frontBack, video, review, result, isTab));
        }
        public static void UpdateArticle(int articleId, int categoryId, string title, string slug, string avatar, string description, string content, DateTime publishDate, int roleId, int authorId, int editorId, int approverId, string adminNote, string tags, string seoTitle, string seoDescription, string intro, string advantage, string process, string qa, string frontBack, string video, string review, string result, bool isTab)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "ArticleUpdate", articleId, categoryId, title, slug, avatar, GetNull(description), content, GetNull(publishDate), GetNull(roleId), GetNull(authorId), GetNull(editorId), GetNull(approverId), GetNull(adminNote), GetNull(tags), seoTitle, seoDescription, intro, advantage, process, qa, frontBack, video, review, result, isTab);
        }
        public static void DeleteArticle(int articleId)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "ArticleDelete", articleId);
        }
        public static IDataReader GetArticle(int articleId)
        {
            return SqlHelper.ExecuteReader(ConnectString, "ArticleGet", articleId);
        }
        public static IDataReader GetArticleBySlug(string slug)
        {
            return SqlHelper.ExecuteReader(ConnectString, "ArticleGetBySlug", slug);
        }
        public static IDataReader GetArticleByCategorySlug(string categorySlug)
        {
            return SqlHelper.ExecuteReader(ConnectString, "ArticleGetByCategorySlug", categorySlug);
        }
        public static IDataReader ListArticleByRole(int status, int userId)
        {
            return SqlHelper.ExecuteReader(ConnectString, "ArticleListByRole", status, userId);
        }
        public static IDataReader ListArticleByCategory(int categoryId, int page, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectString, "ArticleListByCategory", categoryId, page, pageSize);
        }
        public static IDataReader ListArticleByGroup(int categoryId, int group, int page, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectString, "ArticleListByGroup", categoryId, group, page, pageSize);
        }
        public static IDataReader ListArticleByView()
        {
            return SqlHelper.ExecuteReader(ConnectString, "ArticleListByView");
        }
        public static IDataReader ListArticleByOther(int articleId, int categoryId, int pageSize, int page)
        {
            return SqlHelper.ExecuteReader(ConnectString, "ArticleListByOther", articleId, categoryId, pageSize, page);
        }
        public static IDataReader ListArticleByTitle(string title)
        {
            return SqlHelper.ExecuteReader(ConnectString, "ArticleListByTitle", title);
        }
        #endregion

        #region Category
        public static int InsertCategory(int parentId, string categoryName, string slug, string seoH1, string seoTitle, string seoKeyword, string seoDescription, int group, string avatar, bool isShowDetail, bool isShowMenuTop, bool isShowMenuBot, int orderBy)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectString, "CategoryInsert", parentId, categoryName, slug, seoH1, seoTitle, seoKeyword, seoDescription, group, avatar, isShowDetail, isShowMenuTop, isShowMenuBot, orderBy));
        }
        public static void UpdateCategory(int categoryId, int parentId, string categoryName, string slug, string seoH1, string seoTitle, string seoKeyword, string seoDescription, int group, string avatar, bool isShowDetail, bool isShowMenuTop, bool isShowMenuBot, int orderBy)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "CategoryUpdate", categoryId, parentId, categoryName, slug, seoH1, seoTitle, seoKeyword, seoDescription, group, avatar, isShowDetail, isShowMenuTop, isShowMenuBot, orderBy);
        }
        public static void DeleteCategory(int categoryId)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "CategoryDelete", categoryId);
        }
        public static IDataReader GetCategory(int categoryId)
        {
            return SqlHelper.ExecuteReader(ConnectString, "CategoryGet", categoryId);
        }
        public static IDataReader ListCategory()
        {
            return SqlHelper.ExecuteReader(ConnectString, "CategoryList");
        }
        public static IDataReader ListCategoryByGroup(int group)
        {
            return SqlHelper.ExecuteReader(ConnectString, "CategoryListByGroup", group);
        }
        public static IDataReader ListCategoryTop()
        {
            return SqlHelper.ExecuteReader(ConnectString, "CategoryListTop");
        }
        public static IDataReader ListMenu(int menuType)
        {
            return SqlHelper.ExecuteReader(ConnectString, "Menu",menuType);
        }
        public static IDataReader ListCategoryBot()
        {
            return SqlHelper.ExecuteReader(ConnectString, "CategoryListBot");
        }
        public static IDataReader GetCategoryBySlug(string slug)
        {
            return SqlHelper.ExecuteReader(ConnectString, "CategoryGetBySlug", slug);
        }
        #endregion

        #region LinkWebsite
        public static int InsertLinkWebsite(string anchorText, string url, byte orderView, bool target, byte priority)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectString, "LinkWebsiteInsert", anchorText, url, orderView, target, priority));
        }
        public static void UpdateLinkWebsite(int linkWebsiteId, string anchorText, string url, byte orderView, bool target, byte priority)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "LinkWebsiteUpdate", linkWebsiteId, anchorText, url, orderView, target, priority);
        }
        public static void DeleteLinkWebsite(int linkWebsiteId)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "LinkWebsiteDelete", linkWebsiteId);
        }
        public static IDataReader ListLinkWebsite()
        {
            return SqlHelper.ExecuteReader(ConnectString, "LinkWebsiteList");
        }
        public static IDataReader ListLinkWebsiteByPriority(byte priority)
        {
            return SqlHelper.ExecuteReader(ConnectString, "LinkWebsiteListByPriority", priority);
        }
        #endregion

        #region Customer
        public static void InsertCustomer(string fullName, string avatar, string content, DateTime publishDate, int roleId, int authorId, int editorId, int approverId, string adminNote)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "CustomerInsert", fullName, avatar, content, GetNull(publishDate), GetNull(roleId), GetNull(authorId), GetNull(editorId), GetNull(approverId), GetNull(adminNote));
        }
        public static void UpdateCustomer(int customerId, string fullName, string avatar, string content, DateTime publishDate, int roleId, int authorId, int editorId, int approverId, string adminNote)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "CustomerUpdate", customerId, fullName, avatar, content, GetNull(publishDate), GetNull(roleId), GetNull(authorId), GetNull(editorId), GetNull(approverId), GetNull(adminNote));
        }
        public static void DeleteCustomer(int customerId)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "CustomerDelete", customerId);
        }
        public static IDataReader GetCustomer(int customerId)
        {
            return SqlHelper.ExecuteReader(ConnectString, "CustomerGet", customerId);
        }
        public static IDataReader ListCustomer()
        {
            return SqlHelper.ExecuteReader(ConnectString, "CustomerList");
        }
        public static IDataReader ListCustomerByHome()
        {
            return SqlHelper.ExecuteReader(ConnectString, "CustomerListByHome");
        }
        #endregion

        #region CustomerReview
        public static void InsertCustomerReview(int articleId, string title, string avatar, string content, DateTime publishDate, int roleId, int authorId, int editorId, int approverId, string adminNote)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "CustomerReviewInsert", articleId, title, avatar, content, GetNull(publishDate), GetNull(roleId), GetNull(authorId), GetNull(editorId), GetNull(approverId), GetNull(adminNote));
        }
        public static void UpdateCustomerReview(int customerReviewId, int articleId, string title, string avatar, string content, DateTime publishDate, int roleId, int authorId, int editorId, int approverId, string adminNote)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "CustomerReviewUpdate", customerReviewId, articleId, title, avatar, content, GetNull(publishDate), GetNull(roleId), GetNull(authorId), GetNull(editorId), GetNull(approverId), GetNull(adminNote));
        }
        public static void DeleteCustomerReview(int customerReviewId)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "CustomerReviewDelete", customerReviewId);
        }
        public static IDataReader GetCustomerReview(int customerReviewId)
        {
            return SqlHelper.ExecuteReader(ConnectString, "CustomerReviewGet", customerReviewId);
        }
        public static IDataReader ListCustomerReview(int articleId)
        {
            return SqlHelper.ExecuteReader(ConnectString, "CustomerReviewList", articleId);
        }
        public static IDataReader ListCustomerReviewByArticle(int articleId, int page, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectString, "CustomerReviewListByArticle", articleId, page, pageSize);
        }
        #endregion

        #region QuestionAnswer
        public static void InsertQuestionAnswer(int articleId, string title, string content, DateTime publishDate, int roleId, int authorId, int editorId, int approverId, string adminNote, string question, string slug)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "QuestionAnswerInsert", articleId, title, content, GetNull(publishDate), GetNull(roleId), GetNull(authorId), GetNull(editorId), GetNull(approverId), GetNull(adminNote), GetNull(question), slug);
        }
        public static void UpdateQuestionAnswer(int questionAnswerId, int articleId, string title, string content, DateTime publishDate, int roleId, int authorId, int editorId, int approverId, string adminNote, string question, string slug)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "QuestionAnswerUpdate", questionAnswerId, articleId, title, content, GetNull(publishDate), GetNull(roleId), GetNull(authorId), GetNull(editorId), GetNull(approverId), GetNull(adminNote), GetNull(question), slug);
        }
        public static void DeleteQuestionAnswer(int questionAnswerId)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "QuestionAnswerDelete", questionAnswerId);
        }
        public static IDataReader GetQuestionAnswer(int questionAnswerId)
        {
            return SqlHelper.ExecuteReader(ConnectString, "QuestionAnswerGet", questionAnswerId);
        }
        public static IDataReader ListQuestionAnswer(int articleId)
        {
            return SqlHelper.ExecuteReader(ConnectString, "QuestionAnswerList", articleId);
        }
        public static IDataReader ListQuestionAnswerByArticle(int articleId, int page, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectString, "QuestionAnswerListByArticle", articleId, page, pageSize);
        }
        public static IDataReader ListQuestionAnswerByOther(int articleId, int questionAnswerId, int page, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectString, "QuestionAnswerListByOther", articleId, questionAnswerId, page, pageSize);
        }
        public static IDataReader GetQuestionAnswerBySlug(string slug)
        {
            return SqlHelper.ExecuteReader(ConnectString, "QuestionAnswerGetBySlug", slug);
        }
        #endregion

        #region Picture
        public static void InsertPicture(string imagePath, string title, string description, string tags, string seoTitle, string seoDescription, bool isHome, DateTime startDate, DateTime endDate, int createdBy, string slug, string urlPath)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "PictureInsert", imagePath, title, description, tags, seoTitle, seoDescription, GetNull(isHome), GetNull(startDate), GetNull(endDate), createdBy, slug, urlPath);
        }
        public static void UpdatePicture(int pictureId, string imagePath, string title, string description, string tags, string seoTitle, string seoDescription, bool isHome, DateTime startDate, DateTime endDate, int createdBy, int updatedBy, string slug, string urlPath)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "PictureUpdate", pictureId, imagePath, title, description, tags, seoTitle, seoDescription, isHome, startDate, endDate, createdBy, updatedBy, slug, urlPath);
        }
        public static void DeletePicture(int pictureId)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "PictureDelete", pictureId);
        }
        public static IDataReader GetPicture(int pictureId)
        {
            return SqlHelper.ExecuteReader(ConnectString, "PictureGet", pictureId);
        }
        public static IDataReader ListPicture()
        {
            return SqlHelper.ExecuteReader(ConnectString, "PictureList");
        }
        public static IDataReader ListPictureByHome()
        {
            return SqlHelper.ExecuteReader(ConnectString, "PictureListByHome");
        }
        public static IDataReader ListPictureByPaging(int page, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectString, "PictureListByPaging", page, pageSize);
        }
        public static IDataReader GetPictureBySlug(string slug)
        {
            return SqlHelper.ExecuteReader(ConnectString, "PictureGetBySlug", slug);
        } 
        #endregion

        #region Video
        public static void InsertVideo(string url, string title, string description, string tags, string seoTitle, string seoDescription, bool isHome, DateTime startDate, DateTime endDate, int createdBy, string slug)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "VideoInsert", url, title, description, tags, seoTitle, seoDescription, GetNull(isHome), GetNull(startDate), GetNull(endDate), createdBy, slug);
        }
        public static void UpdateVideo(int videoId, string url, string title, string description, string tags, string seoTitle, string seoDescription, bool isHome, DateTime startDate, DateTime endDate, int createdBy, int updatedBy, string slug)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "VideoUpdate", videoId, url, title, description, tags, seoTitle, seoDescription, isHome, startDate, endDate, createdBy, updatedBy, slug);
        }
        public static void DeleteVideo(int videoId)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "VideoDelete", videoId);
        }
        public static IDataReader GetVideo(int videoId)
        {
            return SqlHelper.ExecuteReader(ConnectString, "VideoGet", videoId);
        }
        public static IDataReader ListVideo()
        {
            return SqlHelper.ExecuteReader(ConnectString, "VideoList");
        }
        public static IDataReader ListVideoByHome()
        {
            return SqlHelper.ExecuteReader(ConnectString, "VideoListByHome");
        }
        public static IDataReader ListVideoByPaging(int page, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectString, "VideoListByPaging", page, pageSize);
        }
        public static IDataReader GetVideoBySlug(string slug)
        {
            return SqlHelper.ExecuteReader(ConnectString, "VideoGetBySlug", slug);
        }
        #endregion

        #region Sponsor
        public static int InsertSponsor(int itemId, byte itemType, DateTime startDate, DateTime endDate)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectString, "SponsorInsert", itemId, itemType, startDate, endDate));
        }
        public static void UpdateSponsor(int sponsorId, int itemId, byte itemType, DateTime startDate, DateTime endDate)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "SponsorUpdate", sponsorId, itemId, itemType, startDate, endDate);
        }
        public static void DeleteSponsor(int sponsorId)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "SponsorDelete", sponsorId);
        }
        public static IDataReader GetSponsor(int sponsorId)
        {
            return SqlHelper.ExecuteReader(ConnectString, "SponsorGet", sponsorId);
        }
        public static IDataReader ListSponsor()
        {
            return SqlHelper.ExecuteReader(ConnectString, "SponsorList");
        }
        public static IDataReader ListSponsorByType(int categoryId)
        {
            return SqlHelper.ExecuteReader(ConnectString, "SponsorListByType", categoryId);
        }
        #endregion

        #region Rating
        public static IDataReader InsertRating(int userId, int itemId, int itemType, int price, int quality, int doctor, int attitude, int facility, int customer)
        {
            return SqlHelper.ExecuteReader(ConnectString, "RatingInsert", GetNull(userId), itemId, itemType, price, quality, doctor, attitude, facility, customer);
        }
        #endregion

        #region Banner
        public static void InsertBanner(string title, string imagePath, byte priority, DateTime startDate, DateTime endDate, bool isMobile, string content, int type, string slug, string url, int categoryId,int position)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "BannerInsert", title, imagePath, priority, startDate, endDate, isMobile, GetNull(content), type, GetNull(slug), GetNull(url), GetNull(categoryId), GetNull(position));
        }
        public static void UpdateBanner(int bannerId, string title, string imagePath, byte priority, DateTime startDate, DateTime endDate, bool isMobile, string content, int type, string slug, string url, int categoryId,int position)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "BannerUpdate", bannerId, title, imagePath, priority, startDate, endDate, isMobile, GetNull(content), type, GetNull(slug), GetNull(url), GetNull(categoryId), GetNull(position));
        }
        public static void DeleteBanner(int bannerId)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "BannerDelete", bannerId);
        }
        public static IDataReader GetBanner(int bannerId)
        {
            return SqlHelper.ExecuteReader(ConnectString, "BannerGet", bannerId);
        }
        public static IDataReader GetBannerBySlug(string slug)
        {
            return SqlHelper.ExecuteReader(ConnectString, "BannerGetBySlug", slug);
        }
        public static IDataReader ListBanner()
        {
            return SqlHelper.ExecuteReader(ConnectString, "BannerList");
        }
        public static IDataReader ListBannerPrice()
        {
            return SqlHelper.ExecuteReader(ConnectString, "BannerListPrice");
        }
        public static IDataReader ListBannerByPriority(byte priority)
        {
            return SqlHelper.ExecuteReader(ConnectString, "BannerListByPriority", priority);
        }
        public static IDataReader GetBannerByCategoryId(int categoryId, bool isMobile)
        {
            return SqlHelper.ExecuteReader(ConnectString, "BannerGetByCategoryId", categoryId, isMobile);
        }
        #endregion

        #region HyperLink
        public static void InsertHyperLink(string oldChar, string newChar)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "HyperLinkInsert", oldChar, newChar);
        }
        public static void UpdateHyperLink(int hyperLinkId, string oldChar, string newChar)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "HyperLinkUpdate", hyperLinkId, oldChar, newChar);
        }
        public static void DeleteHyperLink(int hyperLinkId)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "HyperLinkDelete", hyperLinkId);
        }
        public static IDataReader ListHyperLink()
        {
            return SqlHelper.ExecuteReader(ConnectString, "HyperLinkList");
        }
        #endregion

        #region Register
        public static int InsertRegister(bool gender, string fullName, string phoneNumber, string email, string content, string khoaHoc, DateTime? ngayHoc)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectString, "RegisterInsert", gender, fullName, phoneNumber, email, content,khoaHoc,SQL.GetNull(ngayHoc)));
        }
        public static void UpdateRegister(int registerId, bool gender, string fullName, string phoneNumber, string email, string content)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "RegisterUpdate", registerId, gender, fullName, phoneNumber, email, content);
        }
        public static void UpdateRegisterStatus(int registerId, int status)
        {
            SqlHelper.ExecuteNonQuery(ConnectString, "RegisterUpdateStatus", registerId, status);
        }
        public static IDataReader ListRegisterByStatus(int status)
        {
            return SqlHelper.ExecuteReader(ConnectString, "RegisterListByStatus", status);
        }
        #endregion
    }
}
