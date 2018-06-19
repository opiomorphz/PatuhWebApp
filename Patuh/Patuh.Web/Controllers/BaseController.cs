using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using Patuh.Domain;
using Patuh.Application;
using FlashMessageExtensions;
using APP.Framework;

namespace Patuh.Web.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        public ApplAppService applAppService;
        public UserActivity userActivity;

        public UserProfile userProfile
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["UserProfile"] != null)
                {
                    return (UserProfile)System.Web.HttpContext.Current.Session["UserProfile"];
                }
                else
                {
                    return new UserProfile();
                }
            }
            set
            {
                System.Web.HttpContext.Current.Session["UserProfile"] = value;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (userProfile.GlobalID != null)
            {

                applAppService = new ApplAppService(userProfile);
                userActivity.FormName = filterContext.Controller.ToString();
                userActivity.Action = filterContext.ActionDescriptor.ActionName;
                userActivity.DocumentNo = Parameter(filterContext);
            }


            // applAppService.LogUserActivity(userActivity);
        }

        private string Parameter(ActionExecutingContext filterContext)
        {
            string param = "";
            foreach (var item in filterContext.ActionParameters)
            {
                if (param != "")
                {
                    param = param + ", ";
                }
                param = param + item;
            }
            return param;
        }
        //public void UserProfiles()
        //{
        //     WindowsIdentity identity = Request.LogonUserIdentity;
        //     UserProfile.WindowsLogin = identity.Name;
        //     UserProfile.IPAddress = GetIPAddress();
        //     UserProfile.WebBrowser = GetBrowser();
        //     UserProfile.GlobalID = "DebugUser";
        //     UserProfile.ApplicationMode = APP.Framework.Enumeration.ApplicationMode.Production;
        //     UserProfile.DebuggerID = "DEV";
        //     UserProfile.DeviceID = "DEBUGGER";
        //}

        public string GetBrowser()
        {
            return Request.Browser.Browser + " " + Request.Browser.Version;
        }
        public string GetIPAddress()
        {
            return Request.ServerVariables["REMOTE_ADDR"].ToString();
        }

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

    }
}
