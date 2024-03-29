using BOXCricket.BAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BOXCricket.DAL
{
    public class MST_UserDALBase : DAL_Helper
    {
        #region Method: dbo_PR_MST_User_Insert
        public Boolean dbo_PR_MST_User_Insert(string ConnStr, string FirstName, string LastName, string Password, string Email, string Contact, string? ProfilePhotoPath, int? CountryID, int? StateID, int? CityID, string VarificationToken, DateTime? Created, DateTime? Modified)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_User_Insert");
                sqlDB.AddInParameter(dbCMD, "FirstName", SqlDbType.VarChar, FirstName);
                sqlDB.AddInParameter(dbCMD, "LastName", SqlDbType.VarChar, LastName);
                sqlDB.AddInParameter(dbCMD, "Password", SqlDbType.VarChar, Password);
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, Email);
                sqlDB.AddInParameter(dbCMD, "Contact", SqlDbType.VarChar, Contact);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, CityID);
                sqlDB.AddInParameter(dbCMD, "ProfilePhotoPath", SqlDbType.NVarChar, ProfilePhotoPath);
                sqlDB.AddInParameter(dbCMD, "VarificationToken", SqlDbType.NVarChar, VarificationToken);
                sqlDB.AddInParameter(dbCMD, "Created", SqlDbType.DateTime, DateTime.Now);
                sqlDB.AddInParameter(dbCMD, "Modified", SqlDbType.DateTime, DateTime.Now);

                Boolean result = Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD));
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Method: dbo_PR_MST_User_SelectByPK
        public DataTable dbo_PR_MST_User_SelectByPK(int UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_User_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);

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

        #region Method: dbo_PR_MST_User_UpdateByPK
        public Boolean dbo_PR_MST_User_UpdateByPK(string ConnStr, string FirstName, string LastName, string Password, string? Email, string? Contact, int? CountryID, int? StateID, int? CityID, string? ProfilePhotoPath, DateTime? Modified)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_User_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CommonVariables.UserID());
                sqlDB.AddInParameter(dbCMD, "FirstName", SqlDbType.VarChar, FirstName);
                sqlDB.AddInParameter(dbCMD, "LastName", SqlDbType.VarChar, LastName);
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, Email);
                sqlDB.AddInParameter(dbCMD, "Contact", SqlDbType.VarChar, Contact);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, CityID);
                sqlDB.AddInParameter(dbCMD, "ProfilePhotoPath", SqlDbType.NVarChar, ProfilePhotoPath);
                sqlDB.AddInParameter(dbCMD, "Modified", SqlDbType.DateTime, DateTime.Now);

                Boolean result = Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD));
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion       

    }
}