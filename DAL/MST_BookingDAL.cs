using BOXCricket.BAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BOXCricket.DAL
{
    public class MST_BookingDAL : DAL_Helper
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
                DataTable dtGround = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dtGround.Load(dr);
                }

                return dtGround;
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

        #region Method: dbo_PR_MST_Ground_AllowedtoBookDropdown_ByBOXCricket
        public DataTable dbo_PR_MST_Ground_AllowedtoBookDropdown_ByBOXCricket(int BOXCricketID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Ground_AllowedtoBookDropdown_ByBOXCricket");
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

        #region Method: dbo_PR_MST_Slot_Dropdown_Validation_ByDate
        public DataTable dbo_PR_MST_Slot_Dropdown_Validation_ByDate(int GroundID, DateTime BookingDate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Slot_Dropdown_Validation_ByDate");
                sqlDB.AddInParameter(dbCMD, "@GroundID", SqlDbType.Int, GroundID);
                sqlDB.AddInParameter(dbCMD, "@BookingDate", SqlDbType.DateTime, BookingDate);
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

        #region Method: dbo_PR_MST_Rate_HourlyRate_BySlotAndDate
        public DataTable dbo_PR_MST_Rate_HourlyRate_BySlotAndDate(int BOXCricketID, int GroundID, int SlotNO, DateTime BookingDate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Rate_HourlyRate_BySlotAndDate");
                sqlDB.AddInParameter(dbCMD, "@BOXCricketID", SqlDbType.Int, BOXCricketID);
                sqlDB.AddInParameter(dbCMD, "@GroundID", SqlDbType.Int, GroundID);
                sqlDB.AddInParameter(dbCMD, "@SlotNo", SqlDbType.Int, SlotNO);
                sqlDB.AddInParameter(dbCMD, "@BookingDate", SqlDbType.DateTime, BookingDate);
                DataTable dtRate = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dtRate.Load(dr);
                }

                return dtRate;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_MST_Booking_Search_ByFilters
        public DataTable dbo_PR_MST_Booking_Search_ByFilters(string UserName, int GroundID, int BOXCricketID, string Status)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Booking_Search_ByFilters");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CommonVariables.UserID());
                if (UserName != null)
                {
                    sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, UserName);
                }

                if (Status != null)
                {
                    sqlDB.AddInParameter(dbCMD, "Status", SqlDbType.VarChar, Status);
                }

                if (GroundID != 0)
                {
                    sqlDB.AddInParameter(dbCMD, "GroundID", SqlDbType.Int, GroundID);
                }

                if (BOXCricketID != 0)
                {
                    sqlDB.AddInParameter(dbCMD, "BOXCricketID", SqlDbType.Int, BOXCricketID);
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
    }
}
