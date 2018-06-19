using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcContrib.Pagination;
using Patuh.Application;
using Patuh.Domain;

namespace Patuh.Web.Helpers
{
    public class Queries
    {
        //private ApplAppService objFacade;
        //public  Queries(ApplAppService objFacade){
        //    this.objFacade = objFacade;
        //}

        public IPagination<T> dtoPagingData<T>(List<T> collDomainObject, int Start, int Limit)
        {
            int page = Start;

            long totalCount = 0;
            if (collDomainObject != null)
            {
                if (collDomainObject.Count > 0)
                {
                    T local = collDomainObject[0];
                    totalCount = Convert.ToInt64(local.GetType().GetProperty("TotalRecord").GetValue((T)local, null));
                }
            }

            return new CustomPagination<T>(collDomainObject, page, Limit, Convert.ToInt32(totalCount));
        }
    }
}