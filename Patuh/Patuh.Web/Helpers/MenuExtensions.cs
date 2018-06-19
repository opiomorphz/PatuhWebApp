using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Collections.Specialized;

namespace Patuh.Web.HtmlHelpers
{
    public static class MenuExtensions
    {
        public static MvcHtmlString MenuItem(
        this HtmlHelper htmlHelper,
        string text,
        string action,
        string controller)
        {
            var li = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");

            /*if (string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("active");
            }*/
            if (string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("active");
            }
            li.InnerHtml = htmlHelper.ActionLink(text, action, controller).ToHtmlString();
            return MvcHtmlString.Create(li.ToString());
        }

        public static MvcHtmlString MenuItem2(
        this HtmlHelper htmlHelper,
        string text,
        string action,
        string controller)
        {
            var li = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            var mode = currentController.Split('/');
            var AbsolutePath = HttpContext.Current.Request.Url.AbsolutePath;
            bool active = false;
            //if (currentAction.ToLower()=="index")
            //{
            //    if (AbsolutePath.Contains(currentController) && string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase))
            //    {
            //        active = true;
            //    }
            //}
            //else
            //{
            if (AbsolutePath.Contains(currentController) && AbsolutePath.Contains(currentAction) && string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase))
            {
                active = true;
            }
            // }


            if (active)
            {
                li.AddCssClass("active2");
            }

            //NameValueCollection queryString = htmlHelper.ViewContext.RequestContext.HttpContext.Request.QueryString;

            //bool bRoute = true;
            //if (queryString.Count > 0)
            //{
            //    bRoute = false;
            //}


            //if (string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase) &&
            //    string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase) &&
            //    bRoute)
            //{
            //    li.AddCssClass("active2");
            //}
            if (
                string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("active2");
            }
            li.InnerHtml = htmlHelper.ActionLink(text, action, controller).ToHtmlString();
            return MvcHtmlString.Create(li.ToString());
        }

        public static MvcHtmlString MenuItem2(
        this HtmlHelper htmlHelper,
        string text,
        string action,
        string controller,
        object values)
        {
            var li = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            RouteValueDictionary combinedRouteValues = new RouteValueDictionary(values);
            NameValueCollection queryString = htmlHelper.ViewContext.RequestContext.HttpContext.Request.QueryString;

            bool bRoute = false;

            if (combinedRouteValues["mode"].ToString() == queryString["mode"])
            {
                bRoute = true;
            }



            if (string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase) &&
                bRoute)
            {
                li.AddCssClass("active2");
            }
            li.InnerHtml = htmlHelper.ActionLink(text, action, controller, values, null).ToHtmlString();
            return MvcHtmlString.Create(li.ToString());
        }

        public static MvcHtmlString MenuItem2(
        this HtmlHelper htmlHelper,
        string text,
        string action,
        string controller,
        object values,
        object htmlAttributes)
        {
            var li = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            if (string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("active2");
            }
            li.InnerHtml = htmlHelper.ActionLink(text, action, controller, values, htmlAttributes).ToHtmlString();
            return MvcHtmlString.Create(li.ToString());
        }

        public static MvcHtmlString MenuTree(
        this HtmlHelper htmlHelper,
        string text,
        string action,
        string controller)
        {
            var li = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            li.AddCssClass("section");


            if (string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
            {
                li.InnerHtml = htmlHelper.ActionLink(text, action, controller, new { @class = "selected" }).ToHtmlString();
            }
            else
            {
                li.InnerHtml = htmlHelper.ActionLink(text, action, controller).ToHtmlString();
            }

            return MvcHtmlString.Create(li.ToString());
        }
    }
}