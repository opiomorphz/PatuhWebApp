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
    public partial class TrImageAttachmentDataAccess
    {
        private DAL DALInfo;

        public TrImageAttachmentDataAccess(DAL objDAL)
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


        public TrImageAttachment GetAttachmentById(long Id)
        {
            SqlConnection conn = new SqlConnection(DALInfo.ConnectionString);
            SqlCommand cmd = new SqlCommand("up_RetrieveTrImageAttachment", conn);
            TrImageAttachment objTbl = new TrImageAttachment();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@HeaderId", 0);
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

        public IList<TrImageAttachment> GetListAttachmentByHeaderId(long HeaderId)
        {
            SqlConnection conn = new SqlConnection(DALInfo.ConnectionString);
            SqlCommand cmd = new SqlCommand("up_RetrieveTrImageAttachment", conn);
            IList<TrImageAttachment> objTbl = new List<TrImageAttachment>();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", 0);
            cmd.Parameters.AddWithValue("@HeaderId", HeaderId);
            SqlDataReader da = default(SqlDataReader);
            try
            {
                cmd.Connection.Open();
                da = cmd.ExecuteReader();

                if (da.HasRows)
                {
                    objTbl = MoveDataToCollection(da);
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

        public List<TrImageAttachment> GetTrImageAttachmentListCustom(string Where, string OrderBy, int Start, int Limit)
        {
            SqlConnection conn = new SqlConnection(DALInfo.ConnectionString);
            SqlCommand cmd = new SqlCommand("up_RetrieveTrImageAttachmentListCustom", conn);
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

            List<TrImageAttachment> msUserList = new List<TrImageAttachment>();
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

        public IPagination<TrImageAttachment> GetTrImageAttachmentListPaginated(string Where, string OrderBy, int Start, int Limit)
        {
            SqlConnection conn = new SqlConnection(DALInfo.ConnectionString);
            SqlCommand cmd = new SqlCommand("up_RetrieveTrImageAttachmentListCustom", conn);
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

            List<TrImageAttachment> msUserList = new List<TrImageAttachment>();
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


            return new CustomPagination<TrImageAttachment>(msUserList
                , page, Limit
                , Convert.ToInt32(msUserList.Count > 0 ? msUserList[0].TotalRecord : 0)); 
        }


        private List<TrImageAttachment> MoveDataToCollection(SqlDataReader MyReader, Boolean isCustom = false)
        {
            List<TrImageAttachment> msArticleList = new List<TrImageAttachment>();
            while (MyReader.Read())
            {

                var columns = new List<string>();

                for (int i = 0; i < MyReader.FieldCount; i++)
                {
                    columns.Add(MyReader.GetName(i));
                }

                TrImageAttachment objTrImageAttachment = new TrImageAttachment();
                objTrImageAttachment.Id = long.Parse(MyReader["Id"].ToString().Trim());
                objTrImageAttachment.HeaderId = long.Parse(MyReader["HeaderId"].ToString().Trim()); ;
                objTrImageAttachment.Image = (byte[]) MyReader["Image"];
                objTrImageAttachment.Sequence = int.Parse(string.IsNullOrEmpty(MyReader["Sequence"].ToString()) ? "0" : MyReader["Sequence"].ToString());
                objTrImageAttachment.cCreated = MyReader["cCreated"].ToString().Trim();
                objTrImageAttachment.dCreated = Mapper<DateTime>(MyReader["dCreated"]);
                objTrImageAttachment.cLastUpdated = MyReader["cLastUpdated"].ToString().Trim();
                objTrImageAttachment.dLastUpdated = Mapper<DateTime>(MyReader["dLastUpdated"]);
                objTrImageAttachment.RowState = DataRowState.Unchanged;

                int fields = MyReader.FieldCount;
                int visi =  MyReader.VisibleFieldCount;

                if (isCustom == true)
                {
                    objTrImageAttachment.RowNumber = Convert.ToInt64(MyReader["RowNumber"]);

                    objTrImageAttachment.TotalRecord = Convert.ToInt64(MyReader["TotalRecord"]);
                }
                msArticleList.Add(objTrImageAttachment);
            }
            return msArticleList;
        }


        public List<TrImageAttachment> MoveDataToCollectionDomain(DataTable dataTable)
        {
            List<TrImageAttachment> TrImageAttachmentList = new List<TrImageAttachment>();
            for (int i = 0; i <= dataTable.Rows.Count - 1; i++)
            {
                TrImageAttachment objTrImageAttachment = new TrImageAttachment();
                objTrImageAttachment.Id = long.Parse(dataTable.Rows[i]["Id"].ToString().Trim());
                objTrImageAttachment.HeaderId = long.Parse(dataTable.Rows[i]["HeaderId"].ToString().Trim());
                objTrImageAttachment.Image = (byte[]) dataTable.Rows[i]["Image"];
                objTrImageAttachment.Sequence = int.Parse(string.IsNullOrEmpty(dataTable.Rows[i]["Story"].ToString()) ? "0" : dataTable.Rows[i]["Story"].ToString());
                
                objTrImageAttachment.cCreated = dataTable.Rows[i]["cCreated"].ToString().Trim();
                objTrImageAttachment.dCreated = Mapper<DateTime>(dataTable.Rows[i]["dCreated"]);
                objTrImageAttachment.cLastUpdated = dataTable.Rows[i]["cLastUpdated"].ToString().Trim();
                objTrImageAttachment.dLastUpdated = Mapper<DateTime>(dataTable.Rows[i]["dLastUpdated"]);
                objTrImageAttachment.RowState = DataRowState.Unchanged;

                TrImageAttachmentList.Add(objTrImageAttachment);
            }
            return TrImageAttachmentList;
        }


        public TransactionResult Update(ref List<TrImageAttachment> objList)
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

        public TransactionResult Update(ref TrImageAttachment item)
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
        

        public List<TrImageAttachment> AcceptChanges(ref List<TrImageAttachment> objList)
        {
            List<TrImageAttachment> DataBindCol = new List<TrImageAttachment>();

            foreach (TrImageAttachment item in objList)
            {
                if (item.RowState != DataRowState.Deleted)
                {
                    item.RowState = DataRowState.Unchanged;
                    DataBindCol.Add(item);
                }
            }
            objList = new List<TrImageAttachment>();
            objList = DataBindCol;
            return DataBindCol;
        }

        public void GetBatchQueryUpdate(List<TrImageAttachment> objDomain, ref List<SqlCommand> ArraySQLCmd)
        {
            foreach (TrImageAttachment item in objDomain)
            {
                TrImageAttachment itm = item;
                DALInfo.AssignedInfo(ref itm);
                UpdateQuery(itm, ArraySQLCmd);
            }
        }

        public void GetSingleQueryUpdate(TrImageAttachment item, ref List<SqlCommand> ArraySQLCmd)
        {
            TrImageAttachment itm = item;
            //DALInfo.AssignedInfo(ref itm);
            UpdateQuery(itm, ArraySQLCmd);
        }

        public void UpdateQuery(TrImageAttachment item, List<SqlCommand> ArraySQLCmd)
        {
            SqlCommand cmd = null;
            if (item.RowState == DataRowState.Added)
            {
                //using (SqlConnection sqlCon = new SqlConnection(DALInfo.ConnectionString))
                //{
                    //sqlCon.Open();
                    cmd = new SqlCommand("up_InsertTrImageAttachment");
                    //cmd.Connection = sqlCon;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@HeaderId", SqlDbType.BigInt).Value = item.HeaderId;
                    cmd.Parameters.Add("@Image", SqlDbType.VarBinary).Value = item.Image ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@Sequence", SqlDbType.Int).Value = item.Sequence ;
                    cmd.Parameters.Add("@cCreated", SqlDbType.VarChar, 20).Value = item.cCreated ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@cLastUpdated", SqlDbType.VarChar, 20).Value = item.cLastUpdated == null ? (object)DBNull.Value : item.cLastUpdated;

                    //cmd.ExecuteNonQuery();
                //}
            }
            else if (item.RowState == DataRowState.Modified)
            {
                cmd = new SqlCommand("up_UpdateTrImageAttachment");
                //cmd.Connection = new SqlConnection(DALInfo.ConnectionString);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@moduleid", SqlDbType.VarChar, 5).Value = item.ModuleID;
                cmd.Parameters.Add("@Id", SqlDbType.BigInt).Value = item.Id;
                cmd.Parameters.Add("@HeaderId", SqlDbType.BigInt).Value = item.HeaderId;
                cmd.Parameters.Add("@Image", SqlDbType.VarBinary).Value = item.Image ?? (object)DBNull.Value;
                cmd.Parameters.Add("@Sequence", SqlDbType.Int).Value = item.Sequence;
                cmd.Parameters.Add("@cLastUpdated", SqlDbType.VarChar, 20).Value = item.cLastUpdated == null ? (object)DBNull.Value : item.cLastUpdated;
                //cmd.ExecuteNonQuery();
            }
            else if (item.RowState == DataRowState.Deleted)
            {
                cmd = new SqlCommand("up_DeleteTrImageAttachment");
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@moduleid", SqlDbType.VarChar, 5).Value = item.ModuleID;
                cmd.Parameters.Add("@Id", SqlDbType.BigInt).Value = item.Id;
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