using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patuh.Application;
using System.Web.Security;
using System.Security.Principal;
using Patuh.Domain;

namespace Patuh.Web.Controllers
{


    //Public Module MasterSession

    //     Public Property Profile() As UserProfile
    //          Get
    //               If Not IsNothing(HttpContext.Current.Session("UserProfile")) Then
    //                    Return CType(HttpContext.Current.Session("UserProfile"), UserProfile)
    //               Else
    //                    Dim usr As UserProfile = New UserProfile()
    //                    'Debugging purpose only
    //                    'usr.GlobalID = "Debug"
    //                    'usr.ApplicationMode = Enumeration.ApplicationMode.Development
    //                    'usr.DebuggerID = "Debug"
    //                    'usr.IPAddress = "127.0.0.1"
    //                    Return usr
    //               End If
    //          End Get
    //          Set(ByVal value As UserProfile)
    //               HttpContext.Current.Session("UserProfile") = value
    //          End Set
    //     End Property
    public class LoginController : BaseController
    {

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CekLogin(string userid, string Password)
        {

            if (ModelState.IsValid)
            {
                LoginService loginService = new LoginService();
                UserProfile userProfile = loginService.CekLogin(userid, Password);

               
                if (userProfile != null && !string.IsNullOrEmpty(userProfile.GlobalID))
                {

                    WindowsIdentity identity = Request.LogonUserIdentity;
                    userProfile.GlobalID = userid;
                    userProfile.WindowsLogin = identity.Name;
                    userProfile.IPAddress = GetIPAddress();
                    userProfile.WebBrowser = GetBrowser();
                    userProfile.ApplicationMode = APP.Framework.Enumeration.ApplicationMode.Testing;
                    //userProfile.DebuggerID = "simulateUser";
                    userProfile.DeviceID = "deviceID";

                    applAppService = new ApplAppService(userProfile);

                    MsUserAppService msUserAppService = new MsUserAppService(userProfile);
                    List<MsUser> msUserLst = msUserAppService.GetMsUserList().Where(x => x.UserID.Equals(userProfile.GlobalID)).ToList();
                    string userRole = "";
                    foreach (var item in msUserLst)
                    {

                        if (userRole != "")
                        {
                            userRole += "+";
                        }
                        userRole += item.UserRoleID;
                    }
                    //to be retrieved from DB
                    if (userRole == "")
                    {
                        Error("you do not have permission to access this application");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(2,
                       userProfile.GlobalID, DateTime.Now, DateTime.Now.Add(FormsAuthentication.Timeout), false, userRole);

                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);


                        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        FormsAuthentication.SetAuthCookie(userProfile.GlobalID, false);


                        authCookie.Expires = authTicket.Expiration;
                        Response.Cookies.Add(authCookie);

                        userActivity.Action = "Login";
                        userActivity.FormName = "Login.aspx";
                        userActivity.Description = "Login Success";
                        applAppService.LogUserActivity(userActivity);
                    }

                    Session["UserProfile"] = userProfile;


                }
                else
                {
                    Error("Login Failed, Please Check User Name and Password");
                    return RedirectToAction("Index");
                }



            }
            else
            {
                Error("Login Failed, Please Check User Name and Password");
                return RedirectToAction("Index");
            }


            Success("Login Success");

            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {

            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.RedirectToLoginPage();
            return View();

        }
        public ActionResult Index()
        {

            //ViewData["lstdomain"] = listDomain();
            return View();
        }

        //
        // GET: /Login/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Login/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Login/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Login/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Login/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Login/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Login/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
    }
}
