using BOXCricket.Areas.MST_Ground.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BOXCricket.DAL
{
    public class MST_GroundDALBase : DAL_Helper
    {

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

        #region Method: dbo_PR_MST_Ground_DeleteByPK
        public bool? dbo_PR_MST_Ground_DeleteByPK(int? GroundID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Ground_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "GroundID", SqlDbType.Int, GroundID);

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_MST_Ground_Insert
        public bool? dbo_PR_MST_Ground_Insert(MST_GroundModel modelMST_Ground)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Ground_Insert");
                sqlDB.AddInParameter(dbCMD, "@GroundName", SqlDbType.VarChar, modelMST_Ground.GroundName);


                sqlDB.AddInParameter(dbCMD, "@UserID", SqlDbType.Int, modelMST_Ground.UserID);
                sqlDB.AddInParameter(dbCMD, "@GroundCapacity", SqlDbType.Int, modelMST_Ground.GroundCapacity);
                sqlDB.AddInParameter(dbCMD, "@GroundWidth", SqlDbType.Decimal, modelMST_Ground.GroundWidth);
                sqlDB.AddInParameter(dbCMD, "@GroundHeight", SqlDbType.Decimal, modelMST_Ground.GroundHeight);
                sqlDB.AddInParameter(dbCMD, "@CountryID", SqlDbType.Int, modelMST_Ground.CountryID);
                sqlDB.AddInParameter(dbCMD, "@StateID", SqlDbType.Int, modelMST_Ground.StateID);
                sqlDB.AddInParameter(dbCMD, "@CityID", SqlDbType.Int, modelMST_Ground.CityID);
                sqlDB.AddInParameter(dbCMD, "@Address", SqlDbType.VarChar, modelMST_Ground.Address);
                sqlDB.AddInParameter(dbCMD, "@IsAllowedBooking", SqlDbType.Bit, modelMST_Ground.IsAllowedBooking);
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

        #region Method: dbo_PR_MST_Ground_SelectByPK
        public DataTable dbo_PR_MST_Ground_SelectByPK(int? GroundID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Ground_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "GroundID", SqlDbType.Int, GroundID);

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

        #region Method: dbo_PR_MST_Ground_UpdateByPK
        public bool? dbo_PR_MST_Ground_UpdateByPK(MST_GroundModel modelMST_Ground)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Ground_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "GroundID", SqlDbType.Int, modelMST_Ground.GroundID);
                sqlDB.AddInParameter(dbCMD, "@GroundName", SqlDbType.VarChar, modelMST_Ground.GroundName);
                sqlDB.AddInParameter(dbCMD, "@UserID", SqlDbType.Int, modelMST_Ground.UserID);
                sqlDB.AddInParameter(dbCMD, "@GroundCapacity", SqlDbType.Int, modelMST_Ground.GroundCapacity);
                sqlDB.AddInParameter(dbCMD, "@GroundWidth", SqlDbType.Decimal, modelMST_Ground.GroundWidth);
                sqlDB.AddInParameter(dbCMD, "@GroundHeight", SqlDbType.Decimal, modelMST_Ground.GroundHeight);
                sqlDB.AddInParameter(dbCMD, "@CountryID", SqlDbType.Int, modelMST_Ground.CountryID);
                sqlDB.AddInParameter(dbCMD, "@StateID", SqlDbType.Int, modelMST_Ground.StateID);
                sqlDB.AddInParameter(dbCMD, "@CityID", SqlDbType.Int, modelMST_Ground.CityID);
                sqlDB.AddInParameter(dbCMD, "@Address", SqlDbType.VarChar, modelMST_Ground.Address);
                sqlDB.AddInParameter(dbCMD, "@IsAllowedBooking", SqlDbType.Bit, modelMST_Ground.IsAllowedBooking);
                sqlDB.AddInParameter(dbCMD, "@Modified", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_MST_Ground_SelectbyDropdown
        public DataTable dbo_PR_MST_Country_SelectByDropdownList()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Country_Dropdown");
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

        #region Method: dbo_PR_MST_State_DropdownByCountry
        public DataTable dbo_PR_MST_State_DropdownByCountry(int CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_State_DropdownByCountry");
                sqlDB.AddInParameter(dbCMD, "@CountryID", SqlDbType.Int, CountryID);
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

        #region Method: dbo_PR_MST_State_DropdownByCountry
        public DataTable dbo_PR_MST_City_DropdownByState(int StateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_City_DropdownByState");
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
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

