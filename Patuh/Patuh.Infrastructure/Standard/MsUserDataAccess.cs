﻿//Generated by .NET Class Generator Tools

using System;
using System.Data;
using System.Data.SqlClient;
using APP.Framework.Infrastructure;
using MvcContrib.Pagination;
using Patuh.Domain;
using Patuh.Domain.Dto;
using System.Collections.Generic;
using Patuh.Infrastructure.Utils;

namespace Patuh.Infrastructure
{
    public partial class MsUserDataAccess
    {
        private DAL DALInfo;

        public MsUserDataAccess(DAL objDAL)
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



        #region Standard

        public MsUser GetLoginUserProfile(string UserID, string Pwd)
        {
            SqlConnection conn = new SqlConnection(DALInfo.ConnectionString);
            SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM MsUser WHERE UserID = '{0}'", UserID), conn);
            //SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM MsUser", UserID, Pwd), conn);
            
            MsUser objTbl = new MsUser();
            cmd.CommandType = CommandType.Text;
            SqlDataReader da = default(SqlDataReader);
            try
            {
                cmd.Connection.Open();
                da = cmd.ExecuteReader();

                if (da.HasRows)
                {
                    objTbl = MoveDataToCollection(da)[0];

                    string hashedPassword = Security.HashSHA1(Pwd + objTbl.UserGuid);

                    if (objTbl.Pwd == hashedPassword)
                    {
                        return objTbl;
                    }

                    objTbl = new MsUser();

                }
                else
                {
                    return objTbl;
                }              
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                da.Close();
                conn.Close();
                cmd.Dispose();
            }

            return objTbl;
        }


        public MsUser GetMsUserByMsUserID(string UserID)
        {
            SqlConnection conn = new SqlConnection(DALInfo.ConnectionString);
            SqlCommand cmd = new SqlCommand("up_RetrieveMsUser", conn);
            MsUser objTbl = new MsUser();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", UserID); 
            SqlDataReader da = default(SqlDataReader);
            try
            {
                cmd.Connection.Open();
                da = cmd.ExecuteReader();

                if (da.HasRows)
                {
                    objTbl = MoveDataToCollection(da)[0];
                }
                else
                {
                    return objTbl;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                da.Close();
                conn.Close();
                cmd.Dispose();
            }

            return objTbl;
        }


        public List<MsUser> GetMsUserList()
        {
            SqlConnection conn = new SqlConnection(DALInfo.ConnectionString);
            SqlCommand cmd = new SqlCommand("up_RetrieveMsUserList", conn);
            List<MsUser> msUserList = new List<MsUser>();
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader da = default(SqlDataReader);
            try
            {
                cmd.Connection.Open();
                da = cmd.ExecuteReader();

                if (da.HasRows)
                {
                    msUserList = MoveDataToCollection(da);
                }
                else
                {
                    return msUserList;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                da.Close();
                conn.Close();
                cmd.Dispose();
            }

            return msUserList;
        }


        public List<MsUser> GetMsUserListCustom(string Where, string OrderBy, int Start, int Limit)
        {
            SqlConnection conn = new SqlConnection(DALInfo.ConnectionString);
            SqlCommand cmd = new SqlCommand("up_RetrieveMsUserListCustom", conn);
            SqlParameter orderBy = new SqlParameter("@OrderBy", SqlDbType.VarChar);
            SqlParameter where = new SqlParameter("@Where", SqlDbType.VarChar);
            SqlParameter start = new SqlParameter("@Start", SqlDbType.Int);
            SqlParameter limit = new SqlParameter("@Limit", SqlDbType.VarChar);

            Start = (Start - 1) * Limit;

            where.Value = Where;
            orderBy.Value = OrderBy;
            start.Value = Start;
            limit.Value = Limit;

            cmd.Parameters.Add(where);
            cmd.Parameters.Add(orderBy);
            cmd.Parameters.Add(start);
            cmd.Parameters.Add(limit);

            List<MsUser> msUserList = new List<MsUser>();
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader da = default(SqlDataReader);
            cmd.Connection.Open();
            da = cmd.ExecuteReader();

            try
            {
                if (da.HasRows)
                {
                    msUserList = MoveDataToCollection(da, true);
                }
                else
                {
                    return msUserList;
                }
            }
            finally
            {
                da.Close();
                conn.Close();
                cmd.Dispose();
            }

            return msUserList;
        }

        public IPagination<MsUser> GetMsUserListPaginated(string Where, string OrderBy, int Start, int Limit)
        {
            SqlConnection conn = new SqlConnection(DALInfo.ConnectionString);
            SqlCommand cmd = new SqlCommand("up_RetrieveMsUserListCustom", conn);
            SqlParameter orderBy = new SqlParameter("@OrderBy", SqlDbType.VarChar);
            SqlParameter where = new SqlParameter("@Where", SqlDbType.VarChar);
            SqlParameter start = new SqlParameter("@Start", SqlDbType.Int);
            SqlParameter limit = new SqlParameter("@Limit", SqlDbType.VarChar);

            int page = Start;
            Start = (Start - 1) * Limit;

            

            where.Value = Where;
            orderBy.Value = OrderBy;
            start.Value = Start;
            limit.Value = Limit;

            cmd.Parameters.Add(where);
            cmd.Parameters.Add(orderBy);
            cmd.Parameters.Add(start);
            cmd.Parameters.Add(limit);

            List<MsUser> msUserList = new List<MsUser>();
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader da = default(SqlDataReader);
            cmd.Connection.Open();
            da = cmd.ExecuteReader();

            try
            {
                if (da.HasRows)
                {
                    msUserList = MoveDataToCollection(da, true);
                }
                else
                {
                }
            }
            finally
            {
                da.Close();
                conn.Close();
                cmd.Dispose();
            }


            return new CustomPagination<MsUser>(msUserList
                , page, Limit
                , Convert.ToInt32(msUserList.Count > 0 ? msUserList[0].TotalRecord : 0)); 
        }


        private List<MsUser> MoveDataToCollection(SqlDataReader MyReader, Boolean isCustom = false)
        {
            List<MsUser> msUserList = new List<MsUser>();
            while (MyReader.Read())
            {

                var columns = new List<string>();

                for (int i = 0; i < MyReader.FieldCount; i++)
                {
                    columns.Add(MyReader.GetName(i));
                }

                MsUser objMsUser = new MsUser();
                objMsUser.UserID = MyReader["userid"].ToString().Trim();
                objMsUser.UserRoleID = MyReader["userroleid"].ToString().Trim();
                objMsUser.Pwd = MyReader["pwd"].ToString().Trim();
                objMsUser.UserGuid = MyReader["userguid"].ToString();
                objMsUser.FullName = MyReader["fullname"].ToString().Trim();
                objMsUser.Email = MyReader["email"].ToString().Trim();
                objMsUser.Info1 = MyReader["info1"].ToString().Trim();
                objMsUser.Info2 = MyReader["info2"].ToString().Trim();
                objMsUser.Info3 = MyReader["info3"].ToString().Trim();
                objMsUser.CrtUsrID = MyReader["crtusrid"].ToString().Trim();
                objMsUser.TsCrt = Mapper<DateTime>(MyReader["tscrt"]);
                objMsUser.ModUsrID = MyReader["modusrid"].ToString().Trim();
                objMsUser.TsMod = Mapper<DateTime?>(MyReader["tsmod"]);
                objMsUser.ActiveFlag = MyReader["activeflag"].ToString().Trim();
                objMsUser.RowState = DataRowState.Unchanged;

                int fields = MyReader.FieldCount;
                int visi =  MyReader.VisibleFieldCount;

                if (isCustom == true)
                {
                    objMsUser.RowNumber = Convert.ToInt64(MyReader["RowNumber"]);

                    objMsUser.TotalRecord = Convert.ToInt64(MyReader["TotalRecord"]);
                }
                msUserList.Add(objMsUser);
            }
            return msUserList;
        }


        public List<MsUser> MoveDataToCollectionDomain(DataTable dataTable)
        {
            List<MsUser> msUserList = new List<MsUser>();
            for (int i = 0; i <= dataTable.Rows.Count - 1; i++)
            {
                MsUser objMsUser = new MsUser();
                objMsUser.UserID = dataTable.Rows[i]["userid"].ToString();
                objMsUser.UserRoleID = dataTable.Rows[i]["userroleid"].ToString();
                objMsUser.Pwd = dataTable.Rows[i]["pwd"].ToString().Trim();
                objMsUser.UserGuid = dataTable.Rows[i]["userguid"].ToString().Trim();
                objMsUser.FullName = dataTable.Rows[i]["fullname"].ToString();
                objMsUser.Email = dataTable.Rows[i]["email"].ToString();
                objMsUser.Info1 = dataTable.Rows[i]["info1"].ToString();
                objMsUser.Info2 = dataTable.Rows[i]["info2"].ToString();
                objMsUser.Info3 = dataTable.Rows[i]["info3"].ToString();
                objMsUser.CrtUsrID = dataTable.Rows[i]["crtusrid"].ToString();
                objMsUser.TsCrt = Mapper<DateTime>(dataTable.Rows[i]["tscrt"]);
                objMsUser.ModUsrID = dataTable.Rows[i]["modusrid"].ToString();
                objMsUser.TsMod = Mapper<DateTime?>(dataTable.Rows[i]["tsmod"]);
                objMsUser.ActiveFlag = dataTable.Rows[i]["activeflag"].ToString();
                objMsUser.RowState = DataRowState.Unchanged;

                msUserList.Add(objMsUser);
            }
            return msUserList;
        }


        public TransactionResult Update(ref List<MsUser> objList)
        {
            List<SqlCommand> ArraySQLCmd = new List<SqlCommand>();
            TransactionDB TransDB = new TransactionDB(DALInfo);
            TransactionResult ObjTransResult = default(TransactionResult);

            GetBatchQueryUpdate(objList, ref ArraySQLCmd);

            ObjTransResult = TransDB.BatchUpdate(ArraySQLCmd);

            if (ObjTransResult.Result == 1)
            {
                objList = AcceptChanges(ref objList);
            }

            return ObjTransResult;
        }

        public TransactionResult Update(ref MsUser item)
        {
            List<SqlCommand> ArraySQLCmd = new List<SqlCommand>();
            TransactionDB TransDB = new TransactionDB(DALInfo);
            TransactionResult ObjTransResult = default(TransactionResult);

            GetSingleQueryUpdate(item, ref ArraySQLCmd);

            ObjTransResult = TransDB.BatchUpdate(ArraySQLCmd);

            if (ObjTransResult.Result == 1 && !item.RowState.Equals(DataRowState.Deleted))
            {
                item.RowState = DataRowState.Unchanged;
            }

            return ObjTransResult;
        }

        public List<MsUser> AcceptChanges(ref List<MsUser> objList)
        {
            List<MsUser> DataBindCol = new List<MsUser>();

            foreach (MsUser item in objList)
            {
                if (item.RowState != DataRowState.Deleted)
                {
                    item.RowState = DataRowState.Unchanged;
                    DataBindCol.Add(item);
                }
            }
            objList = new List<MsUser>();
            objList = DataBindCol;
            return DataBindCol;
        }

        public void GetBatchQueryUpdate(List<MsUser> objDomain, ref List<SqlCommand> ArraySQLCmd)
        {
            foreach (MsUser item in objDomain)
            {
                MsUser itm = item;
                DALInfo.AssignedInfo(ref itm);
                UpdateQuery(itm, ArraySQLCmd);
            }
        }

        public void GetSingleQueryUpdate(MsUser item, ref List<SqlCommand> ArraySQLCmd)
        {
            MsUser itm = item;
            //DALInfo.AssignedInfo(ref itm);
            UpdateQuery(itm, ArraySQLCmd);
        }

        public void UpdateQuery(MsUser item, List<SqlCommand> ArraySQLCmd)
        {
            SqlCommand cmd = null;
            if (item.RowState == DataRowState.Added)
            {
                //using (SqlConnection sqlCon = new SqlConnection(DALInfo.ConnectionString))
                //{
                    //sqlCon.Open();
                    cmd = new SqlCommand("up_InsertMsUser");
                    //cmd.Connection = sqlCon;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@USERID", SqlDbType.VarChar, 10).Value = item.UserID ?? (object) DBNull.Value;
                    cmd.Parameters.Add("@USERROLEID", SqlDbType.VarChar, 20).Value = item.UserRoleID ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@PWD", SqlDbType.VarChar, 50).Value = item.Pwd ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@USERGUID", SqlDbType.VarChar, 50).Value = item.UserGuid ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@fullname", SqlDbType.VarChar, 100).Value = item.FullName == null ? (object)DBNull.Value : item.FullName;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = item.Email == null ? (object)DBNull.Value : item.Email;
                    cmd.Parameters.Add("@info1", SqlDbType.VarChar, 100).Value = item.Info1 == null ? (object)DBNull.Value : item.Info1;
                    cmd.Parameters.Add("@info2", SqlDbType.VarChar, 100).Value = item.Info2 == null ? (object)DBNull.Value : item.Info2;
                    cmd.Parameters.Add("@info3", SqlDbType.VarChar, 100).Value = item.Info3 == null ? (object)DBNull.Value : item.Info3;
                    cmd.Parameters.Add("@crtusrid", SqlDbType.VarChar, 20).Value = item.CrtUsrID ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@modusrid", SqlDbType.VarChar, 20).Value = item.ModUsrID == null ? (object)DBNull.Value : item.ModUsrID;
                    cmd.Parameters.Add("@activeflag", SqlDbType.Char, 1).Value = item.ActiveFlag ?? (object)DBNull.Value;

                    //cmd.ExecuteNonQuery();
                //}
            }
            else if (item.RowState == DataRowState.Modified)
            {
                cmd = new SqlCommand("up_UpdateMsUser");
                //cmd.Connection = new SqlConnection(DALInfo.ConnectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@moduleid", SqlDbType.VarChar, 5).Value = item.ModuleID;
                cmd.Parameters.Add("@userid", SqlDbType.VarChar, 10).Value = item.UserID;
                cmd.Parameters.Add("@userroleid", SqlDbType.VarChar, 20).Value = item.UserRoleID;
                cmd.Parameters.Add("@pwd", SqlDbType.VarChar, 50).Value = item.Pwd ?? (object)DBNull.Value;
                cmd.Parameters.Add("@userguid", SqlDbType.VarChar, 50).Value = item.UserGuid ?? (object)DBNull.Value;
                cmd.Parameters.Add("@fullname", SqlDbType.VarChar, 100).Value = item.FullName == null ? (object)DBNull.Value : item.FullName;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = item.Email == null ? (object)DBNull.Value : item.Email;
                cmd.Parameters.Add("@info1", SqlDbType.VarChar, 100).Value = item.Info1 == null ? (object)DBNull.Value : item.Info1;
                cmd.Parameters.Add("@info2", SqlDbType.VarChar, 100).Value = item.Info2 == null ? (object)DBNull.Value : item.Info2;
                cmd.Parameters.Add("@info3", SqlDbType.VarChar, 100).Value = item.Info3 == null ? (object)DBNull.Value : item.Info3;
                cmd.Parameters.Add("@modusrid", SqlDbType.VarChar, 20).Value = item.ModUsrID == null ? (object)DBNull.Value : item.ModUsrID;
                cmd.Parameters.Add("@activeflag", SqlDbType.Char, 1).Value = item.ActiveFlag;
                //cmd.ExecuteNonQuery();
            }
            else if (item.RowState == DataRowState.Deleted)
            {
                cmd = new SqlCommand("up_DeleteMsUser");
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@moduleid", SqlDbType.VarChar, 5).Value = item.ModuleID;
                cmd.Parameters.Add("@userid", SqlDbType.VarChar, 10).Value = item.UserID;
                //cmd.Parameters.Add("@userroleid", SqlDbType.VarChar, 20).Value = item.UserRoleID;
                //cmd.Parameters.Add("@modusrid", SqlDbType.VarChar, 20).Value = item.ModUsrID == null ? (object)DBNull.Value : item.ModUsrID;
                //cmd.ExecuteNonQuery();
            }

            if (cmd != null)
            {
                ArraySQLCmd.Add(cmd);
            }
        }


        #endregion
    }
}