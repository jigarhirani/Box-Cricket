using BOXCricket.BAL;
using BOXCricket.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BOXCricket.DAL
{
    public class HomeDALBase : DAL_Helper
    {
        #region Method: dbo_PR_MST_BOXCricket_SelectAll
        public DataTable dbo_PR_MST_BOXCricket_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_BOXCricket_SelectAll");
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

        #region Method: dbo_PR_MST_Ground_SelectAll
        public DataTable dbo_PR_MST_Ground_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Ground_SelectAll");
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
        public bool? dbo_PR_MST_Booking_Insert(BookingModel modelBooking)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Booking_Insert");
                sqlDB.AddInParameter(dbCMD, "@BOXCricketID", SqlDbType.Int, modelBooking.BOXCricketID);
                sqlDB.AddInParameter(dbCMD, "@GroundID", SqlDbType.Int, modelBooking.GroundID);
                sqlDB.AddInParameter(dbCMD, "@BookedBy", SqlDbType.Int, CommonVariables.UserID());
                sqlDB.AddInParameter(dbCMD, "@BookingDate", SqlDbType.DateTime, modelBooking.BookingDate);
                sqlDB.AddInParameter(dbCMD, "@Slots", SqlDbType.NVarChar, modelBooking.Slots);
                sqlDB.AddInParameter(dbCMD, "@BookingAmount", SqlDbType.Decimal, modelBooking.BookingAmount);
                sqlDB.AddInParameter(dbCMD, "@Status", SqlDbType.VarChar, modelBooking.Status);
                sqlDB.AddInParameter(dbCMD, "@Remarks", SqlDbType.VarChar, modelBooking.Remarks);
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
    }
}
