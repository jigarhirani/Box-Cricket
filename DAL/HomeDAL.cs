using BOXCricket.BAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BOXCricket.DAL
{
    public class HomeDAL : DAL_Helper
    {
        #region Method: dbo_PR_MST_Ground_SelectAll_ByBoxCricketID
        public DataTable dbo_PR_MST_Ground_SelectAll_ByBoxCricketID(int? BOXCricketID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Ground_SelectAll_ByBoxCricketID");
                sqlDB.AddInParameter(dbCMD, "BOXCricketID", SqlDbType.Int, BOXCricketID);
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

        #region Method: dbo_PR_MST_Booking_SelectAll_ByUserID
        public DataTable dbo_PR_MST_Booking_SelectAll_ByUserID()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Booking_SelectAll_ByUserID");
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

        #region Method: dbo_PR_Dashboard_Counts
        public DataTable dbo_PR_Dashboard_Counts()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_Dashboard_Counts");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CommonVariables.UserID());
                DataTable dtcounts = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dtcounts.Load(dr);
                }

                return dtcounts;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_MST_BOXCricket_ByFilters
        public DataTable dbo_PR_MST_BOXCricket_ByFilters(int CityID, decimal HourlyRate, DateTime BookingDate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_BOXCricket_ByFilters");
                sqlDB.AddInParameter(dbCMD, "OwnerID", SqlDbType.Int, CommonVariables.UserID());
                if (CityID != 0)
                {
                    sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, CityID);
                }

                if (HourlyRate != 0)
                {
                    sqlDB.AddInParameter(dbCMD, "HourlyRate", SqlDbType.Decimal, HourlyRate);
                }

                if (BookingDate != null)
                {
                    sqlDB.AddInParameter(dbCMD, "BookingDate", SqlDbType.DateTime, BookingDate);
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

        #region Method: dbo_PR_LOC_City_SelectDropDownList
        public DataTable dbo_PR_LOC_City_SelectDropDownList()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_City_SelectDropDownList");
                DataTable dtCity = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dtCity.Load(dr);
                }

                return dtCity;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}
