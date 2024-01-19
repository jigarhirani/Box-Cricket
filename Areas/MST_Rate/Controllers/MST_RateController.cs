using BOXCricket.Areas.MST_Rate.Models;
using BOXCricket.BAL;
using BOXCricket.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BOXCricket.Areas.MST_Rate.Controllers
{
    [CheckAccess]
    [Area("MST_Rate")]
    [Route("MST_Rate/[controller]/[action]")]
    public class MST_RateController : Controller
    {
        MST_RateDALBase dalMST_RateDAL = new MST_RateDALBase();
        public MST_RateController()
        {

        }

        #region Select All 
        public IActionResult RateList()
        {
            DataTable dt = dalMST_RateDAL.dbo_PR_MST_Rate_SelectAll();
            return View("RateList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int RateID)
        {
            if (Convert.ToBoolean(dalMST_RateDAL.dbo_PR_MST_Rate_DeleteByPK(RateID)))
                return RedirectToAction("RateList");
            return View("RateList");
        }

        #endregion

        #region Add/Edit
        public IActionResult Add(int? RateID)
        {

            #region Dropdown For Ground           
            DataTable dtGround = dalMST_RateDAL.dbo_PR_MST_Ground_Dropdown();

            List<MST_GroundDropDownModel> MST_GroundDropdown_List = new List<MST_GroundDropDownModel>();
            foreach (DataRow dr in dtGround.Rows)
            {
                MST_GroundDropDownModel vlst = new MST_GroundDropDownModel();
                vlst.GroundID = Convert.ToInt32(dr["GroundID"]);
                vlst.GroundName = dr["GroundName"].ToString();
                MST_GroundDropdown_List.Add(vlst);
            }
            ViewBag.GroundList = MST_GroundDropdown_List;
            #endregion            

            #region Record Select by PK
            if (RateID != null)
            {
                DataTable dt = dalMST_RateDAL.dbo_PR_MST_Rate_SelectByPK(RateID);
                if (dt.Rows.Count > 0)
                {
                    MST_RateModel model = new MST_RateModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        model.RateID = Convert.ToInt32(dr["RateID"]);
                        model.GroundID = Convert.ToInt32(dr["GroundID"]);
                        model.UserID = Convert.ToInt32(dr["UserID"]);
                        model.DayOfWeek = dr["DayOfWeek"].ToString();
                        model.StartTime = Convert.ToDateTime(dr["StartTime"].ToString());
                        model.EndTime = Convert.ToDateTime(dr["EndTime"].ToString());
                        model.HourlyRate = Convert.ToDecimal(dr["HourlyRate"]);
                    }
                    return View("RateAddEdit", model);
                }
            }
            #endregion           

            return View("RateAddEdit");
        }
        #endregion

        #region Save 
        [HttpPost]
        public IActionResult Save(MST_RateModel modelMST_Rate)
        {

            if (ModelState.IsValid)
            {
                if (modelMST_Rate.RateID == null)
                {
                    if (Convert.ToBoolean(dalMST_RateDAL.dbo_PR_MST_Rate_Insert(modelMST_Rate)))
                        TempData["successMessage"] = "Record Inserted Successfully";
                    return RedirectToAction("RateList");
                }
                else
                {
                    if (Convert.ToBoolean(dalMST_RateDAL.dbo_PR_MST_Rate_UpdateByPK(modelMST_Rate)))
                        TempData["successMessage"] = "Record Updated Successfully";
                    return RedirectToAction("RateList");
                }
            }
            TempData["errorMessage"] = "Some error has occurred";
            return RedirectToAction("RateList");
        }
        #endregion       

        #region RateSearch
        public IActionResult RateSearch(string DayOfWeek, decimal HourlyRate)
        {
            DataTable dt = dalMST_RateDAL.dbo_PR_MST_Rate_Search(DayOfWeek, HourlyRate);
            return View("RateList", dt);

        }
        #endregion
    }
}
