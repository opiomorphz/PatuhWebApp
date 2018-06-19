using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Patuh.Web.HtmlHelpers
{

    public static class UrlMaker
    {
        /// <summary>
        /// Outputs html to user
        /// </summary>
        /// <returns>string</returns>
        public static string OpenTag()
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("<div class=\"btn-group\">");
            stb.Append("<a class=\"btn dropdown-toggle\" data-toggle=\"dropdown\" href=\"#\">");
            stb.Append("Action");
            stb.Append("<span class=\"caret\"></span>");
            stb.Append("</a>");
            stb.Append("<ul class=\"dropdown-menu\">");

            string html = stb.ToString();

            return html;
        }

        public static string CloseTag()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("</ul>");
            stb.Append("</div>");
            string html = stb.ToString();

            return html;
        }

        public static string LiOpenTag()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("<li>");

            string html = stb.ToString();

            return html;
        }
        public static string LiCloseTag()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("</li>");
            string html = stb.ToString();

            return html;
        }

        public static string DividerTag()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("<li class=\"divider\"></li>");
            string html = stb.ToString();

            return html;
        }
    }
}