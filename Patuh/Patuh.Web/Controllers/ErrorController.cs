using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Patuh.Domain;
using Patuh.Application;
using System.Text;
using APP.Framework;

namespace Patuh.Web.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index(int status, Exception error)
        {
            ErrorHandler errorHandler = new ErrorHandler();

            Response.StatusCode = status;
            if (status == 404)
            {
                return RedirectToAction("Error404");
            }

            var page = Request.Url.AbsolutePath;
            var exception = error;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Message : ");
            sb.AppendLine(exception.Message);
            sb.AppendLine();
            if (exception.StackTrace != null)
            {
                sb.AppendLine("StackTrace :");
                sb.AppendLine(exception.StackTrace);
                sb.AppendLine();
            }
            if (exception.InnerException != null)
            {
                sb.AppendLine("InnerException :");
                sb.AppendLine(exception.InnerException.ToString());
                sb.AppendLine();
            }

            UserProfile userProfile;

            userProfile = (UserProfile)System.Web.HttpContext.Current.Session["userProfile"];
            ApplAppService applAppService = new ApplAppService(userProfile);

            errorHandler.ErrorMessage = sb.ToString();
            errorHandler.ErrorException = exception;
            errorHandler.LastPageError = page;
            applAppService.SaveErrorLog(errorHandler);

            return View(status);
        }

        public ActionResult Error404()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}
