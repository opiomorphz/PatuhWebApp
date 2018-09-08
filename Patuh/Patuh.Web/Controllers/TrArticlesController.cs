

using System.Web.Mvc;
using System;
using MvcContrib;
using System.Collections.Generic;
using AutoMapper;
using MvcContrib.Pagination;

using System.Linq;
using MvcContrib.UI.Grid;
using Newtonsoft.Json;
using System.IO;
using Patuh.Web.Controllers;
using Patuh.Web.Helpers;
using Patuh.Infrastructure;
using APP.Framework.Infrastructure;
using Patuh.Domain;
using Patuh.Web.Controllers.ViewModels;
using Patuh.Web.Utils;
using System.Drawing;

namespace SOPCompliance.Web.Mvc.Controllers
{
    [HandleError]
    public class TrArticlesController : BaseController
    {
        private MsUserDataAccess msUserDataAccess;
        private MsUserRoleDataAccess msUserRoleDataAccess;
        private TrArticleDataAccess trArticleDataAccess;
        private TrImageAttachmentDataAccess trImageAttachmentDataAccess; 

        public TrArticlesController()
        { 
            DAL dal = new DAL();
            dal.ApplicationMode = APP.Framework.Enumeration.ApplicationMode.Testing;
            msUserDataAccess = new MsUserDataAccess(dal);
            msUserRoleDataAccess = new MsUserRoleDataAccess(dal);
            trArticleDataAccess = new TrArticleDataAccess(dal);
            trImageAttachmentDataAccess = new TrImageAttachmentDataAccess(dal);
        }

		public ActionResult Index(int? page)
        {
            BootstrapBreadcrumbHelper bcrumb = new BootstrapBreadcrumbHelper();        
            
            bcrumb.AddNode(Url.Action("Index", "Home"), "Home");
            bcrumb.AddNode(Url.Action("Index", "TrArticles"), "Artikel");

            ViewBag.Breadcrumb = bcrumb.Output();


            page = page == null ? 1 : page;
            string whereClause = "Category = 'ARTICLE'";
            string OrderBy = "";
            if (Request["Search"]==null)
            {
                foreach (var item in Request.Form.AllKeys )
                {
                    if (Request[item] != null && Request[item] !="")
                    {
                        if (whereClause !=""){
                            whereClause = " AND ";
                        }
                        whereClause += item + " = '" + Request[item] + "'";
                    }
                }
            }
            else
            {
                if (Request["searchWord"] !="" || Request["searchWord"] !=null){
                    whereClause = string.Format("Title like '%{0}%' OR Story like '%{0}%'", Request["searchWord"]);
                    }

            }


            IPagination<TrArticle> listArticles = trArticleDataAccess.GetTrArticleListPaginated(whereClause, "Id DESC", page.Value, 10);

            //IMsUserAccessesQuery Query = new MsUserAccessesListQuery();
            //IPagination<MsUserAccessDto> listMsUserAccessViewModel = Query.GetPagedQRY(2, page.Value, DataGlobals.ROW_PAGE, whereClause, OrderBy);


            return View(listArticles);
        }

        [HttpGet]
        public ActionResult CreateOrUpdate(int id)
        {
            BootstrapBreadcrumbHelper bcrumb = new BootstrapBreadcrumbHelper();        
            
            bcrumb.AddNode(Url.Action("Index", "Home"), "Home");
            bcrumb.AddNode(Url.Action("Index", "TrArticles"), "Artikel");
            bcrumb.AddNode(Url.Action("CreateOrUpdate", "TrArticles"), "Create Or Update");

            ViewBag.Breadcrumb = bcrumb.Output();


            TrArticle trArticle = trArticleDataAccess.GetArticleById(id);
            TrArticleViewModel trArticleViewModel = new TrArticleViewModel();

            if (trArticle != null)
            {
                trArticleViewModel.Title = trArticle.Title;
                trArticleViewModel.Story = trArticle.Story;
                trArticleViewModel.listImage = getListImages(id);
            }

            return View(trArticleViewModel);
        }


        [HttpGet]
        public ActionResult SetStatus(int id, string status)
        {
            string msg = "Artikel berhasil diupdate";
            try
            {
                TrArticle trArticle = trArticleDataAccess.GetArticleById(id);
                trArticle.cStatus = status; //trArticle.cStatus == "Y" ? "N" : "Y";

                trArticleDataAccess.Update(ref trArticle);


            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            return this.Json(new {message= msg}, JsonRequestBehavior.AllowGet);
        }

       


        [HttpGet]
        public ActionResult downloadImage(int id)
        {
            
            TrImageAttachment theImage = trImageAttachmentDataAccess.GetAttachmentById(id);

            Image img = null;
            if (theImage != null)
            {
                return File(theImage.Image, "image/jpg", "image.jpg");
            }

            return null;
        }
       
        private IList<TrImageAttachmentViewModel> getListImages(int headerId)
        {
            IList<TrImageAttachmentViewModel> listImageViewModels = new List<TrImageAttachmentViewModel>();

            IList<TrImageAttachment> listImageObjs =  trImageAttachmentDataAccess.GetListAttachmentByHeaderId(headerId);

            if (listImageObjs != null && listImageObjs.Count > 0)
            {
                foreach (TrImageAttachment img in listImageObjs)
                {

                    TrImageAttachmentViewModel row = new TrImageAttachmentViewModel();
                    row.Id = img.Id;
                    row.HeaderId = headerId;
                    row.Image = MakeThumbnail(img.Image,150,150);
                    row.Sequence = img.Sequence;
                    row.cCreated = img.cCreated;
                    row.dCreated = img.dCreated;
                    row.cLastUpdated = img.cLastUpdated;
                    row.dLastUpdated = img.dLastUpdated;

                    listImageViewModels.Add(row);
                }
            }

            return listImageViewModels;
        }


        public static byte[] MakeThumbnail(byte[] myImage, int thumbWidth, int thumbHeight)
        {
            using (MemoryStream ms = new MemoryStream())
            using (Image thumbnail = Image.FromStream(new MemoryStream(myImage)).GetThumbnailImage(thumbWidth, thumbHeight, null, new IntPtr()))
            {
                thumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
        
    }
}
