using APP.Framework;
using System;
using APP.Framework.Infrastructure;

namespace Patuh.Infrastructure
{

    public class Connection
    {
        private DAL DALInfo;
        private String ApplicationName = "Patuh";
        private String SqlProfilerInfo;

        public Connection(DAL objDAL)
        {
            DALInfo = objDAL;
            SqlProfilerInfo = "Application Name=" + ApplicationName + ";Workstation ID=" + DALInfo.GlobalID + ";";
        }

        private string APPConnectionDev
        {
            get
            {
                return @"Data Source=dbdev\sqlexpress;Initial Catalog=TestingFramework;Persist Security Info=True;User ID=webdev;Password=123456;" + SqlProfilerInfo;
            }
        }

        private string APPConnectionPrd
        {
            get
            {
                //return @"Data Source=JKT-1311\SQLEXPRESS;Initial Catalog=TestingFramework;Persist Security Info=True;Integrated Security=SSPI;;application name='APP.Framework'";
                return @"Data Source=sqldev;Initial Catalog=TestingFramework;Persist Security Info=True;User ID=webdev;Password=123456;" + SqlProfilerInfo; // + DALInfo.IPAddress + ";";
            }
        }

        private string APPConnectionTst
        {
            get
            {
                //return @"Data Source=dbdev\sqlexpress;Initial Catalog=TestingFramework;Persist Security Info=True;User ID=webdev;Password=123456;" + SqlProfilerInfo;
                return @"Data Source=JKT-1370;Initial Catalog=Patuh;Integrated Security=false;User ID=sa;Password=123456;" + SqlProfilerInfo;
            }
        }

        public string ConnectionString(APP.Framework.Enumeration.ApplicationMode ApplicationMode)
        {

            if (ApplicationMode == Enumeration.ApplicationMode.Development)
            {
                return this.APPConnectionDev;
            }
            if (ApplicationMode == Enumeration.ApplicationMode.Testing)
            {
                return this.APPConnectionTst;
            }
            if (ApplicationMode == Enumeration.ApplicationMode.Production)
            {
                return this.APPConnectionPrd;
            }
            return "";

        }

        private string MailConnectionDev
        {
            get
            {
                return @"Data Source=dbdev\sqlexpress;Initial Catalog=Notification;Persist Security Info=True;User ID=webdev;Password=123456;" + SqlProfilerInfo;
            }
        }

        private string MailConnectionPrd
        {
            get
            {
                return "Data Source=ADSDB;Initial Catalog=Notification;Persist Security Info=True;User ID=sa;Password=admin123;" + SqlProfilerInfo;
            }
        }

        public string MailConnectionString(Enumeration.ApplicationMode ApplicationMode)
        {

            if (ApplicationMode == Enumeration.ApplicationMode.Development)
            {
                return this.MailConnectionDev;
            }
            if (ApplicationMode == Enumeration.ApplicationMode.Testing)
            {
                return this.MailConnectionDev;
            }
            if (ApplicationMode == Enumeration.ApplicationMode.Production)
            {
                return this.MailConnectionPrd;
            }
            return "";

        }

        private string MailConnectionTst
        {
            get
            {
                return "Data Source=ADSDB;Initial Catalog=Notification;Persist Security Info=True;User ID=sa;Password=admin123;" + SqlProfilerInfo;
            }
        }


    }
}

