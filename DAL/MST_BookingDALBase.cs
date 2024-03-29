using BOXCricket.Areas.MST_Booking.Models;
using BOXCricket.BAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BOXCricket.DAL
{
    public class MST_BookingDALBase : DAL_Helper
    {
        #region Method: dbo_PR_MST_Booking_SelectAll_ByOwner
        public DataTable dbo_PR_MST_Booking_SelectAll_ByOwner()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Booking_SelectAll_ByOwner");
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

        #region Method: dbo_PR_MST_Booking_Insert
        public bool? dbo_PR_MST_Booking_Insert(MST_BookingModel modelMST_Bookings)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Booking_Insert");
                sqlDB.AddInParameter(dbCMD, "@BOXCricketID", SqlDbType.Int, modelMST_Bookings.BOXCricketID);
                sqlDB.AddInParameter(dbCMD, "@GroundID", SqlDbType.Int, modelMST_Bookings.GroundID);
                sqlDB.AddInParameter(dbCMD, "@BookedBy", SqlDbType.Int, CommonVariables.UserID());
                sqlDB.AddInParameter(dbCMD, "@BookingDate", SqlDbType.DateTime, modelMST_Bookings.BookingDate);
                sqlDB.AddInParameter(dbCMD, "@Slots", SqlDbType.NVarChar, modelMST_Bookings.Slots);
                sqlDB.AddInParameter(dbCMD, "@BookingAmount", SqlDbType.Decimal, modelMST_Bookings.BookingAmount);
                sqlDB.AddInParameter(dbCMD, "@Status", SqlDbType.VarChar, modelMST_Bookings.Status);
                sqlDB.AddInParameter(dbCMD, "@Remarks", SqlDbType.VarChar, modelMST_Bookings.Remarks);
                sqlDB.AddInParameter(dbCMD, "Created", SqlDbType.DateTime, DateTime.Now);
                sqlDB.AddInParameter(dbCMD, "Modified", SqlDbType.DateTime, DateTime.Now);

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception)
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
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_MST_Booking_StatusUpdateByPK
        public bool? dbo_PR_MST_Booking_StatusUpdateByPK(MST_BookingStatusUpdateModel modelMST_BookingStatusUpdate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Booking_StatusUpdateByPK");
                sqlDB.AddInParameter(dbCMD, "BookingID", SqlDbType.Int, modelMST_BookingStatusUpdate.BookingID);
                sqlDB.AddInParameter(dbCMD, "@Status", SqlDbType.VarChar, modelMST_BookingStatusUpdate.Status);
                sqlDB.AddInParameter(dbCMD, "@Remarks", SqlDbType.VarChar, modelMST_BookingStatusUpdate.Remarks);
                sqlDB.AddInParameter(dbCMD, "@Modified", SqlDbType.DateTime, DateTime.Now);

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
