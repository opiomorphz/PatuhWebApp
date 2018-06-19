using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using APP.Framework.Infrastructure;
using Patuh.Domain.Dto;


namespace Patuh.Infrastructure
{
    public class MenuDataAccess
    {
        private DAL DALInfo;
        public MenuDataAccess(DAL objDAL)
        {
            DALInfo = objDAL;
            DALInfo.ConnectionString = new Connection(DALInfo).ConnectionString(DALInfo.ApplicationMode);
        }
        private static T Mapper<T>(object obj)
        {
            T val = default(T);
            if (obj != DBNull.Value)
            {
                val = (T)obj;
            }
            return val;
        }

        private List<Getmenu> MoveDataToCollection(SqlDataReader MyReader, Boolean isCustom = false)
        {
            List<Getmenu> getmenuList = new List<Getmenu>();
            while (MyReader.Read())
            {
                Getmenu objGetmenu = new Getmenu();
                objGetmenu.MenuID = Mapper<int>(MyReader["MenuID"]);
                objGetmenu.ParentID = Mapper<int>(MyReader["ParentID"]);
                objGetmenu.Seq = Mapper<int>(MyReader["Seq"]);
                objGetmenu.MenuText = MyReader["MenuText"].ToString();
                objGetmenu.PageName = MyReader["PageName"].ToString();

                if (isCustom == true)
                {
                    objGetmenu.RowNumber = Convert.ToInt64(MyReader["RowNumber"]);
                    objGetmenu.TotalRecord = Convert.ToInt64(MyReader["TotalRecord"]);
                }
                getmenuList.Add(objGetmenu);
            }
            return getmenuList;
        }


        public List<Getmenu> RenderMenu(string userID)
        {
            StringBuilder sb = new StringBuilder();
            List<Getmenu> lstMenu = new List<Getmenu>();
            using (SqlConnection sqlCon = new SqlConnection(DALInfo.ConnectionString))
            {
                sqlCon.Open();
                using (SqlCommand sqlCmd = sqlCon.CreateCommand())
                {
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "dto_GetMenu";
                    sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = userID;

                    using (SqlDataReader sqlDr = sqlCmd.ExecuteReader())
                    {
                        lstMenu = MoveDataToCollection(sqlDr);
                    }
                }
            }
            return lstMenu;
        }

    }
}
