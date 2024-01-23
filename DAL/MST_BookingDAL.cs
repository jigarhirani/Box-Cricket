using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BOXCricket.DAL
{
    public class MST_BookingDAL : DAL_Helper
    {

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
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}
