

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

namespace SOPCompliance.Web.Mvc.Controllers
{
    [HandleError]
    public class MsUserAccessesController : BaseController
    {
        private MsUserDataAccess msUserDataAccess;
        private MsUserRoleDataAccess msUserRoleDataAccess;

        public MsUserAccessesController()
        { 
            DAL dal = new DAL();
            dal.ApplicationMode = APP.Framework.Enumeration.ApplicationMode.Testing;
            msUserDataAccess = new MsUserDataAccess(dal);
            msUserRoleDataAccess = new MsUserRoleDataAccess(dal);
        }

		public ActionResult Index(int? page)
        {
            BootstrapBreadcrumbHelper bcrumb = new BootstrapBreadcrumbHelper();        
            
            bcrumb.AddNode(Url.Action("Index", "Home"), "Home");
            bcrumb.AddNode(Url.Action("Index", "MsUserAccesses"), "User Access");

            ViewBag.Breadcrumb = bcrumb.Output();


            page = page == null ? 1 : page;
            string whereClause = "";
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
                    whereClause = string.Format("UserID like '%{0}%' OR FullName like '%{0}%'", Request["searchWord"]);
                    }

            }


            IPagination<MsUser> listUsers = msUserDataAccess.GetMsUserListPaginated(whereClause, "TsCrt DESC", page.Value, 10);

            //IMsUserAccessesQuery Query = new MsUserAccessesListQuery();
            //IPagination<MsUserAccessDto> listMsUserAccessViewModel = Query.GetPagedQRY(2, page.Value, DataGlobals.ROW_PAGE, whereClause, OrderBy);
            

            return View(listUsers);
        }

        [HttpGet]
        public ActionResult CreateOrUpdate(string id="")
        {
            BootstrapBreadcrumbHelper bcrumb = new BootstrapBreadcrumbHelper();        
            
            bcrumb.AddNode(Url.Action("Index", "Home"), "Home");
            bcrumb.AddNode(Url.Action("Index", "MsUserAccesses"), "User Access");
            bcrumb.AddNode(Url.Action("CreateOrUpdate", "MsUserAccesses"), "Create Or Update");

            ViewBag.Breadcrumb = bcrumb.Output();


            MsUser msUser = msUserDataAccess.GetMsUserByMsUserID(id);
            MsUserAccessViewModel msUserAccessViewModel = new MsUserAccessViewModel();

            if (msUser != null)
            {
                msUserAccessViewModel.FullName = msUser.FullName;
                msUserAccessViewModel.UserID = id;
                //msUserAccessViewModel.Pwd = msUser.Pwd;
            }

            IList<MsUserRole> listUserRoles =  msUserRoleDataAccess.GetMsUserRoleList();
            IList<MsUserRoleViewModel> listUserRoleViewModels = new List<MsUserRoleViewModel>(); 

            if (listUserRoles != null && listUserRoles.Count > 0)
            {
                foreach(MsUserRole role in listUserRoles)
                {
                    MsUserRoleViewModel userRoleViewmodel = new MsUserRoleViewModel();
                    userRoleViewmodel.UserRoleId = role.UserRoleID;
                    userRoleViewmodel.UserRoleDesc = role.UserRoleDesc;
                    userRoleViewmodel.UserRoleIsActive = (msUser ?? new MsUser()).UserRoleID == role.UserRoleID;

                    listUserRoleViewModels.Add(userRoleViewmodel);
                }

            }


            msUserAccessViewModel.UserRoles = listUserRoleViewModels;
            msUserAccessViewModel.isUpdate = !string.IsNullOrEmpty(id);


            return View(msUserAccessViewModel);
        }
      
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateOrUpdate(MsUserAccessViewModel msUserViewModel)
        {
            BootstrapBreadcrumbHelper bcrumb = new BootstrapBreadcrumbHelper();

            bcrumb.AddNode(Url.Action("Index", "Home"), "Home");
            bcrumb.AddNode(Url.Action("Index", "MsUserAccesses"), "User Access");
            bcrumb.AddNode(Url.Action("CreateOrUpdate", "MsUserAccesses"), "Create Or Update");
            if(!msUserViewModel.isUpdate){
                int countDuplicate  = msUserDataAccess.GetMsUserListCustom(string.Format("UserID = '{0}'", msUserViewModel.UserID), "", 1, 100).Count();
                if (countDuplicate > 0)
                {
                    Error("User ID already registered, please choose another User ID");
                    return View(msUserViewModel);
                }
            }
            

            MsUser msUser = msUserDataAccess.GetMsUserByMsUserID(msUserViewModel.UserID);

            if (string.IsNullOrEmpty(msUser.UserID))
            {
                Guid userGuid = System.Guid.NewGuid();
                string hashedPassword = Security.HashSHA1(msUserViewModel.Pwd + userGuid.ToString());

                msUser = new MsUser();
                msUser.Pwd = hashedPassword;
                msUser.UserGuid = userGuid.ToString();
                msUser.RowState = System.Data.DataRowState.Added;
            }
            else
            {
                if (!string.IsNullOrEmpty(msUserViewModel.Pwd))
                {
                    Guid userGuid = System.Guid.NewGuid();
                    string hashedPassword = Security.HashSHA1(msUserViewModel.Pwd + userGuid.ToString());

                    msUser.Pwd = hashedPassword;
                    msUser.UserGuid = userGuid.ToString();
                }
                msUser.RowState = System.Data.DataRowState.Modified;
            }

            if (msUserViewModel.UserRoles != null)
            {
                foreach (MsUserRoleViewModel role in msUserViewModel.UserRoles)
                {
                    if (role.UserRoleIsActive) msUser.UserRoleID = role.UserRoleId;
                }

            }

            
            msUser.UserID = msUserViewModel.UserID;
            msUser.FullName = msUserViewModel.FullName;
            msUser.CrtUsrID = "Admin";
            
            msUserDataAccess.Update(ref msUser);

            Success("User Successfully saved");
            return this.RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(string id="")
        {
            BootstrapBreadcrumbHelper bcrumb = new BootstrapBreadcrumbHelper();

            bcrumb.AddNode(Url.Action("Index", "Home"), "Home");
            bcrumb.AddNode(Url.Action("Index", "MsUserAccesses"), "User Access");
            
            MsUser msUser = msUserDataAccess.GetMsUserByMsUserID(id);

            msUser.RowState = System.Data.DataRowState.Deleted;           

            msUserDataAccess.Update(ref msUser);

            Success("User Successfully deleted");
            return this.RedirectToAction("Index");
        }

       

        
        
    }
}
