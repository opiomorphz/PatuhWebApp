using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Patuh.Web.Controllers;
using System.Text;
using Patuh.Domain;
using Patuh.Application;

namespace Patuh.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    var exception = Server.GetLastError().GetBaseException();
        //    Server.ClearError();
        //    var routeData = new RouteData();
        //    routeData.Values.Add("controller", "Error");
        //    routeData.Values.Add("action", "Index");



        //    if (exception.GetType() == typeof(HttpException))
        //    {

        //        var page = HttpContext.Current.Request.Url.AbsolutePath;


        //        var httpException = (HttpException)exception;
        //        StringBuilder sb = new StringBuilder();
        //        sb.AppendLine("Message : ");
        //        sb.AppendLine(exception.Message);
        //        sb.AppendLine();
        //        if (exception.StackTrace != null)
        //        {
        //            sb.AppendLine("StackTrace :");
        //            sb.AppendLine(exception.StackTrace);
        //            sb.AppendLine();
        //        }
        //        if (exception.InnerException != null)
        //        {
        //            sb.AppendLine("InnerException :");
        //            sb.AppendLine(exception.InnerException.ToString());
        //            sb.AppendLine();
        //        }


        //        var code = httpException.GetHttpCode();
        //        routeData.Values.Add("status", code);

        //    }
        //    else
        //    {
        //        routeData.Values.Add("status", 500);
        //    }

        //    routeData.Values.Add("error", exception);
        //    IController errorController = new ErrorController();
        //    errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        //}

    }

}