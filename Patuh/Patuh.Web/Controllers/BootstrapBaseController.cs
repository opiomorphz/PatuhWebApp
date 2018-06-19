using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapSupport;
using SOPCompliance.Web.Mvc.Controllers.ViewModels;
using SOPCompliance.Web.Mvc.Controllers.Queries;
using SOPCompliance.Contracts.Query;
using System.Data;
using SOPCompliance.Infrastructure;
using System.Data.OleDb;
using SharpArch.NHibernate;
using SOPCompliance.Domain;


namespace SOPCompliance.Web.Mvc.Controllers
{
    public class BootstrapBaseController : Controller
    {

        public void Attention(string message)
        {
            //  TempData.Remove(Alerts.ATTENTION);

            if (!TempData.ContainsKey(Alerts.ATTENTION))
            {
                TempData.Add(Alerts.ATTENTION, message);
            }
            else
            {
                TempData[Alerts.ATTENTION] = message;
            }
        }

        public void Success(string message)
        {
            if (!TempData.ContainsKey(Alerts.SUCCESS))
            {
                TempData.Add(Alerts.SUCCESS, message);
            }
            else
            {
                TempData[Alerts.SUCCESS] = message;
            }

            //TempData.Remove(Alerts.SUCCESS);
            //TempData.Add(Alerts.SUCCESS, message);
        }

        public void Information(string message)
        {
            //TempData.Remove(Alerts.INFORMATION);
            if (!TempData.ContainsKey(Alerts.INFORMATION))
            {
                TempData.Add(Alerts.INFORMATION, message);
            }
            else
            {
                TempData[Alerts.INFORMATION] = message;
            }

            //TempData.Add(Alerts.INFORMATION, message);
        }

        public void Error(string message)
        {
            //TempData.Remove(Alerts.ERROR);
            if (!TempData.ContainsKey(Alerts.ERROR))
            {
                TempData.Add(Alerts.ERROR, message);
            }
            else
            {
                TempData[Alerts.ERROR] = message;
            }
            //TempData.Add(Alerts.ERROR, message);
        }

        public void CheckRoleMenu()
        {
            if (Request.IsAuthenticated)
            {



               
                if (TempData["RoleMenu"]==null)
                {
                
                    System.Web.Security.FormsIdentity identity = (System.Web.Security.FormsIdentity)HttpContext.User.Identity;
                    string[] roles =identity.Ticket.UserData.Split('|');

                    //Role
                       IMsUserAccessesQuery Query = new MsUserAccessesListQuery();
                    IList<MsGroupAccessViewModel> lst = Query.GetRightsRole(roles[4], roles[1]);
                    TempData["RoleMenu"] = lst;

                    
                }
                TempData.Keep("RoleMenu");
               
                

            }
           

            //

            TempData.Keep("RoleMenu");
            IList<MsGroupAccessViewModel> lstGA = (IList<MsGroupAccessViewModel>)TempData["RoleMenu"];
            var path = Request.Path.ToString();

            if (lstGA != null)
            {
                var Found = from x in lstGA
                            where x.RoleName == MenuRoles.ADD && x.MenuItem == path
                            select x;
                TempData[MenuRoles.ADD] = "disabled";
                if (Found.Count() > 0)
                {
                    // ViewData.Add(key, "dis");
                    TempData[MenuRoles.ADD] = "";


                }
                

                TempData.Keep(MenuRoles.ADD);
            }

            foreach (string key in MenuRoles.ALL.Except(new[] { MenuRoles.ADD }))
            {
                if (lstGA != null)
                {
                    var Found = from x in lstGA
                                where x.RoleName == key && x.MenuItem == path
                                select x;
                    TempData[key] = "sr-only";
                    if (Found.Count() > 0)
                    {
                        // ViewData.Add(key, "dis");
                        TempData[key] = "dis";

                    }
                    
                    TempData.Keep(key);
                }
            }
        }
        

        public bool CheckRoleMenu(string Menu) {
            TempData.Keep("RoleMenu");
            IList<MsGroupAccessViewModel> lstGA = (IList<MsGroupAccessViewModel>)TempData["RoleMenu"];
            var path = Request.Path.ToString();
            bool bFlag = false;
            if (lstGA !=null)
            {
                var Found = from x in lstGA
                                where x.RoleName == Menu && x.MenuItem == path
                                select x;
                if (Found.Count() > 0)
                {
                    bFlag = true;
                }
            }

            return bFlag;
        }


        public string GetUserId()
        {
            if (Request.IsAuthenticated)
            {
                System.Web.Security.FormsIdentity identity = (System.Web.Security.FormsIdentity)HttpContext.User.Identity;
                string[] roles = identity.Ticket.UserData.Split('|');
                if (roles.Length > 0)
                {
                    return roles[0];
                }

                return "";
            }
            else
                return "";
        }

        public string GetUserGroup()
        {
            if (Request.IsAuthenticated)
            {
                System.Web.Security.FormsIdentity identity = (System.Web.Security.FormsIdentity)HttpContext.User.Identity;
                string[] roles = identity.Ticket.UserData.Split('|');
                if (roles.Length > 3)
                {
                    return roles[3];
                }

                return "";
            }
            else
                return "";
        }

        public int GetUserPernr()
        {
            if (Request.IsAuthenticated)
            {
                System.Web.Security.FormsIdentity identity = (System.Web.Security.FormsIdentity)HttpContext.User.Identity;
                string[] roles = identity.Ticket.UserData.Split('|');
                if (roles.Length > 5)
                {
                    return int.Parse(roles[5]);
                }

                return 0;
            }
            else
                return 0;
        }

        public string[] GetUserLocations()
        {
            if (Request.IsAuthenticated)
            {

                System.Web.Security.FormsIdentity identity = (System.Web.Security.FormsIdentity)HttpContext.User.Identity;
                string[] roles = identity.Ticket.UserData.Split('|');
                if (roles.Length > 1)
                {
                    return roles[1].Split(',');
                }

                return new string[0];
            }
            else
                return new string[0];
        }


        public string[] GetUserRegions()
        {
            if (Request.IsAuthenticated)
            {
                System.Web.Security.FormsIdentity identity = (System.Web.Security.FormsIdentity)HttpContext.User.Identity;
                string[] roles = identity.Ticket.UserData.Split('|');
                if (roles.Length > 7)
                {
                    return roles[7].Split(',');
                }

                return new string[0];
            }
            else
                return new string[0];
        }

        public string[] GetUserDistricts()
        {
            if (Request.IsAuthenticated)
            {
                System.Web.Security.FormsIdentity identity = (System.Web.Security.FormsIdentity)HttpContext.User.Identity;
                string[] roles = identity.Ticket.UserData.Split('|');
                if (roles.Length > 9)
                {
                    return roles[9].Split(',');
                }

                return new string[0];
            }
            else
                return new string[0];
        }




        public IEnumerable<SelectListItem> RegionAuth(string selValue)
        {
            System.Web.Security.FormsIdentity identity = (System.Web.Security.FormsIdentity)HttpContext.User.Identity;
            string[] roles = identity.Ticket.UserData.Split('|');

            selValue = string.IsNullOrEmpty(selValue) ? string.Empty : selValue;
            if (roles.Length > 4)
            {
                IMsDistrictAuthorizationQuery msDistrictAuthorizationQuery = new MsDistrictAuthorizationListQuery();
                IList<MsDistrictAuthorizationDto> listDistrictAuth = msDistrictAuthorizationQuery
                    .GetMsDistrictAuthorizationQRY(2, 0, 0, "cStatus = 1 AND nPernr = " + roles[5], "cRegionName");

                IEnumerable<SelectListItem> query = (from da in listDistrictAuth
                                                     group da by new { da.cRegionCode, da.cRegionName } into g
                                                     select new SelectListItem
                                                     {
                                                         Value = g.FirstOrDefault().cRegionCode,
                                                         Text = g.FirstOrDefault().cRegionName,
                                                         Selected = selValue.Equals(g.FirstOrDefault().cRegionCode)
                                                     });

                return query;
            }

            return null;
        }


        public IEnumerable<MsDistrictAuthorizationDto> RegionAuth(int pernr)
        {
                IMsDistrictAuthorizationQuery msDistrictAuthorizationQuery = new MsDistrictAuthorizationListQuery();
                IList<MsDistrictAuthorizationDto> listDistrictAuth = msDistrictAuthorizationQuery
                    .GetMsDistrictAuthorizationQRY(2, 0, 0, "cStatus = 1 AND nPernr = " + pernr, "cRegionName");

                IEnumerable<MsDistrictAuthorizationDto> query = (from da in listDistrictAuth
                                                     group da by new { da.cRegionCode, da.cRegionName } into g
                                                     select new MsDistrictAuthorizationDto
                                                     {
                                                         cRegionCode = g.FirstOrDefault().cRegionCode,
                                                         cRegionName = g.FirstOrDefault().cRegionName,
                                                     });

                return query;

        }


        public IEnumerable<SelectListItem> ProvinceAuth(string selValue)
        {
            System.Web.Security.FormsIdentity identity = (System.Web.Security.FormsIdentity)HttpContext.User.Identity;
            string[] roles = identity.Ticket.UserData.Split('|');

            selValue = string.IsNullOrEmpty(selValue) ? string.Empty : selValue;
            if (roles.Length > 4)
            {
                IMsDistrictAuthorizationQuery msDistrictAuthorizationQuery = new MsDistrictAuthorizationListQuery();
                IList<MsDistrictAuthorizationDto> listDistrictAuth = msDistrictAuthorizationQuery
                    .GetMsDistrictAuthorizationQRY(2, 0, 0, "cStatus = 1 AND nPernr = " + roles[5], "cProvince");

                IEnumerable<SelectListItem> query = (from da in listDistrictAuth
                                                     group da by new { da.cProvince} into g
                                                     select new SelectListItem
                                                     {
                                                         Value = g.FirstOrDefault().cProvince,
                                                         Text = g.FirstOrDefault().cProvince,
                                                         Selected = selValue.Equals(g.FirstOrDefault().cProvince)
                                                     });

                return query;
            }

            return null;
        }

        public void SetRegionSelected(string setVal)
        {
            if (!string.IsNullOrEmpty(setVal))
            {
                Session["ddlRegion"] = setVal;
            }
        }

        public String GetRegionSelected()
        {
            string ddlRegion = Request["ddlRegion"];
            if (!string.IsNullOrEmpty(ddlRegion))
            {
                Session["ddlRegion"] = ddlRegion;
            }
            else
            {
                // Set default region
                string regionSelected = string.Empty;
                IMsDistrictAuthorizationQuery msDistrictAuthorizationQuery = new MsDistrictAuthorizationListQuery();
                IList<MsDistrictAuthorizationDto> listDistrictAuth = msDistrictAuthorizationQuery
                    .GetMsDistrictAuthorizationQRY(2, 0, 0
                        , "cStatus = 1 AND nPernr = " + HttpContext.User.Identity.Name, string.Empty);

                if (listDistrictAuth.Count > 0)
                {
                    regionSelected = listDistrictAuth[0].cRegionCode;
                }

                ddlRegion = Session["ddlRegion"] == null ? regionSelected :
                    Session["ddlRegion"].ToString();
            }
            return ddlRegion;
        }

        public IEnumerable<SelectListItem> RegionAuth()
        {
            return this.RegionAuth(GetRegionSelected());
        }

        public IEnumerable<SelectListItem> ProvinceAuth()
        {
            return this.ProvinceAuth(GetRegionSelected());
        }

        public ActionResult DistrictAuthJson(string cRegion)
        {
            return this.Json(this.DistrictAuth(cRegion, string.Empty));
        }

        public IEnumerable<SelectListItem> DistrictAuth(string cRegion, string selectedValue)
        {
            System.Web.Security.FormsIdentity identity = (System.Web.Security.FormsIdentity)HttpContext.User.Identity;
            string[] roles = identity.Ticket.UserData.Split('|');

            if (roles.Length > 4)
            {
                IMsDistrictAuthorizationQuery msDistrictAuthorizationQuery = new MsDistrictAuthorizationListQuery();
                IList<MsDistrictAuthorizationDto> listDistrictAuth = msDistrictAuthorizationQuery
                    .GetMsDistrictAuthorizationQRY(2, 0, 0
                    , "cStatus = 1 AND nPernr = " + roles[5] + " AND cRegionCode = '" + cRegion + "'"
                    , "cDistrictName");

                IEnumerable<SelectListItem> query = (from da in listDistrictAuth
                                                     select new SelectListItem
                                                     {
                                                         Value = da.cDistrictCode,
                                                         Text = da.cDistrictName,
                                                         Selected = da.cDistrictCode.Equals(selectedValue) ? true : false
                                                     });

                return query;
            }

            return null;
        }

        public DataTable ReadTableExcel(string strFileName)
        {
            string ConnectionString = "";
            System.Data.OleDb.OleDbDataAdapter adapter;
            System.Data.DataSet ds = new System.Data.DataSet();

            try
            {
                if (strFileName.Trim().EndsWith(".xlsx"))
                {
                    ConnectionString =
                        string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", strFileName);
                }
                else if (strFileName.Trim().EndsWith(".xls"))
                {
                    try
                    {
                        ConnectionString =
                        string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";", strFileName);
                    }
                    catch (Exception)
                    {
                        ConnectionString =
                        string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", strFileName);
                    }
                }

                System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(ConnectionString);
                conn.Open();
                DataTable sheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                foreach (DataRow dr in sheets.Rows)
                {
                    string sht = dr[2].ToString().Replace("'", "");
                    adapter = new OleDbDataAdapter("select * from [" + sht + "]", conn);
                    adapter.Fill(ds);

                    if (conn != null)
                    {
                        adapter.Dispose();
                        conn.Close();
                    }
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return ds.Tables[0];
        }

    }
}