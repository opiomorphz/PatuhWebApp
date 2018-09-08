

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
using System.Web;
using System.Drawing;

namespace SOPCompliance.Web.Mvc.Controllers
{
    [HandleError]
    public class TrNewsController : BaseController
    {
        private MsUserDataAccess msUserDataAccess;
        private MsUserRoleDataAccess msUserRoleDataAccess;
        private TrArticleDataAccess trArticleDataAccess;
        private TrImageAttachmentDataAccess trImageAttachmentDataAccess;

        public TrNewsController()
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
            bcrumb.AddNode(Url.Action("Index", "TrNews"), "Berita");

            ViewBag.Breadcrumb = bcrumb.Output();


            page = page == null ? 1 : page;
            string whereClause = "Category = 'NEWS'";
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
            bcrumb.AddNode(Url.Action("Index", "TrNews"), "Berita");
            bcrumb.AddNode(Url.Action("CreateOrUpdate", "TrNews"), "Add/Edit Berita");

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
        public ActionResult SetStatus(int id)
        {
            string msg = "Artikel berhasil diupdate";
            try
            {
                TrArticle trArticle = trArticleDataAccess.GetArticleById(id);
                trArticle.cStatus = trArticle.cStatus == "Y" ? "N" : "Y";

                trArticleDataAccess.Update(ref trArticle);


            }
            catch (Exception e)
            {
                msg = e.Message;
            }



            return this.Json(new {message= msg}, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateOrUpdate(TrArticleViewModel trArticleViewModel, IEnumerable<HttpPostedFileBase> imageFiles)
        {
            BootstrapBreadcrumbHelper bcrumb = new BootstrapBreadcrumbHelper();

            string a = Request["a"];

            bcrumb.AddNode(Url.Action("Index", "Home"), "Home");
            bcrumb.AddNode(Url.Action("Index", "TrNews"), "Berita");
            bcrumb.AddNode(Url.Action("CreateOrUpdate", "TrNews"), "Add/Edit Berita");


            TrArticle trArticle = trArticleDataAccess.GetArticleById(trArticleViewModel.Id);
            try
            {
                if (trArticle == null)
                {
                    trArticle = new TrArticle();
                    trArticle.Category = "NEWS";
                    trArticle.Title = trArticleViewModel.Title;
                    trArticle.Story = trArticleViewModel.Story;
                    trArticle.dCreated = DateTime.Now;
                    trArticle.cCreated = HttpContext.User.Identity.Name;
                    trArticle.RowState = System.Data.DataRowState.Added;
                    trArticleDataAccess.Insert(ref trArticle);
                }
                else
                {
                    trArticle.Title = trArticleViewModel.Title;
                    trArticle.Story = trArticleViewModel.Story;
                    trArticle.dLastUpdated = DateTime.Now;
                    trArticle.cLastUpdated = HttpContext.User.Identity.Name;
                    trArticle.RowState = System.Data.DataRowState.Modified;
                    trArticleDataAccess.Update(ref trArticle);
                }

                this.deleteListImages(trArticle.Id, trArticleViewModel.listImage == null ? null : trArticleViewModel.listImage.Select(x => x.Id).ToArray());
                this.saveListImages(trArticle.Id, imageFiles);
            }
            catch (Exception e)
            {
                Error(e.Message);
                return View(trArticleViewModel);

            }



            Success("Article Successfully saved");
            return this.RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(long id)
        {
            BootstrapBreadcrumbHelper bcrumb = new BootstrapBreadcrumbHelper();

            bcrumb.AddNode(Url.Action("Index", "Home"), "Home");
            bcrumb.AddNode(Url.Action("Index", "TrNews"), "Berita");
            
            TrArticle trArticle = trArticleDataAccess.GetArticleById(id);

            trArticle.RowState = System.Data.DataRowState.Deleted;

            trArticleDataAccess.Update(ref trArticle);

            Success("Article Successfully deleted");
            return this.RedirectToAction("Index");
        }

        private IList<TrImageAttachmentViewModel> getListImages(long headerId)
        {
            IList<TrImageAttachmentViewModel> listImageViewModels = new List<TrImageAttachmentViewModel>();

            IList<TrImageAttachment> listImageObjs = trImageAttachmentDataAccess.GetListAttachmentByHeaderId(headerId);

            if (listImageObjs != null && listImageObjs.Count > 0)
            {
                foreach (TrImageAttachment img in listImageObjs)
                {

                    TrImageAttachmentViewModel row = new TrImageAttachmentViewModel();
                    row.Id = img.Id;
                    row.HeaderId = headerId;
                    row.Image = MakeThumbnail(img.Image, 150, 150);
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

        private void deleteListImages(long headerId, long[] retainedImageIds)
        {
            IList<TrImageAttachment> listImageObjs = trImageAttachmentDataAccess.GetListAttachmentByHeaderId(headerId);

            if (listImageObjs != null && listImageObjs.Count > 0)
            {
                foreach (TrImageAttachment img in listImageObjs)
                {
                    if (retainedImageIds == null || !retainedImageIds.Contains(img.Id))
                    {
                        TrImageAttachment theImage = img;
                        theImage.RowState = System.Data.DataRowState.Deleted;
                        trImageAttachmentDataAccess.Update(ref theImage);
                    }
                }
            }

        }

        private void saveListImages(long headerId, IEnumerable<HttpPostedFileBase> imageFiles)
        {
            int sequence = 1;
            if (imageFiles != null && imageFiles.Count() > 0)
            {
                foreach (HttpPostedFileBase uploadedImg in imageFiles)
                {
                    byte[] imageBytes = new byte[uploadedImg.ContentLength];
                    uploadedImg.InputStream.Read(imageBytes, 0, uploadedImg.ContentLength);

                    TrImageAttachment theImage = new TrImageAttachment();
                    theImage.HeaderId = headerId;
                    theImage.Image = imageBytes;
                    theImage.Sequence = sequence;
                    theImage.cCreated = HttpContext.User.Identity.Name;
                    theImage.dCreated = DateTime.Now;
                    theImage.cLastUpdated = HttpContext.User.Identity.Name;
                    theImage.dLastUpdated = DateTime.Now;
                    theImage.RowState = System.Data.DataRowState.Added;

                    trImageAttachmentDataAccess.Update(ref theImage);

                    sequence++;
                }
            }

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
