using BOXCricket.BAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BOXCricket.DAL
{
    public class MST_GroundDAL : DAL_Helper
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

        #region Method: dbo_PR_MST_Ground_Search_ByFilter
        public DataTable dbo_PR_MST_Ground_Search_ByFilter(string GroundName, int GroundCapacity, string IsAllowedBooking, int BOXCricketID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Ground_Search_ByFilter");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CommonVariables.UserID());
                if (GroundName != null)
                {
                    sqlDB.AddInParameter(dbCMD, "GroundName", SqlDbType.VarChar, GroundName);
                }

                if (GroundCapacity != null)
                {
                    sqlDB.AddInParameter(dbCMD, "GroundCapacity", SqlDbType.Int, GroundCapacity);
                }

                if (IsAllowedBooking != null)
                {
                    sqlDB.AddInParameter(dbCMD, "IsAllowedBooking", SqlDbType.VarChar, IsAllowedBooking);
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
