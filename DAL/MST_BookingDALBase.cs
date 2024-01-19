using BOXCricket.Areas.MST_Booking.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace BOXCricket.DAL
{
    public class MST_BookingDALBase : DAL_Helper
    {
        #region Method: dbo_PR_MST_Booking_SelectAll
        public DataTable dbo_PR_MST_Booking_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Booking_SelectAll");

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

        #region Method: dbo_PR_MST_Booking_DeleteByPK
        public bool? dbo_PR_MST_Booking_DeleteByPK(int? BookingID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Booking_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "BookingID", SqlDbType.Int, BookingID);

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_MST_Booking_Insert
        public bool? dbo_PR_MST_Booking_Insert(MST_BookingModel modelMST_Bookings)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Booking_Insert");
                sqlDB.AddInParameter(dbCMD, "@GroundID", SqlDbType.Int, modelMST_Bookings.GroundID);
                sqlDB.AddInParameter(dbCMD, "@UserID", SqlDbType.Int, modelMST_Bookings.UserID);
                sqlDB.AddInParameter(dbCMD, "@BookingDate", SqlDbType.DateTime, modelMST_Bookings.BookingDate);
                sqlDB.AddInParameter(dbCMD, "@FromTime", SqlDbType.DateTime, modelMST_Bookings.FromTime);
                sqlDB.AddInParameter(dbCMD, "@ToTime", SqlDbType.DateTime, modelMST_Bookings.ToTime);
                sqlDB.AddInParameter(dbCMD, "@Status", SqlDbType.VarChar, modelMST_Bookings.Status);
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

        #region Method: dbo_PR_MST_Booking_SelectByPK
        public DataTable dbo_PR_MST_Booking_SelectByPK(int? BookingID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Booking_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "BookingID", SqlDbType.Int, BookingID);

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

        #region Method: dbo_PR_MST_Booking_UpdateByPK
        public bool? dbo_PR_MST_Booking_UpdateByPK(MST_BookingModel modelMST_Bookings)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Booking_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "BookingID", SqlDbType.Int, modelMST_Bookings.BookingID);
                sqlDB.AddInParameter(dbCMD, "@GroundID", SqlDbType.Int, modelMST_Bookings.GroundID);
                sqlDB.AddInParameter(dbCMD, "@UserID", SqlDbType.Int, modelMST_Bookings.UserID);
                sqlDB.AddInParameter(dbCMD, "@BookingDate", SqlDbType.DateTime, modelMST_Bookings.BookingDate);
                sqlDB.AddInParameter(dbCMD, "@FromTime", SqlDbType.DateTime, modelMST_Bookings.FromTime);
                sqlDB.AddInParameter(dbCMD, "@ToTime", SqlDbType.DateTime, modelMST_Bookings.ToTime);
                sqlDB.AddInParameter(dbCMD, "@Status", SqlDbType.VarChar, modelMST_Bookings.Status);
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

        #region Method: dbo_PR_MST_Booking_Search
        public DataTable dbo_PR_MST_Booking_Search(string UserName, int GroundID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Booking_Search");

                if (UserName != null)
                {
                    sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, UserName);
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
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
