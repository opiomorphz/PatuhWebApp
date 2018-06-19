using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Patuh.Web.Helpers
{

    public class Breadcrumbs
    {
        public string Url { get; set; }
        public string Title { get; set; }
    }

    public partial class BootstrapBreadcrumbHelper
    {

        //
        // Private Member
        //
        List<Breadcrumbs> breadcrumbs = new List<Breadcrumbs>();

        ///
        /// Options
        ///
        string divider = "";
        string tag_open = "<ol class=\"breadcrumb\">";
        string tag_close = "</ol>";

        /// <summary>
        /// Add node to breadcrumb
        /// </summary>
        /// <param name="Url">Url to use</param>
        /// <param name="Title">Title of link</param>
        public void AddNode(string Url, string Title)
        {
            this.breadcrumbs.Add(new Breadcrumbs
            {
                Url = Url,
                Title = Title
            });
        }

        /// <summary>
        /// Outputs html to user
        /// </summary>
        /// <returns>string</returns>
        public IHtmlString Output()
        {
            string html = this.tag_open;
            int count = 1;

            foreach (Breadcrumbs breadcrumb in this.breadcrumbs)
            {
                //see if this is the last item on the list.
                if (this.breadcrumbs.Count == count)
                {
                    html += "<li class=\"active\">" + breadcrumb.Title + "</li>";
                }
                else
                {
                    html += "<li><a href='" + breadcrumb.Url + "'>" + breadcrumb.Title + "</a> " + this.divider + "</li>";
                    count++; //increment counter.
                }
            }

            html += this.tag_close;
            return new MvcHtmlString(html);
        }
    }
}