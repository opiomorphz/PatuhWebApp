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
    public partial class TrArticleAppService : AbstractAppService
    {
        #region Collection
        public TrArticleAppService(AbstractUserProfile objUser)
            : base(objUser)
        {
        }



        public List<TrArticle> GetTrArticleListCustom(string Where, string OrderBy, int Start, int Limit)
        {
            return new TrArticleDataAccess(DALInfo).GetTrArticleListCustom(Where, OrderBy, Start, Limit);
        }


        public MsUserRole GetMsUserRoleByMsUserRoleID(string UserRoleID)
        {
            return new MsUserRoleDataAccess(DALInfo).GetMsUserRoleByMsUserRoleID(UserRoleID);
        }

        public TransactionResult Update(ref List<MsUserRole> objList)
        {
            return new MsUserRoleDataAccess(DALInfo).Update(ref objList);
        }

        public TransactionResult Update(ref MsUserRole item)
        {
            return new MsUserRoleDataAccess(DALInfo).Update(ref item);
        }

        #endregion
    }
}