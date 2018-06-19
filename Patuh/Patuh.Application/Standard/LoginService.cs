using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patuh.Application.SMFLocation;
using Patuh.Infrastructure;
using APP.Framework.Infrastructure;
using Patuh.Domain;

namespace Patuh.Application
{
    public class LoginService
    {
        private DAL dal;

        public LoginService()
        {
            dal = new DAL();
            dal.ApplicationMode = APP.Framework.Enumeration.ApplicationMode.Testing;
            dal.GlobalID = "Patuh";
        }

        public UserProfile CekLogin(string UserId, string Password)
        {
            UserProfile userProfile = new UserProfile();

            MsUserDataAccess userDataAccess = new MsUserDataAccess(dal);

            MsUser msUser =  userDataAccess.GetLoginUserProfile(UserId, Password);

                if (msUser != null && !string.IsNullOrEmpty(msUser.FullName))  //(true == true) SEMENTARA tidak cek PASSWORD untuk testing
                {
                    userProfile.GlobalID = UserId;

                }


            return userProfile;

        }

        
    }
}
