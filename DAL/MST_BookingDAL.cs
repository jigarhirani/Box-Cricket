﻿using BOXCricket.BAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BOXCricket.DAL
{
    public class MST_BookingDAL : DAL_Helper
    {
        #region Method: dbo_PR_MST_Ground_Dropdown
        public DataTable dbo_PR_MST_Ground_Dropdown()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Ground_Dropdown");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CommonVariables.UserID());
                DataTable dtGround = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dtGround.Load(dr);
                }

                return dtGround;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion    

        //#region Method: dbo_PR_MST_Slot_Dropdown
        //public DataTable dbo_PR_MST_Slot_Dropdown()
        //{
        //    try
        //    {
        //        SqlDatabase sqlDB = new SqlDatabase(ConnStr);
        //        DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Slot_Dropdown");

        //        DataTable dtSlot = new DataTable();
        //        using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
        //        {
        //            dtSlot.Load(dr);
        //        }

        //        return dtSlot;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        //#endregion    

        #region Method: dbo_PR_MST_Slot_Dropdown_Validation
        public DataTable dbo_PR_MST_Slot_Dropdown_Validation(int GroundID, DateTime BookingDate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Slot_Dropdown_Validation");
                sqlDB.AddInParameter(dbCMD, "@GroundID", SqlDbType.Int, GroundID);
                sqlDB.AddInParameter(dbCMD, "@BookingDate", SqlDbType.DateTime, BookingDate);
                DataTable dtSlot = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dtSlot.Load(dr);
                }

                return dtSlot;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion    

        #region Method: dbo_PR_MST_Booking_Search
        public DataTable dbo_PR_MST_Booking_Search(string UserName, int GroundID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Booking_Search");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CommonVariables.UserID());
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
