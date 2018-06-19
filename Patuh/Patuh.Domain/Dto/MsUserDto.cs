using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patuh.Domain.Dto
{
    public class MsUserDto
    {
        public string ModuleID { get; set; }

        public string UserID { get; set; }

        public string UserRoleID { get; set; }

        public string Area { get; set; }

        public string Pwd { get; set; }

        public string UserGuid { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Info1 { get; set; }

        public string Info2 { get; set; }

        public string Info3 { get; set; }

        public string CrtUsrID { get; set; }

        public DateTime TsCrt { get; set; }

        public string ModUsrID { get; set; }

        public DateTime? TsMod { get; set; }

        public string ActiveFlag { get; set; }

        public long TotalRecord { get; set; }

        public long RowNumber { get; set; }

    }
}
