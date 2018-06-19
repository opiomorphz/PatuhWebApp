using APP.Framework.Application;
using APP.Framework.Infrastructure;

using Patuh.Domain;
using Patuh.Infrastructure;
using System;
using System.Diagnostics;
using System.Collections.Generic;
namespace Patuh.Application
{
    [DebuggerStepThrough()]
    public partial class MsUserAppService : AbstractAppService
    {
        #region Collection
        public MsUserAppService(AbstractUserProfile objUser)
            : base(objUser)
        {
        }


        public List<MsUser> GetMsUserList()
        {
            return new MsUserDataAccess(DALInfo).GetMsUserList();
        }


        public List<MsUser> GetMsUserListCustom(string Where, string OrderBy, int Start, int Limit)
        {
            return new MsUserDataAccess(DALInfo).GetMsUserListCustom(Where, OrderBy, Start, Limit);
        }


        public MsUser GetMsUserByMsUserID(string UserID)
        {
            return new MsUserDataAccess(DALInfo).GetMsUserByMsUserID(UserID);
        }

        public TransactionResult Update(ref List<MsUser> objList)
        {
            return new MsUserDataAccess(DALInfo).Update(ref objList);
        }

        public TransactionResult Update(ref MsUser item)
        {
            return new MsUserDataAccess(DALInfo).Update(ref item);
        }

        #endregion
    }
}