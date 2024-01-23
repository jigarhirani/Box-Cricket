using BOXCricket.Areas.MST_Ground.Models;
using BOXCricket.BAL;
using BOXCricket.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BOXCricket.Areas.MST_Ground.Controllers
{
    [CheckAccess]
    [Area("MST_Ground")]
    [Route("MST_Ground/[controller]/[action]")]
    public class MST_GroundController : Controller
    {
        MST_GroundDALBase dalMST_GroundDALBase = new MST_GroundDALBase();
        MST_GroundDAL dalMST_GroundDAL = new MST_GroundDAL();
        public MST_GroundController()
        {

        }

        #region Select All 
        public IActionResult GroundList()
        {
            DataTable dt = dalMST_GroundDALBase.dbo_PR_MST_Ground_SelectAll();
            return View("GroundList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int GroundID)
        {
            if (Convert.ToBoolean(dalMST_GroundDALBase.dbo_PR_MST_Ground_DeleteByPK(GroundID)))
                return RedirectToAction("GroundList");
            return View("GroundList");
        }

        #endregion

        #region Add/Edit
        public IActionResult Add(int? GroundID)
        {

            #region Dropdown For Country           
            DataTable dtCountry = dalMST_GroundDALBase.dbo_PR_MST_Country_SelectByDropdownList();

            List<MST_CountryDropDownModel> MST_CountryDropdown_List = new List<MST_CountryDropDownModel>();
            foreach (DataRow dr in dtCountry.Rows)
            {
                MST_CountryDropDownModel vlst = new MST_CountryDropDownModel();
                vlst.CountryID = Convert.ToInt32(dr["CountryID"]);
                vlst.CountryName = dr["CountryName"].ToString();
                MST_CountryDropdown_List.Add(vlst);
            }
            ViewBag.CountryList = MST_CountryDropdown_List;
            #endregion

            #region Dropdown For State

            List<MST_StateDropDownModel> MST_StateDropdown_List = new List<MST_StateDropDownModel>();
            ViewBag.StateList = MST_StateDropdown_List;

            #endregion

            #region Dropdown For City 

            List<MST_CityDropDownModel> MST_CityDropdown_List = new List<MST_CityDropDownModel>();
            ViewBag.CityList = MST_CityDropdown_List;

            #endregion

            #region Record Select by PK
            if (GroundID != null)
            {
                DataTable dt = dalMST_GroundDALBase.dbo_PR_MST_Ground_SelectByPK(GroundID);
                if (dt.Rows.Count > 0)
                {
                    MST_GroundModel model = new MST_GroundModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        model.GroundID = Convert.ToInt32(dr["GroundID"]);
                        model.GroundName = dr["GroundName"].ToString();
                        model.UserID = Convert.ToInt32(dr["UserID"]);
                        model.GroundCapacity = Convert.ToInt32(dr["GroundCapacity"]);
                        model.GroundWidth = Convert.ToDecimal(dr["GroundWidth"]);
                        model.GroundHeight = Convert.ToDecimal(dr["GroundWidth"]);
                        model.CountryID = Convert.ToInt32(dr["CountryID"]);
                        model.StateID = Convert.ToInt32(dr["CountryID"]);
                        model.CityID = Convert.ToInt32(dr["CityID"]);
                        model.Address = dr["Address"].ToString();
                        model.IsAllowedBooking = Convert.ToBoolean(dr["IsAllowedBooking"].ToString());
                    }
                    return View("GroundAddEdit", model);
                }
            }
            #endregion           

            return View("GroundAddEdit");
        }
        #endregion

        #region Save 
        [HttpPost]
        public IActionResult Save(MST_GroundModel modelMST_Ground)
        {

            if (ModelState.IsValid)
            {
                if (modelMST_Ground.GroundID == null)
                {
                    if (Convert.ToBoolean(dalMST_GroundDALBase.dbo_PR_MST_Ground_Insert(modelMST_Ground)))
                        TempData["successMessage"] = "Record Inserted Successfully";
                    return RedirectToAction("GroundList");
                }
                else
                {
                    if (Convert.ToBoolean(dalMST_GroundDALBase.dbo_PR_MST_Ground_UpdateByPK(modelMST_Ground)))
                        TempData["successMessage"] = "Record Updated Successfully";
                    return RedirectToAction("GroundList");
                }
            }
            TempData["errorMessage"] = "Some error has occurred";
            return RedirectToAction("Add");
        }
        #endregion

        #region Dropdown For State
        public IActionResult StateDropdownByCountry(int CountryID)
        {
            DataTable dtState = dalMST_GroundDALBase.dbo_PR_MST_State_DropdownByCountry(CountryID);

            List<MST_StateDropDownModel> MST_StateDropdownByCountry_List = new List<MST_StateDropDownModel>();
            foreach (DataRow dr in dtState.Rows)
            {
                MST_StateDropDownModel vlst = new MST_StateDropDownModel();
                vlst.StateID = Convert.ToInt32(dr["StateID"]);
                vlst.StateName = dr["StateName"].ToString();
                MST_StateDropdownByCountry_List.Add(vlst);
            }
            var casecade = MST_StateDropdownByCountry_List;
            ViewBag.StateList = MST_StateDropdownByCountry_List;
            return Json(casecade);
        }
        #endregion

        #region Dropdown For City 
        public IActionResult CityDropdownByState(int StateID)
        {
            DataTable dtCity = dalMST_GroundDALBase.dbo_PR_MST_City_DropdownByState(StateID);

            List<MST_CityDropDownModel> MST_CityDropdownByState_List = new List<MST_CityDropDownModel>();
            foreach (DataRow dr in dtCity.Rows)
            {
                MST_CityDropDownModel vlst = new MST_CityDropDownModel();
                vlst.CityID = Convert.ToInt32(dr["CityID"]);
                vlst.CityName = dr["CityName"].ToString();
                MST_CityDropdownByState_List.Add(vlst);
            }
            ViewBag.CityList = MST_CityDropdownByState_List;
            var casecade = MST_CityDropdownByState_List;
            return Json(casecade);
        }
        #endregion

        #region GroundSearch
        public IActionResult GroundSearch(string GroundName, int GroundCapacity)
        {
            DataTable dt = dalMST_GroundDAL.dbo_PR_MST_Ground_Search(GroundName, GroundCapacity);
            return View("GroundList", dt);

        }
        #endregion

    }

}