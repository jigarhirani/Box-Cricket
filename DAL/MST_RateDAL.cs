
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BOXCricket.DAL
{
    public class MST_RateDAL : DAL_Helper
    {
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
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}
