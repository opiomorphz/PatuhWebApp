﻿using APP.Framework.Domain;

using System;

using System.Diagnostics;

/*Generated by .NET Class Generator Tools*/

namespace Patuh.Domain
{
    [DebuggerStepThrough()]
    public partial class MsUser : AbstractTable
    {
        #region Data Member
        private string _moduleid;
        private string _userid;
        private string _userroleid;
        private string _area;
        private string _pwd;
        private string _userguid;
        private string _fullname;
        private string _email;
        private string _info1;
        private string _info2;
        private string _info3;
        private string _crtusrid;
        private DateTime _tscrt;
        private string _modusrid;
        private DateTime? _tsmod;
        private string _activeflag = "Y";
        #endregion

        #region Default Value
        public MsUser()
        {
        }
        #endregion


        #region Properties
        public string ModuleID
        {
            get
            {
                return _moduleid;
            }
            set
            {
                _moduleid = value;
                Modify();
            }
        }

        public string UserID
        {
            get
            {
                return _userid;
            }
            set
            {
                _userid = value;
                Modify();
            }
        }

        public string UserRoleID
        {
            get
            {
                return _userroleid;
            }
            set
            {
                _userroleid = value;
                Modify();
            }
        }

        public string Area
        {
            get
            {
                return _area;
            }
            set
            {
                _area = value;
                Modify();
            }
        }


        public string Pwd
        {
            get
            {
                return _pwd;
            }
            set
            {
                _pwd = value;
                Modify();
            }
        }

        public string UserGuid
        {
            get
            {
                return _userguid;
            }
            set
            {
                _userguid = value;
                Modify();
            }
        }

        public string FullName
        {
            get
            {
                return _fullname;
            }
            set
            {
                _fullname = value;
                Modify();
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                Modify();
            }
        }

        public string Info1
        {
            get
            {
                return _info1;
            }
            set
            {
                _info1 = value;
                Modify();
            }
        }

        public string Info2
        {
            get
            {
                return _info2;
            }
            set
            {
                _info2 = value;
                Modify();
            }
        }

        public string Info3
        {
            get
            {
                return _info3;
            }
            set
            {
                _info3 = value;
                Modify();
            }
        }

        public string CrtUsrID
        {
            get
            {
                return _crtusrid;
            }
            set
            {
                _crtusrid = value;
                Modify();
            }
        }

        public DateTime TsCrt
        {
            get
            {
                return _tscrt;
            }
            set
            {
                _tscrt = value;
                Modify();
            }
        }

        public string ModUsrID
        {
            get
            {
                return _modusrid;
            }
            set
            {
                _modusrid = value;
                Modify();
            }
        }

        public DateTime? TsMod
        {
            get
            {
                return _tsmod;
            }
            set
            {
                _tsmod = value;
                Modify();
            }
        }

        public string ActiveFlag
        {
            get
            {
                return _activeflag;
            }
            set
            {
                _activeflag = value;
                Modify();
            }
        }

        #endregion

    }
}