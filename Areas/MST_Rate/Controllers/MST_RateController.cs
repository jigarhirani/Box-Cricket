using BOXCricket.Areas.MST_Rate.Models;
using BOXCricket.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static BOXCricket.Models.MST_DropDownModel;

namespace BOXCricket.Areas.MST_Rate.Controllers
{
    //[CheckAccess]
    [Area("MST_Rate")]
    [Route("MST_Rate/[controller]/[action]")]
    public class MST_RateController : Controller
    {
        MST_RateDALBase dalMST_RateDALBase = new MST_RateDALBase();
        MST_RateDAL dalMST_RateDAL = new MST_RateDAL();

        public MST_RateController()
        {

        }

        #region Select All 
        public IActionResult RateList()
        {
            #region Dropdown For Slot    

            GetSlots();

            #endregion

            DataTable dt = dalMST_RateDALBase.dbo_PR_MST_Rate_SelectAll();
            return View("RateList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int RateID)
        {
            if (Convert.ToBoolean(dalMST_RateDALBase.dbo_PR_MST_Rate_DeleteByPK(RateID)))
                return RedirectToAction("RateList");
            return View("RateList");
        }

        #endregion

        #region Add/Edit
        public IActionResult Add(int? RateID)
        {
            #region Dropdown For BOXCricket 

            BOXCricketDropDown();

            #endregion

            #region Dropdown For Ground By BOXCricket

            List<MST_GroundDropDownModel> MST_GroundDropdown_List = new List<MST_GroundDropDownModel>();
            ViewBag.GroundList = MST_GroundDropdown_List;

            #endregion

            #region Dropdown For Slot    

            GetSlots();

            #endregion  

            #region Record Select by PK
            if (RateID != null)
            {
                DataTable dt = dalMST_RateDALBase.dbo_PR_MST_Rate_SelectByPK(RateID);
                if (dt.Rows.Count > 0)
                {
                    MST_RateModel model = new MST_RateModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        model.RateID = Convert.ToInt32(dr["RateID"]);
                        model.BOXCricketID = Convert.ToInt32(dr["BOXCricketID"]);
                        model.GroundID = Convert.ToInt32(dr["GroundID"]);
                        model.UserID = Convert.ToInt32(dr["UserID"]);
                        model.DayOfWeek = dr["DayOfWeek"].ToString();
                        model.SlotNO = Convert.ToInt32(dr["SlotNO"]);
                        model.HourlyRate = Convert.ToDecimal(dr["HourlyRate"]);
                    }
                    GroundDropDownByBOXCricket(model.BOXCricketID);
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
                    if (Convert.ToBoolean(dalMST_RateDALBase.dbo_PR_MST_Rate_Insert(modelMST_Rate)))
                        TempData["successMessage"] = "Record Inserted Successfully";
                    return RedirectToAction("RateList");
                }
                else
                {
                    if (Convert.ToBoolean(dalMST_RateDALBase.dbo_PR_MST_Rate_UpdateByPK(modelMST_Rate)))
                        TempData["successMessage"] = "Record Updated Successfully";
                    return RedirectToAction("RateList");
                }
            }
            TempData["errorMessage"] = "Some error has occurred";
            return RedirectToAction("RateList");
        }
        #endregion       

        #region Dropdown For BOXCricketDropDown 
        public void BOXCricketDropDown()
        {
            DataTable dtBOXCricket = dalMST_RateDAL.dbo_PR_MST_BOXCricket_Dropdown();

            List<MST_BOXCricketDropDownModel> MST_BOXCricketDropdown_List = new List<MST_BOXCricketDropDownModel>();
            foreach (DataRow dr in dtBOXCricket.Rows)
            {
                MST_BOXCricketDropDownModel mst_BOXCricketdropdownmodel = new MST_BOXCricketDropDownModel();
                mst_BOXCricketdropdownmodel.BOXCricketID = Convert.ToInt32(dr["BOXCricketID"]);
                mst_BOXCricketdropdownmodel.BOXCricketName = dr["BOXCricketName"].ToString();
                MST_BOXCricketDropdown_List.Add(mst_BOXCricketdropdownmodel);
            }
            ViewBag.BOXCricketList = MST_BOXCricketDropdown_List;
        }
        #endregion

        #region Dropdown For GroundDropDownByBOXCricket    
        public IActionResult GroundDropDownByBOXCricket(int BOXCricketID)
        {
            DataTable dtGround = dalMST_RateDAL.dbo_PR_MST_Ground_DropdownByBOXCricket(BOXCricketID);

            List<MST_GroundDropDownModel> MST_GroundDropdown_List = new List<MST_GroundDropDownModel>();
            foreach (DataRow dr in dtGround.Rows)
            {
                MST_GroundDropDownModel vlst = new MST_GroundDropDownModel();
                vlst.GroundID = Convert.ToInt32(dr["GroundID"]);
                vlst.GroundName = dr["GroundName"].ToString();
                MST_GroundDropdown_List.Add(vlst);
            }
            ViewBag.GroundList = MST_GroundDropdown_List;
            var Ground = MST_GroundDropdown_List;
            return Json(Ground);
        }

        #endregion

        #region Dropdown For Slot 
        public void GetSlots()
        {
            DataTable dtSlot = dalMST_RateDAL.dbo_PR_MST_Slot_Dropdown();

            List<MST_SlotDropDownModel> MST_SlotDropdown_List = new List<MST_SlotDropDownModel>();
            foreach (DataRow dr in dtSlot.Rows)
            {
                MST_SlotDropDownModel mst_Slotdropdownmodel = new MST_SlotDropDownModel();
                mst_Slotdropdownmodel.SlotNO = Convert.ToInt32(dr["SlotNO"]);
                mst_Slotdropdownmodel.SlotDetails = dr["SlotDetails"].ToString();
                MST_SlotDropdown_List.Add(mst_Slotdropdownmodel);
            }
            ViewBag.SlotList = MST_SlotDropdown_List;
        }
        #endregion

        #region RateSearch
        public IActionResult RateSearch(string DayOfWeek, decimal HourlyRate, int SlotNO)
        {
            #region Dropdown For Slot    

            GetSlots();

            #endregion

            DataTable dt = dalMST_RateDAL.dbo_PR_MST_Rate_Search(DayOfWeek, HourlyRate, SlotNO);
            return View("RateList", dt);

        }
        #endregion
    }
}
