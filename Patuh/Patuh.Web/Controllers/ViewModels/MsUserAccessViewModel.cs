using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Patuh.Web.Controllers.ViewModels
{
    public class MsUserAccessViewModel
    {
        public String UserID { get; set; }
        public String FullName { get; set; }
        public String Pwd { get; set; }
        public IList<MsUserRoleViewModel> UserRoles { get; set; }
        public bool isUpdate { get; set; }
    }



    public class MsUserRoleViewModel
    {
        public String UserRoleId { get; set; }
        public String UserRoleDesc { get; set; }
        public bool UserRoleIsActive { get; set; }

    }
}