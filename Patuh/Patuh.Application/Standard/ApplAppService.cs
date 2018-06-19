using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using APP.Framework.Infrastructure;
using APP.Framework.Application;
using APP.Framework;

using Patuh.Infrastructure;
using Patuh.Domain;
using System.Data.SqlClient;

using System.Configuration;

namespace Patuh.Application
{
    public partial class ApplAppService : AbstractAppService
    {

        private string AppType = Enumeration.ApplicationType.ASPNET_MVC.ToString();
        private const string AppVersion = "v1.0.20120501";
        private const string Mail_Appl_GUID = "{19d856f3-2b28-4f24-b370-813154da6a8e}";
        private int Mail_Send_Ready = 1;
        private UserProfile objUser;
        private string MailConnectionString;

        public ApplAppService(UserProfile objUser)
            : base(objUser)
        {
            this.objUser = objUser;
            this.MailConnectionString = new Connection(DALInfo).MailConnectionString(DALInfo.ApplicationMode);
        }

        public bool LogUserActivity(UserActivity UserActivity)
        {
            DALInfo.ConnectionString = new Connection(DALInfo).ConnectionString(DALInfo.ApplicationMode);
            UserActivityLog oUserActivityLog = new UserActivityLog(DALInfo);
            return oUserActivityLog.LogActivity(UserActivity, objUser, AppType, AppVersion).Result == 0 ? false : true;
        }

        public Int16 SendEmail(Email emailStruc)
        {

            EmailNotification objEmail = default(EmailNotification);
            //Dim strBody(10) As String

            //strBody(0) = "Test"
            //emailStruc.appGUID = Mail_Appl_GUID
            //emailStruc.mailFrom = "Jeffry_untoro@app.co.id"
            //emailStruc.mailTo = "Jeffry_untoro@app.co.id"
            //emailStruc.mailSubject = "Test Email"
            //emailStruc.mailBody = strBody

            objEmail = new EmailNotification(DALInfo, MailConnectionString);
            objEmail.SubmitEmail(ref emailStruc);
            return 0;

        }

        public Boolean SaveErrorLog(ErrorHandler errorHandler)
        {
            if (errorHandler != null)
            {
                DALInfo.ConnectionString = new Connection(DALInfo).ConnectionString(DALInfo.ApplicationMode);
                LogExceptionAPP.WriteLog(errorHandler, DALInfo);
            }
            return true;
        }

        public int GetNewNumber(String SerialID)
        {
            MsSerialAppService msSerialAppService = new MsSerialAppService(objUser);
            MsSerial msSerial;
            int CurrentNum;
            //TransactionResult isSuccess;

            msSerial = msSerialAppService.GetMsSerialByMsSerialID(SerialID);
            CurrentNum = msSerial.CurrentNum;
            //msSerial.CurrentNum += 1;
            //isSuccess = msSerialAppService.Update(ref msSerial);
            return CurrentNum;
            //if (isSuccess.Result == 1)
            //{
            //    return CurrentNum;
            //}
            //else
            //{ 
            //    return 0;
            //}
        }


    }


}
