using BOXCricket.Areas.MST_BOXCricket.Models;
using BOXCricket.BAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BOXCricket.DAL
{
    public class MST_BOXCricketDALBase : DAL_Helper
    {
        #region Method: dbo_PR_MST_BOXCricket_SelectAll
        public DataTable dbo_PR_MST_BOXCricket_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_BOXCricket_SelectAll");
                sqlDB.AddInParameter(dbCMD, "OwnerID", SqlDbType.Int, CommonVariables.UserID());
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

        #region Method: dbo_PR_MST_BOXCricket_DeleteByPK
        public bool? dbo_PR_MST_BOXCricket_DeleteByPK(int? BOXCricketID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_BOXCricket_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "BOXCricketID", SqlDbType.Int, BOXCricketID);
                sqlDB.AddInParameter(dbCMD, "OwnerID", SqlDbType.Int, CommonVariables.UserID());
                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_MST_BOXCricket_Insert
        public bool? dbo_PR_MST_BOXCricket_Insert(MST_BOXCricketModel modelMST_BOXCricket)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_BOXCricket_Insert");

                sqlDB.AddInParameter(dbCMD, "@BOXCricketName", SqlDbType.VarChar, modelMST_BOXCricket.BOXCricketName);
                sqlDB.AddInParameter(dbCMD, "@OwnerID", SqlDbType.Int, CommonVariables.UserID());
                sqlDB.AddInParameter(dbCMD, "@Address", SqlDbType.VarChar, modelMST_BOXCricket.Address);
                sqlDB.AddInParameter(dbCMD, "@CountryID", SqlDbType.Int, modelMST_BOXCricket.CountryID);
                sqlDB.AddInParameter(dbCMD, "@StateID", SqlDbType.Int, modelMST_BOXCricket.StateID);
                sqlDB.AddInParameter(dbCMD, "@CityID", SqlDbType.Int, modelMST_BOXCricket.CityID);
                sqlDB.AddInParameter(dbCMD, "Created", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                sqlDB.AddInParameter(dbCMD, "Modified", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_MST_BOXCricket_SelectByPK
        public DataTable dbo_PR_MST_BOXCricket_SelectByPK(int? BOXCricketID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_BOXCricket_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "BOXCricketID", SqlDbType.Int, BOXCricketID);
                sqlDB.AddInParameter(dbCMD, "OwnerID", SqlDbType.Int, CommonVariables.UserID());
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

        #region Method: dbo_PR_MST_BOXCricket_UpdateByPK
        public bool? dbo_PR_MST_BOXCricket_UpdateByPK(MST_BOXCricketModel modelMST_BOXCricket)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_BOXCricket_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "BOXCricketID", SqlDbType.Int, modelMST_BOXCricket.BOXCricketID);
                sqlDB.AddInParameter(dbCMD, "@BOXCricketName", SqlDbType.VarChar, modelMST_BOXCricket.BOXCricketName);
                sqlDB.AddInParameter(dbCMD, "@OwnerID", SqlDbType.Int, CommonVariables.UserID());
                sqlDB.AddInParameter(dbCMD, "@Address", SqlDbType.VarChar, modelMST_BOXCricket.Address);
                sqlDB.AddInParameter(dbCMD, "@CountryID", SqlDbType.Int, modelMST_BOXCricket.CountryID);
                sqlDB.AddInParameter(dbCMD, "@StateID", SqlDbType.Int, modelMST_BOXCricket.StateID);
                sqlDB.AddInParameter(dbCMD, "@CityID", SqlDbType.Int, modelMST_BOXCricket.CityID);
                sqlDB.AddInParameter(dbCMD, "Modified", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

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
