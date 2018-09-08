﻿using APP.Framework.Domain;

using System;

using System.Diagnostics;

/*Generated by .NET Class Generator Tools*/

namespace Patuh.Domain
{
    [DebuggerStepThrough()]
    public partial class TrArticle : AbstractTable
    {
        #region Data Member
        private long _id;
        private string _category;
        private string _title;
        private string _story;
        private string _gpslocation;
        private double _gpslong;
        private double _gpslat;
        private string _cstatus;
        private string _ccreated;
        private DateTime _dcreated;
        private string _clastupdated;
        private DateTime _dlastupdated;
        #endregion

        #region Default Value
        public TrArticle()
        {
        }
        #endregion


        #region Properties
        public long Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                Modify();
            }
        }

        public string Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
                Modify();
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                Modify();
            }
        }

        public string Story
        {
            get
            {
                return _story;
            }
            set
            {
                _story = value;
                Modify();
            }
        }


        public string GPSLocation
        {
            get
            {
                return _gpslocation;
            }
            set
            {
                _gpslocation = value;
                Modify();
            }
        }

        public double GPSLong
        {
            get
            {
                return _gpslong;
            }
            set
            {
                _gpslong = value;
                Modify();
            }
        }

        public double GPSLat
        {
            get
            {
                return _gpslat;
            }
            set
            {
                _gpslat = value;
                Modify();
            }
        }

        public string cStatus
        {
            get
            {
                return _cstatus;
            }
            set
            {
                _cstatus = value;
                Modify();
            }
        }

        public string cCreated
        {
            get
            {
                return _ccreated;
            }
            set
            {
                _ccreated = value;
                Modify();
            }
        }

        public DateTime dCreated
        {
            get
            {
                return _dcreated;
            }
            set
            {
                _dcreated = value;
                Modify();
            }
        }

        public string cLastUpdated
        {
            get
            {
                return _clastupdated;
            }
            set
            {
                _clastupdated = value;
                Modify();
            }
        }

        public DateTime dLastUpdated
        {
            get
            {
                return _dlastupdated;
            }
            set
            {
                _dlastupdated = value;
                Modify();
            }
        }

        

        #endregion

    }
}