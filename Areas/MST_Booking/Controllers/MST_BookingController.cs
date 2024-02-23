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

        #region Select All By Owner
        public IActionResult BookingList()
        {
            ViewBag.IsMyBookingPage = false;

            #region Dropdown For BOXCricket 

            BOXCricketDropDown();

            #endregion

            #region Dropdown For Ground 

            GroundDropDown();

            #endregion

            DataTable dt = dalMST_BookingDALBase.dbo_PR_MST_Booking_SelectAll_ByOwner();
            return View("BookingList", dt);
        }
        #endregion

        #region Select All By UserID
        public IActionResult SelectAllMyBooking()
        {
            ViewBag.IsMyBookingPage = true;

            #region Dropdown For BOXCricket 

            BOXCricketDropDown();

            #endregion

            #region Dropdown For Ground 

            GroundDropDown();

            #endregion

            DataTable dt = dalMST_BookingDALBase.dbo_PR_MST_Booking_SelectAll_ByUserID();
            return View("BookingList", dt);
        }
        #endregion 

        #region Add Booking
        public IActionResult Add(int? BookingID)
        {
            #region Dropdown For BOXCricket 

            BOXCricketDropDown();

            #endregion

            #region Dropdown For Ground By BOXCricket

            List<MST_GroundDropDownModel> MST_GroundDropdown_List = new List<MST_GroundDropDownModel>();
            ViewBag.GroundList = MST_GroundDropdown_List;

            #endregion

            #region Dropdown For Slot    

            List<MST_SlotDropDownModel> MST_SlotDropdown_List = new List<MST_SlotDropDownModel>();
            ViewBag.SlotList = MST_SlotDropdown_List;

            #endregion

            return View("BookingAdd");
        }
        #endregion

        #region Save 
        [HttpPost]
        public IActionResult Save(MST_BookingModel modelMST_Booking, string[] selectedSlots)
        {
            if (ModelState.IsValid)
            {
                // Set the selected slots in the model
                modelMST_Booking.Slots = selectedSlots != null ? string.Join(",", selectedSlots) : null;

                if (modelMST_Booking.BookingID == null)
                {
                    if (Convert.ToBoolean(dalMST_BookingDALBase.dbo_PR_MST_Booking_Insert(modelMST_Booking)))
                        TempData["successMessage"] = "Record Inserted Successfully";
                    return RedirectToAction("BookingList");
                }
            }
            TempData["errorMessage"] = "Some error has occurred";
            return RedirectToAction("BookingList");
        }

        #endregion

        #region BookingStatusEdit
        public IActionResult BookingStatusEdit(int? BookingID)
        {
            #region Record Select by PK for Status Edit
            if (BookingID != null)
            {
                DataTable dt = dalMST_BookingDALBase.dbo_PR_MST_Booking_SelectByPK(BookingID);
                if (dt.Rows.Count > 0)
                {
                    MST_BookingStatusUpdateModel model = new MST_BookingStatusUpdateModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        model.BookingID = Convert.ToInt32(dr["BookingID"]);
                        model.BOXCricketName = Convert.ToString(dr["BOXCricketName"]);
                        model.GroundName = Convert.ToString(dr["GroundName"]);
                        model.BookingDate = Convert.ToDateTime(dr["BookingDate"].ToString());
                        model.BookedBy = Convert.ToString(dr["BookedBy"]);
                        model.SlotDetail = dr["SlotDetails"].ToString();
                        model.BookingAmount = Convert.ToDecimal(dr["BookingAmount"]);
                        model.TotalSlotsBooked = Convert.ToInt32(dr["TotalSlotsBooked"].ToString());
                        model.Status = dr["Status"].ToString();
                        model.Remarks = dr["Remarks"].ToString();
                    }
                    return View("BookingStatusEdit", model);
                }
            }
            #endregion

            return View("BookingList");
        }
        #endregion

        #region BookingStatusSave 
        [HttpPost]
        public IActionResult BookingStatusSave(MST_BookingStatusUpdateModel modelMST_BookingStatusUpdate)
        {
            if (ModelState.IsValid)
            {
                if (Convert.ToBoolean(dalMST_BookingDALBase.dbo_PR_MST_Booking_StatusUpdateByPK(modelMST_BookingStatusUpdate)))
                    TempData["successMessage"] = "Booking Status Updated Successfully";
                return RedirectToAction("BookingList");
            }
            TempData["errorMessage"] = "Some error has occurred";
            return RedirectToAction("BookingList");
        }

        #endregion

        #region Dropdown For BOXCricketDropDown 
        public void BOXCricketDropDown()
        {
            DataTable dtBOXCricket = dalMST_BookingDAL.dbo_PR_MST_BOXCricket_Dropdown_ByUserID();

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

        #region Dropdown For GroundDropDown    
        public IActionResult GroundDropDown()
        {
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
            var Ground = MST_GroundDropdown_List;
            return Json(Ground);
        }

        #endregion

        #region Dropdown For GroundDropDownByBOXCricket    
        public IActionResult GroundDropDownByBOXCricket(int BOXCricketID)
        {
            DataTable dtGround = dalMST_BookingDAL.dbo_PR_MST_Ground_DropdownByBOXCricket(BOXCricketID);

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

        #region Dropdown For GroundAllowedtoBookDropdownByBOXCricket    
        public IActionResult GroundAllowedtoBookDropdownByBOXCricket(int BOXCricketID)
        {
            DataTable dtGround = dalMST_BookingDAL.dbo_PR_MST_Ground_AllowedtoBookDropdown_ByBOXCricket(BOXCricketID);

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
        public IActionResult GetSlots(int GroundID, DateTime BookingDate)
        {
            DataTable dtSlot = dalMST_BookingDAL.dbo_PR_MST_Slot_Dropdown_Validation_ByDate(GroundID, BookingDate);

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

        #region Get Rate 
        public IActionResult Rate(int BOXCricketID, int GroundID, int[] SlotNO, DateTime BookingDate)
        {
            decimal TotalRate = 0;
            foreach (int slot in SlotNO)
            {
                DataTable dtRate = dalMST_BookingDAL.dbo_PR_MST_Rate_HourlyRate_BySlotAndDate(BOXCricketID, GroundID, slot, BookingDate);
                if (dtRate.Rows.Count > 0)
                {
                    TotalRate += Convert.ToDecimal(dtRate.Rows[0]["HourlyRate"]);
                }
            }

            return Json(TotalRate);
        }

        #endregion       

        #region BookingSearch by filters
        public IActionResult BookingSearch(string UserName, int GroundID, int BOXCricketID, string Status)
        {
            #region Dropdown For BOXCricket 

            BOXCricketDropDown();

            #endregion

            #region Dropdown For Ground 

            GroundDropDown();

            #endregion

            if (ViewBag.IsMyBookingPage = false)
            {
                ViewBag.IsMyBookingPage = false;
            }
            else
            {
                ViewBag.IsMyBookingPage = true;
            }

            DataTable dt = dalMST_BookingDAL.dbo_PR_MST_Booking_Search_ByFilters(UserName, GroundID, BOXCricketID, Status);
            return View("BookingList", dt);

        }
        #endregion

    }
}