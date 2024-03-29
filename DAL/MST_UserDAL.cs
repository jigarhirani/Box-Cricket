using BOXCricket.Areas.MST_User.Models;
using BOXCricket.BAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BOXCricket.DAL
{
    public class MST_UserDAL : DAL_Helper
    {
        #region Method: dbo_PR_MST_User_Select_ByEmailPassword
        public DataTable dbo_PR_MST_User_Select_ByEmailPassword(string ConnStr, string Email, string Password)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_User_Select_ByEmailPassword");
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, Email);
                sqlDB.AddInParameter(dbCMD, "Password", SqlDbType.VarChar, Password);

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

        #region Method: dbo_PR_MST_User_Select_ByEmail
        public DataTable dbo_PR_MST_User_Select_ByEmail(string ConnStr, string Email)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_User_Select_ByEmail");
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, Email);

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

        #region Method: dbo_PR_MST_Ground_SelectbyDropdown
        public DataTable dbo_PR_LOC_Country_SelectByDropdownList()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_Country_Dropdown");
                DataTable dtCountry = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dtCountry.Load(dr);
                }

                return dtCountry;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_LOC_State_DropdownByCountry
        public DataTable dbo_PR_LOC_State_DropdownByCountry(int CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_State_DropdownByCountry");
                sqlDB.AddInParameter(dbCMD, "@CountryID", SqlDbType.Int, CountryID);
                DataTable dtState = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dtState.Load(dr);
                }

                return dtState;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_LOC_State_DropdownByCountry
        public DataTable dbo_PR_LOC_City_DropdownByState(int StateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_City_DropdownByState");
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
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

        #region Method: dbo_PR_MST_User_Password_UpdateByPK
        public bool? dbo_PR_MST_User_Password_UpdateByPK(MST_User_PasswrodChange modelMST_User_PasswordChange)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_User_Password_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CommonVariables.UserID());
                sqlDB.AddInParameter(dbCMD, "CurruntPassword", SqlDbType.VarChar, modelMST_User_PasswordChange.CurruntPassword);
                sqlDB.AddInParameter(dbCMD, "NewPassword", SqlDbType.VarChar, modelMST_User_PasswordChange.NewPassword);
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

        #region Method: dbo_PR_MST_User_PasswordReset_Update
        public bool? dbo_PR_MST_User_PasswordReset_Update(string Email, MST_User_PasswrodReset modelMST_User_PasswrodReset)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_User_PasswordReset_Update");
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, Email);
                sqlDB.AddInParameter(dbCMD, "NewPassword", SqlDbType.VarChar, modelMST_User_PasswrodReset.NewPassword);
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

        #region Method: dbo_PR_MST_User_EmailVarification_UpdateByPK
        public bool? dbo_PR_MST_User_EmailVarification_UpdateByPK(string Email, string token)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_User_EmailVarification_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, Email);
                sqlDB.AddInParameter(dbCMD, "VarificationToken", SqlDbType.NVarChar, token);

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_MST_User_VarificationToken_UpdateByEmail
        public Boolean dbo_PR_MST_User_VarificationToken_UpdateByEmail(string Email, string VarificationToken)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_User_VarificationToken_UpdateByEmail");
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, Email);
                sqlDB.AddInParameter(dbCMD, "VarificationToken", SqlDbType.NVarChar, VarificationToken);
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
