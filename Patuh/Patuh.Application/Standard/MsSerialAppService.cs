using APP.Framework.Application;
using APP.Framework.Infrastructure;

using Patuh.Domain;
using Patuh.Domain.Dto;
using Patuh.Infrastructure;
using System;
using System.Diagnostics;
using System.Collections.Generic;
namespace Patuh.Application
{
    [DebuggerStepThrough()]
    public partial class MsSerialAppService : AbstractAppService
    {
        #region Collection
        public MsSerialAppService(AbstractUserProfile objUser)
            : base(objUser)
        {
        }


        public List<MsSerial> GetMsSerialList()
        {
            return new MsSerialDataAccess(DALInfo).GetMsSerialList();
        }


        public List<MsSerial> GetMsSerialListCustom(string Where, string OrderBy, int Start, int Limit)
        {
            return new MsSerialDataAccess(DALInfo).GetMsSerialListCustom(Where, OrderBy, Start, Limit);
        }


        public MsSerial GetMsSerialByMsSerialID(string SerialID)
        {
            return new MsSerialDataAccess(DALInfo).GetMsSerialByMsSerialID(SerialID);
        }

        public TransactionResult Update(ref List<MsSerial> objList)
        {
            return new MsSerialDataAccess(DALInfo).Update(ref objList);
        }

        public TransactionResult Update(ref MsSerial item)
        {
            return new MsSerialDataAccess(DALInfo).Update(ref item);
        }

        #endregion
    }
}