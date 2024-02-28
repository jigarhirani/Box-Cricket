using BOXCricket.BAL;
using BOXCricket.DAL;
using BOXCricket.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using static BOXCricket.Models.MST_DropDownModel;
namespace BOXCricket.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        HomeDALBase dalHomeDALBase = new HomeDALBase();
        HomeDAL dalHomeDAL = new HomeDAL();

        #region Select All BOX Crickets With Allowed to book Grounds
        public IActionResult Index()
        {
            DataTable dt = dalHomeDALBase.dbo_PR_MST_BOXCricket_SelectAll();
            return View("Index", dt);
        }
        #endregion

        #region Select All Allowed to book Grounds
        public IActionResult Grounds(int? BOXCricketID)
        {
            if (BOXCricketID.HasValue)
            {
                DataTable dt = dalHomeDAL.dbo_PR_MST_Ground_SelectAll_ByBoxCricketID(BOXCricketID);
                return View("Grounds", dt);
            }
            else
            {
                DataTable dt = dalHomeDALBase.dbo_PR_MST_Ground_SelectAll();
                return View("Grounds", dt);
            }
        }
        #endregion

        [CheckAccess]
        #region Add Booking
        public IActionResult BookingAdd(int GroundID, int BOXCricketID)
        {
            #region Dropdown For Slot    

            List<MST_SlotDropDownModel> MST_SlotDropdown_List = new List<MST_SlotDropDownModel>();
            ViewBag.SlotList = MST_SlotDropdown_List;

            #endregion

            return View("UserBookingAdd");
        }
        #endregion

        [CheckAccess]
        #region Save 
        [HttpPost]
        public IActionResult Save(BookingModel modelBooking, string[] selectedSlots)
        {
            if (ModelState.IsValid)
            {
                // Set the selected slots in the model
                modelBooking.Slots = selectedSlots != null ? string.Join(",", selectedSlots) : null;

                if (modelBooking.BookingID == null)
                {
                    if (Convert.ToBoolean(dalHomeDALBase.dbo_PR_MST_Booking_Insert(modelBooking)))
                        TempData["successMessage"] = "Record Inserted Successfully";
                    return RedirectToAction("Index");
                }
            }
            TempData["errorMessage"] = "Some error has occurred";
            return RedirectToAction("Index");
        }
        #endregion

        [CheckAccess]
        #region Select All My Booking
        public IActionResult SelectAllMyBooking()
        {
            DataTable dt = dalHomeDAL.dbo_PR_MST_Booking_SelectAll_ByUserID();
            return View("UserBookingList", dt);
        }
        #endregion 

        #region Dropdown For Slot 
        public IActionResult GetSlots(int GroundID, DateTime BookingDate)
        {
            DataTable dtSlot = dalHomeDAL.dbo_PR_MST_Slot_Dropdown_Validation_ByDate(GroundID, BookingDate);

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
                DataTable dtRate = dalHomeDAL.dbo_PR_MST_Rate_HourlyRate_BySlotAndDate(BOXCricketID, GroundID, slot, BookingDate);
                if (dtRate.Rows.Count > 0)
                {
                    TotalRate += Convert.ToDecimal(dtRate.Rows[0]["HourlyRate"]);
                }
            }

            return Json(TotalRate);
        }

        #endregion

        #region AdminIndex Page load
        public IActionResult AdminIndex()
        {
            ViewBag.UserID = HttpContext.Session.GetString("UserID");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.ProfilePhotoPath = HttpContext.Session.GetString("ProfilePhotoPath");

            #region Dashboard Count
            DataTable dtcounts = dalHomeDAL.dbo_PR_Dashboard_Counts();
            #endregion

            return View("AdminIndex", dtcounts);
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}