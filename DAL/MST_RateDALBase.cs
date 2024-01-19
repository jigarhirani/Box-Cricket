
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using BOXCricket.Areas.MST_Rate.Models;

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

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
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

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
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
                sqlDB.AddInParameter(dbCMD, "@UserID", SqlDbType.Int, modelMST_Rates.UserID);
                sqlDB.AddInParameter(dbCMD, "@DayOfWeek", SqlDbType.VarChar, modelMST_Rates.DayOfWeek);
                sqlDB.AddInParameter(dbCMD, "@StartTime", SqlDbType.DateTime, modelMST_Rates.StartTime);
                sqlDB.AddInParameter(dbCMD, "@EndTime", SqlDbType.DateTime, modelMST_Rates.EndTime);
                sqlDB.AddInParameter(dbCMD, "@HourlyRate", SqlDbType.Decimal, modelMST_Rates.HourlyRate);                
                sqlDB.AddInParameter(dbCMD, "Created", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                sqlDB.AddInParameter(dbCMD, "Modified", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
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

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception ex)
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
                sqlDB.AddInParameter(dbCMD, "@UserID", SqlDbType.Int, modelMST_Rates.UserID);
                sqlDB.AddInParameter(dbCMD, "@DayOfWeek", SqlDbType.VarChar, modelMST_Rates.DayOfWeek);
                sqlDB.AddInParameter(dbCMD, "@StartTime", SqlDbType.DateTime, modelMST_Rates.StartTime);
                sqlDB.AddInParameter(dbCMD, "@EndTime", SqlDbType.DateTime, modelMST_Rates.EndTime);
                sqlDB.AddInParameter(dbCMD, "@HourlyRate", SqlDbType.Decimal, modelMST_Rates.HourlyRate);
                sqlDB.AddInParameter(dbCMD, "@Modified", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_MST_Ground_Dropdown
        public DataTable dbo_PR_MST_Ground_Dropdown()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Ground_Dropdown");
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion    

        #region Method: dbo_PR_MST_Rate_Search
        public DataTable dbo_PR_MST_Rate_Search(string DayOfWeek, decimal HourlyRate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Rate_Search");

                if (DayOfWeek != null)
                {
                    sqlDB.AddInParameter(dbCMD, "DayOfWeek", SqlDbType.VarChar, DayOfWeek);
                }

                if (HourlyRate != -1)
                {
                    sqlDB.AddInParameter(dbCMD, "HourlyRate", SqlDbType.Decimal, HourlyRate);
                }

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
