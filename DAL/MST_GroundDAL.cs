using BOXCricket.BAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BOXCricket.DAL
{
    public class MST_GroundDAL : DAL_Helper
    {
        #region Method: dbo_PR_MST_BOXCricket_Dropdown
        public DataTable dbo_PR_MST_BOXCricket_Dropdown()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_BOXCricket_Dropdown");
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

        #region Method: dbo_PR_MST_Ground_Search
        public DataTable dbo_PR_MST_Ground_Search(string GroundName, int GroundCapacity)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Ground_Search");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CommonVariables.UserID());
                if (GroundName != null)
                {
                    sqlDB.AddInParameter(dbCMD, "GroundName", SqlDbType.VarChar, GroundName);
                }

                if (GroundCapacity != null)
                {
                    sqlDB.AddInParameter(dbCMD, "GroundCapacity", SqlDbType.Int, GroundCapacity);
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
