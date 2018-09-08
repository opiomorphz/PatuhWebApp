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
    public partial class TrImageAttachmentAppService : AbstractAppService
    {
        #region Collection
        public TrImageAttachmentAppService(AbstractUserProfile objUser)
            : base(objUser)
        {
        }



        public List<TrImageAttachment> GetTrImageAttachmentListCustom(string Where, string OrderBy, int Start, int Limit)
        {
            return new TrImageAttachmentDataAccess(DALInfo).GetTrImageAttachmentListCustom(Where, OrderBy, Start, Limit);
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