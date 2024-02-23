using BOXCricket.Areas.MST_Ground.Models;
using BOXCricket.BAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BOXCricket.DAL
{
    public class MST_GroundDALBase : DAL_Helper
    {
        #region Method: dbo_PR_MST_Ground_SelectAll_ByUserID
        public DataTable dbo_PR_MST_Ground_SelectAll_ByUserID()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Ground_SelectAll_ByUserID");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CommonVariables.UserID());
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_MST_Ground_DeleteByPK
        public bool? dbo_PR_MST_Ground_DeleteByPK(int? GroundID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Ground_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "GroundID", SqlDbType.Int, GroundID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CommonVariables.UserID());
                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_MST_Ground_Insert
        public bool? dbo_PR_MST_Ground_Insert(MST_GroundModel modelMST_Ground)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Ground_Insert");

                sqlDB.AddInParameter(dbCMD, "@GroundName", SqlDbType.VarChar, modelMST_Ground.GroundName);
                sqlDB.AddInParameter(dbCMD, "@GroundNickName", SqlDbType.VarChar, modelMST_Ground.GroundNickName);
                sqlDB.AddInParameter(dbCMD, "@BOXCricketID", SqlDbType.Int, modelMST_Ground.BOXCricketID);
                sqlDB.AddInParameter(dbCMD, "@UserID", SqlDbType.Int, CommonVariables.UserID());
                sqlDB.AddInParameter(dbCMD, "@GroundCapacity", SqlDbType.Int, modelMST_Ground.GroundCapacity);
                sqlDB.AddInParameter(dbCMD, "@GroundHeight", SqlDbType.Decimal, modelMST_Ground.GroundHeight);
                sqlDB.AddInParameter(dbCMD, "@GroundWidth", SqlDbType.Decimal, modelMST_Ground.GroundWidth);
                sqlDB.AddInParameter(dbCMD, "@GroundLength", SqlDbType.Decimal, modelMST_Ground.GroundLength);
                sqlDB.AddInParameter(dbCMD, "@ContactPersonName", SqlDbType.VarChar, modelMST_Ground.GroundName);
                sqlDB.AddInParameter(dbCMD, "@ContactPersonNumber", SqlDbType.VarChar, modelMST_Ground.ContactPersonNumber);
                sqlDB.AddInParameter(dbCMD, "@IsAllowedBooking", SqlDbType.Bit, modelMST_Ground.IsAllowedBooking);
                sqlDB.AddInParameter(dbCMD, "Created", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                sqlDB.AddInParameter(dbCMD, "Modified", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_MST_Ground_SelectByPK
        public DataTable dbo_PR_MST_Ground_SelectByPK(int? GroundID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Ground_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "GroundID", SqlDbType.Int, GroundID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CommonVariables.UserID());
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_MST_Ground_UpdateByPK
        public bool? dbo_PR_MST_Ground_UpdateByPK(MST_GroundModel modelMST_Ground)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Ground_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "GroundID", SqlDbType.Int, modelMST_Ground.GroundID);
                sqlDB.AddInParameter(dbCMD, "@GroundName", SqlDbType.VarChar, modelMST_Ground.GroundName);
                sqlDB.AddInParameter(dbCMD, "@GroundNickName", SqlDbType.VarChar, modelMST_Ground.GroundNickName);
                sqlDB.AddInParameter(dbCMD, "@BOXCricketID", SqlDbType.Int, modelMST_Ground.BOXCricketID);
                sqlDB.AddInParameter(dbCMD, "@UserID", SqlDbType.Int, CommonVariables.UserID());
                sqlDB.AddInParameter(dbCMD, "@GroundCapacity", SqlDbType.Int, modelMST_Ground.GroundCapacity);
                sqlDB.AddInParameter(dbCMD, "@GroundHeight", SqlDbType.Decimal, modelMST_Ground.GroundHeight);
                sqlDB.AddInParameter(dbCMD, "@GroundWidth", SqlDbType.Decimal, modelMST_Ground.GroundWidth);
                sqlDB.AddInParameter(dbCMD, "@GroundLength", SqlDbType.Decimal, modelMST_Ground.GroundLength);
                sqlDB.AddInParameter(dbCMD, "@ContactPersonName", SqlDbType.VarChar, modelMST_Ground.GroundName);
                sqlDB.AddInParameter(dbCMD, "@ContactPersonNumber", SqlDbType.VarChar, modelMST_Ground.ContactPersonNumber);
                sqlDB.AddInParameter(dbCMD, "@IsAllowedBooking", SqlDbType.Bit, modelMST_Ground.IsAllowedBooking);
                sqlDB.AddInParameter(dbCMD, "Modified", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

    }
}

