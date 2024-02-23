using BOXCricket.Areas.MST_Ground.Models;
using BOXCricket.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static BOXCricket.Models.MST_DropDownModel;

namespace BOXCricket.Areas.MST_Ground.Controllers
{
    //[CheckAccess]
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
            BOXCricketDropDown();
            DataTable dt = dalMST_GroundDALBase.dbo_PR_MST_Ground_SelectAll_ByUserID();
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
            #region Dropdown For BOXCricket           
            DataTable dtBOXCricket = dalMST_GroundDAL.dbo_PR_MST_BOXCricket_Dropdown_ByUserID();

            List<MST_BOXCricketDropDownModel> MST_BOXCricketDropdown_List = new List<MST_BOXCricketDropDownModel>();
            foreach (DataRow dr in dtBOXCricket.Rows)
            {
                MST_BOXCricketDropDownModel mst_BOXCricketdropdownmodel = new MST_BOXCricketDropDownModel();
                mst_BOXCricketdropdownmodel.BOXCricketID = Convert.ToInt32(dr["BOXCricketID"]);
                mst_BOXCricketdropdownmodel.BOXCricketName = dr["BOXCricketName"].ToString();
                MST_BOXCricketDropdown_List.Add(mst_BOXCricketdropdownmodel);
            }
            ViewBag.BOXCricketList = MST_BOXCricketDropdown_List;
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
                        model.GroundNickName = dr["GroundNickName"].ToString();
                        model.BOXCricketID = Convert.ToInt32(dr["BOXCricketID"]);
                        model.UserID = Convert.ToInt32(dr["UserID"]);
                        model.GroundCapacity = Convert.ToInt32(dr["GroundCapacity"]);
                        model.GroundHeight = Convert.ToDecimal(dr["GroundHeight"]);
                        model.GroundWidth = Convert.ToDecimal(dr["GroundWidth"]);
                        model.GroundLength = Convert.ToDecimal(dr["GroundWidth"]);
                        model.ContactPersonName = dr["ContactPersonName"].ToString();
                        model.ContactPersonNumber = dr["ContactPersonNumber"].ToString();
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

        #region Dropdown For BOXCricketDropDown 
        public void BOXCricketDropDown()
        {
            DataTable dtBOXCricket = dalMST_GroundDAL.dbo_PR_MST_BOXCricket_Dropdown_ByUserID();

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

        #region GroundSearch
        public IActionResult GroundSearch(string GroundName, int GroundCapacity, string IsAllowedBooking, int BOXCricketID)
        {
            #region Dropdown For BOXCricket 

            BOXCricketDropDown();

            #endregion

            DataTable dt = dalMST_GroundDAL.dbo_PR_MST_Ground_Search_ByFilter(GroundName, GroundCapacity, IsAllowedBooking, BOXCricketID);
            return View("GroundList", dt);

        }
        #endregion

    }

}