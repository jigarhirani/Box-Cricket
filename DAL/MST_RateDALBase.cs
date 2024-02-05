
using BOXCricket.Areas.MST_Rate.Models;
using BOXCricket.BAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BOXCricket.DAL
{
    public class MST_RateDALBase : DAL_Helper
    {
        #region Method: dbo_PR_MST_Rate_SelectAll
        public DataTable dbo_PR_MST_Rate_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Rate_SelectAll");
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

        #region Method: dbo_PR_MST_Rate_DeleteByPK
        public bool? dbo_PR_MST_Rate_DeleteByPK(int? RateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Rate_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "RateID", SqlDbType.Int, RateID);
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

        #region Method: dbo_PR_MST_Rate_Insert
        public bool? dbo_PR_MST_Rate_Insert(MST_RateModel modelMST_Rates)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Rate_Insert");
                sqlDB.AddInParameter(dbCMD, "@GroundID", SqlDbType.Int, modelMST_Rates.GroundID);
                sqlDB.AddInParameter(dbCMD, "@UserID", SqlDbType.Int, CommonVariables.UserID());
                sqlDB.AddInParameter(dbCMD, "@DayOfWeek", SqlDbType.VarChar, modelMST_Rates.DayOfWeek);
                sqlDB.AddInParameter(dbCMD, "@SlotNO", SqlDbType.Int, modelMST_Rates.SlotNO);
                sqlDB.AddInParameter(dbCMD, "@HourlyRate", SqlDbType.Decimal, modelMST_Rates.HourlyRate);
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

        #region Method: dbo_PR_MST_Rate_SelectByPK
        public DataTable dbo_PR_MST_Rate_SelectByPK(int? RateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Rate_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "RateID", SqlDbType.Int, RateID);
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

        #region Method: dbo_PR_MST_Rate_UpdateByPK
        public bool? dbo_PR_MST_Rate_UpdateByPK(MST_RateModel modelMST_Rates)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Rate_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "RateID", SqlDbType.Int, modelMST_Rates.RateID);
                sqlDB.AddInParameter(dbCMD, "@GroundID", SqlDbType.Int, modelMST_Rates.GroundID);
                sqlDB.AddInParameter(dbCMD, "@UserID", SqlDbType.Int, CommonVariables.UserID());
                sqlDB.AddInParameter(dbCMD, "@DayOfWeek", SqlDbType.VarChar, modelMST_Rates.DayOfWeek);
                sqlDB.AddInParameter(dbCMD, "@SlotNO", SqlDbType.Int, modelMST_Rates.SlotNO);
                sqlDB.AddInParameter(dbCMD, "@HourlyRate", SqlDbType.Decimal, modelMST_Rates.HourlyRate);
                sqlDB.AddInParameter(dbCMD, "@Modified", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

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
