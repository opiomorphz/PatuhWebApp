using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using Patuh.Infrastructure;
using APP.Framework.Application;
using Patuh.Domain.Dto;


namespace Patuh.Application
{
    public class MenuAppService : AbstractAppService
    {
        public MenuAppService(AbstractUserProfile objUser)
            : base(objUser)
        {
        }

        public string RenderMenuWebMVC(string userID)
        {
            StringBuilder sb = new StringBuilder();
            List<Getmenu> lstMenu = new List<Getmenu>();
            lstMenu = new MenuDataAccess(DALInfo).RenderMenu(userID);

            if (lstMenu.Count > 0)
            {
                var parentMenu = (from m in lstMenu
                                  where m.ParentID.Equals(0)
                                  select m
                ).AsParallel().ToList();

                foreach (Getmenu mp in parentMenu)
                {
                    string iconClass = string.Empty;
                    switch (mp.MenuText)
                    {
                        case "Master":
                            iconClass = "fa-sitemap";
                            break;
                        case "Transaction":
                            iconClass = "fa-newspaper-o";
                            break;
                        default:
                            iconClass = "fa-home";
                            break;
                    }
                    sb.AppendLine("<li>");
                    sb.AppendLine("<a><i class=\"fa " + iconClass + "\"></i> " + mp.MenuText + " <span class=\"fa fa-chevron-down\"></span></a>");

                    var childMenu = (from m in lstMenu
                                     where m.ParentID.Equals(mp.MenuID)
                                     select m
                    ).AsParallel().ToList();
                    if (childMenu.Count > 0)
                    {
                        sb.AppendLine("<ul class=\"nav child_menu\">");
                        foreach (Getmenu mc in childMenu)
                        {
                            sb.AppendLine(" <li><a href=\"" + mc.PageName + "\">" + mc.MenuText + "</a></li>");
                        }
                        sb.AppendLine("</ul>");
                    }

                    sb.AppendLine("</li>");
                }
            }

            return sb.ToString();
        }

    }
}
