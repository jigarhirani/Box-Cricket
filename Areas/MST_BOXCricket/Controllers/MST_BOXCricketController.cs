using BOXCricket.Areas.MST_BOXCricket.Models;
using BOXCricket.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static BOXCricket.Models.LOC_DropDownModel;

namespace BOXCricket.Areas.MST_BOXCricket.Controllers
{
    //[CheckAccess]
    [Area("MST_BOXCricket")]
    [Route("MST_BOXCricket/[controller]/[action]")]
    public class MST_BOXCricketController : Controller
    {
        MST_BOXCricketDALBase dalMST_BOXCricketDALBase = new MST_BOXCricketDALBase();
        MST_BOXCricketDAL dalMST_BOXCricketDAL = new MST_BOXCricketDAL();

        public MST_BOXCricketController()
        {

        }

        #region Select All 
        public IActionResult BOXCricketList()
        {
            DataTable dt = dalMST_BOXCricketDALBase.dbo_PR_MST_BOXCricket_SelectAll();
            return View("BOXCricketList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int BOXCricketID)
        {
            if (Convert.ToBoolean(dalMST_BOXCricketDALBase.dbo_PR_MST_BOXCricket_DeleteByPK(BOXCricketID)))
                return RedirectToAction("BOXCricketList");
            return View("BOXCricketList");
        }

        #endregion

        #region Add/Edit
        public IActionResult Add(int? BOXCricketID)
        {

            #region Dropdown For Country           
            DataTable dtCountry = dalMST_BOXCricketDAL.dbo_PR_LOC_Country_SelectByDropdownList();

            List<LOC_CountryDropDownModel> LOC_CountryDropdown_List = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr in dtCountry.Rows)
            {
                LOC_CountryDropDownModel vlst = new LOC_CountryDropDownModel();
                vlst.CountryID = Convert.ToInt32(dr["CountryID"]);
                vlst.CountryName = dr["CountryName"].ToString();
                LOC_CountryDropdown_List.Add(vlst);
            }
            ViewBag.CountryList = LOC_CountryDropdown_List;
            #endregion

            #region Dropdown For State

            List<LOC_StateDropDownModel> LOC_StateDropdown_List = new List<LOC_StateDropDownModel>();
            ViewBag.StateList = LOC_StateDropdown_List;

            #endregion

            #region Dropdown For City 

            List<LOC_CityDropDownModel> LOC_CityDropdown_List = new List<LOC_CityDropDownModel>();
            ViewBag.CityList = LOC_CityDropdown_List;

            #endregion

            #region Record Select by PK
            if (BOXCricketID != null)
            {
                DataTable dt = dalMST_BOXCricketDALBase.dbo_PR_MST_BOXCricket_SelectByPK(BOXCricketID);
                if (dt.Rows.Count > 0)
                {
                    MST_BOXCricketModel model = new MST_BOXCricketModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        model.BOXCricketID = Convert.ToInt32(dr["BOXCricketID"]);
                        model.BOXCricketName = dr["BOXCricketName"].ToString();
                        model.Address = dr["Address"].ToString();
                        model.CountryID = Convert.ToInt32(dr["CountryID"]);
                        model.StateID = Convert.ToInt32(dr["StateID"]);
                        model.CityID = Convert.ToInt32(dr["CityID"]);
                    }
                    StateDropdownByCountry(model.CountryID);
                    CityDropdownByState(model.StateID);
                    return View("BOXCricketAddEdit", model);
                }
            }
            #endregion           

            return View("BOXCricketAddEdit");
        }
        #endregion

        #region Save 
        [HttpPost]
        public IActionResult Save(MST_BOXCricketModel modelMST_BOXCricket)
        {

            if (ModelState.IsValid)
            {
                if (modelMST_BOXCricket.BOXCricketID == null)
                {
                    if (Convert.ToBoolean(dalMST_BOXCricketDALBase.dbo_PR_MST_BOXCricket_Insert(modelMST_BOXCricket)))
                        TempData["successMessage"] = "Record Inserted Successfully";
                    return RedirectToAction("BOXCricketList");
                }
                else
                {
                    if (Convert.ToBoolean(dalMST_BOXCricketDALBase.dbo_PR_MST_BOXCricket_UpdateByPK(modelMST_BOXCricket)))
                        TempData["successMessage"] = "Record Updated Successfully";
                    return RedirectToAction("BOXCricketList");
                }
            }
            TempData["errorMessage"] = "Some error has occurred";
            return RedirectToAction("Add");
        }
        #endregion

        #region Dropdown For State
        public IActionResult StateDropdownByCountry(int CountryID)
        {
            DataTable dtState = dalMST_BOXCricketDAL.dbo_PR_LOC_State_DropdownByCountry(CountryID);

            List<LOC_StateDropDownModel> LOC_StateDropdownByCountry_List = new List<LOC_StateDropDownModel>();
            foreach (DataRow dr in dtState.Rows)
            {
                LOC_StateDropDownModel vlst = new LOC_StateDropDownModel();
                vlst.StateID = Convert.ToInt32(dr["StateID"]);
                vlst.StateName = dr["StateName"].ToString();
                LOC_StateDropdownByCountry_List.Add(vlst);
            }
            var casecade = LOC_StateDropdownByCountry_List;
            ViewBag.StateList = LOC_StateDropdownByCountry_List;
            return Json(casecade);
        }
        #endregion

        #region Dropdown For City 
        public IActionResult CityDropdownByState(int StateID)
        {
            DataTable dtCity = dalMST_BOXCricketDAL.dbo_PR_LOC_City_DropdownByState(StateID);

            List<LOC_CityDropDownModel> LOC_CityDropdownByState_List = new List<LOC_CityDropDownModel>();
            foreach (DataRow dr in dtCity.Rows)
            {
                LOC_CityDropDownModel vlst = new LOC_CityDropDownModel();
                vlst.CityID = Convert.ToInt32(dr["CityID"]);
                vlst.CityName = dr["CityName"].ToString();
                LOC_CityDropdownByState_List.Add(vlst);
            }
            ViewBag.CityList = LOC_CityDropdownByState_List;
            var casecade = LOC_CityDropdownByState_List;
            return Json(casecade);
        }
        #endregion

        #region BOXCricketSearch
        public IActionResult BOXCricketSearch(string BOXCricketName)
        {
            DataTable dt = dalMST_BOXCricketDAL.dbo_PR_MST_BOXCricket_Search(BOXCricketName);
            return View("BOXCricketList", dt);

        }
        #endregion
    }
}
