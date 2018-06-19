using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace Patuh.Web.HtmlHelpers
{
    public static class MenuNavigation
    {
        public static MvcHtmlString NavBar(this HtmlHelper helper, string Menu)
        {

            return new MvcHtmlString(Menu);
        }


    }
}