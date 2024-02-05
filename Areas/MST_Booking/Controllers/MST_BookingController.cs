using BOXCricket.Areas.MST_Booking.Models;
using BOXCricket.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static BOXCricket.Models.MST_DropDownModel;

namespace BOXCricket.Areas.MST_Booking.Controllers
{
    //[CheckAccess]
    [Area("MST_Booking")]
    [Route("MST_Booking/[controller]/[action]")]
    public class MST_BookingController : Controller
    {
        MST_BookingDALBase dalMST_BookingDALBase = new MST_BookingDALBase();

        MST_BookingDAL dalMST_BookingDAL = new MST_BookingDAL();
        public MST_BookingController()
        {


        }

        #region Select All 
        public IActionResult BookingList()
        {
            #region Dropdown For Ground           
            DataTable dtGround = dalMST_BookingDAL.dbo_PR_MST_Ground_Dropdown();

            List<MST_GroundDropDownModel> MST_GroundDropdown_List = new List<MST_GroundDropDownModel>();
            foreach (DataRow dr in dtGround.Rows)
            {
                MST_GroundDropDownModel mst_Grounddropdownmodel = new MST_GroundDropDownModel();
                mst_Grounddropdownmodel.GroundID = Convert.ToInt32(dr["GroundID"]);
                mst_Grounddropdownmodel.GroundName = dr["GroundName"].ToString();
                MST_GroundDropdown_List.Add(mst_Grounddropdownmodel);
            }
            ViewBag.GroundList = MST_GroundDropdown_List;
            #endregion

            DataTable dt = dalMST_BookingDALBase.dbo_PR_MST_Booking_SelectAll();
            return View("BookingList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int BookingID)
        {
            if (Convert.ToBoolean(dalMST_BookingDALBase.dbo_PR_MST_Booking_DeleteByPK(BookingID)))
                return RedirectToAction("BookingList");
            return View("BookingList");
        }

        #endregion

        #region Add/Edit
        public IActionResult Add(int? BookingID)
        {
            #region Dropdown For Ground           
            DataTable dtGround = dalMST_BookingDAL.dbo_PR_MST_Ground_Dropdown();

            List<MST_GroundDropDownModel> MST_GroundDropdown_List = new List<MST_GroundDropDownModel>();
            foreach (DataRow dr in dtGround.Rows)
            {
                MST_GroundDropDownModel mst_Grounddropdownmodel = new MST_GroundDropDownModel();
                mst_Grounddropdownmodel.GroundID = Convert.ToInt32(dr["GroundID"]);
                mst_Grounddropdownmodel.GroundName = dr["GroundName"].ToString();
                MST_GroundDropdown_List.Add(mst_Grounddropdownmodel);
            }
            ViewBag.GroundList = MST_GroundDropdown_List;
            #endregion

            #region Dropdown For Slot    

            List<MST_SlotDropDownModel> MST_SlotDropdown_List = new List<MST_SlotDropDownModel>();
            ViewBag.SlotList = MST_SlotDropdown_List;

            #endregion  

            #region Record Select by PK
            if (BookingID != null)
            {
                DataTable dt = dalMST_BookingDALBase.dbo_PR_MST_Booking_SelectByPK(BookingID);
                if (dt.Rows.Count > 0)
                {
                    MST_BookingModel model = new MST_BookingModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        model.BookingID = Convert.ToInt32(dr["BookingID"]);
                        model.GroundID = Convert.ToInt32(dr["GroundID"]);
                        model.BookedBy = Convert.ToInt32(dr["BookedBy"]);
                        model.BookingDate = Convert.ToDateTime(dr["BookingDate"].ToString());
                        model.SlotNO = Convert.ToInt32(dr["SlotNO"]);
                        model.Status = dr["Status"].ToString();
                    }
                    //GetSlots(model.GroundID, model.BookingDate);
                    return View("BookingAddEdit", model);
                }
            }
            #endregion           

            return View("BookingAddEdit");
        }
        #endregion

        #region Save 
        [HttpPost]
        public IActionResult Save(MST_BookingModel modelMST_Booking)
        {

            if (ModelState.IsValid)
            {
                if (modelMST_Booking.BookingID == null)
                {
                    if (Convert.ToBoolean(dalMST_BookingDALBase.dbo_PR_MST_Booking_Insert(modelMST_Booking)))
                        TempData["successMessage"] = "Record Inserted Successfully";
                    return RedirectToAction("BookingList");
                }
                else
                {
                    if (Convert.ToBoolean(dalMST_BookingDALBase.dbo_PR_MST_Booking_UpdateByPK(modelMST_Booking)))
                        TempData["successMessage"] = "Record Updated Successfully";
                    return RedirectToAction("BookingList");
                }
            }
            TempData["errorMessage"] = "Some error has occurred";
            return RedirectToAction("BookingList");
        }
        #endregion       

        #region Dropdown For Slot 
        public IActionResult GetSlots(int GroundID, DateTime BookingDate)
        {
            DataTable dtSlot = dalMST_BookingDAL.dbo_PR_MST_Slot_Dropdown_Validation(GroundID, BookingDate);

            List<MST_SlotDropDownModel> MST_SlotDropdown_List = new List<MST_SlotDropDownModel>();
            foreach (DataRow dr in dtSlot.Rows)
            {
                MST_SlotDropDownModel mst_Slotdropdownmodel = new MST_SlotDropDownModel();
                mst_Slotdropdownmodel.SlotNO = Convert.ToInt32(dr["SlotNO"]);
                mst_Slotdropdownmodel.SlotDetails = dr["SlotDetails"].ToString();
                MST_SlotDropdown_List.Add(mst_Slotdropdownmodel);
            }
            ViewBag.SlotList = MST_SlotDropdown_List;
            var Slots = MST_SlotDropdown_List;
            return Json(Slots);
        }
        #endregion

        #region BookingSearch
        public IActionResult BookingSearch(string UserName, int GroundID)
        {
            #region Dropdown For Ground           
            DataTable dtGround = dalMST_BookingDAL.dbo_PR_MST_Ground_Dropdown();

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

            DataTable dt = dalMST_BookingDAL.dbo_PR_MST_Booking_Search(UserName, GroundID);
            return View("BookingList", dt);

        }
        #endregion
    }
}
