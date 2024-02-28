
using BOXCricket.BAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BOXCricket.DAL
{
    public class MST_RateDAL : DAL_Helper
    {
        #region Method: dbo_PR_MST_BOXCricket_Dropdown_ByUserID
        public DataTable dbo_PR_MST_BOXCricket_Dropdown_ByUserID()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_BOXCricket_Dropdown_ByUserID");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CommonVariables.UserID());
                DataTable dtBOXCricket = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dtBOXCricket.Load(dr);
                }

                return dtBOXCricket;
            }
            catch (Exception)
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

        #region Method: dbo_PR_MST_Ground_DropdownByBOXCricket
        public DataTable dbo_PR_MST_Ground_DropdownByBOXCricket(int BOXCricketID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Ground_DropdownByBOXCricket");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CommonVariables.UserID());
                sqlDB.AddInParameter(dbCMD, "BOXCricketID", SqlDbType.Int, BOXCricketID);
                DataTable dtBOXCricket = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dtBOXCricket.Load(dr);
                }

                return dtBOXCricket;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion        

        #region Method: dbo_PR_MST_Slot_Dropdown
        public DataTable dbo_PR_MST_Slot_Dropdown()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Slot_Dropdown");

                DataTable dtSlot = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dtSlot.Load(dr);
                }

                return dtSlot;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion    

        #region Method: dbo_PR_MST_Rate_Search_ByFilters
        public DataTable dbo_PR_MST_Rate_Search_ByFilters(string DayOfWeek, decimal HourlyRate, int SlotNO, int BOXCricketID, int GroundID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Rate_Search_ByFilters");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CommonVariables.UserID());
                if (DayOfWeek != null)
                {
                    sqlDB.AddInParameter(dbCMD, "DayOfWeek", SqlDbType.VarChar, DayOfWeek);
                }

                if (HourlyRate != -1)
                {
                    sqlDB.AddInParameter(dbCMD, "HourlyRate", SqlDbType.Decimal, HourlyRate);
                }

                if (SlotNO != 0)
                {
                    sqlDB.AddInParameter(dbCMD, "SlotNO", SqlDbType.Int, SlotNO);
                }

                if (BOXCricketID != 0)
                {
                    sqlDB.AddInParameter(dbCMD, "BOXCricketID", SqlDbType.Int, BOXCricketID);
                }

                if (GroundID != 0)
                {
                    sqlDB.AddInParameter(dbCMD, "GroundID", SqlDbType.Int, GroundID);
                }

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

        #region Method: dbo_PR_MST_Rate_GetByDetails
        public DataTable dbo_PR_MST_Rate_GetByDetails(int BOXCricketID, int GroundID, string DayOfWeek, int SlotNO)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Rate_GetByDetails");
                sqlDB.AddInParameter(dbCMD, "@BOXCricketID", SqlDbType.Int, BOXCricketID);
                sqlDB.AddInParameter(dbCMD, "@GroundID", SqlDbType.Int, GroundID);
                sqlDB.AddInParameter(dbCMD, "@DayOfWeek", SqlDbType.VarChar, DayOfWeek);
                sqlDB.AddInParameter(dbCMD, "@SlotNo", SqlDbType.Int, SlotNO);
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
    }
}