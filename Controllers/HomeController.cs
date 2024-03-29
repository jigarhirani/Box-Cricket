using BOXCricket.BAL;
using BOXCricket.DAL;
using BOXCricket.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using static BOXCricket.Models.LOC_DropDownModel;
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
            #region Load Dropdown For City 
            CityDropdown();
            #endregion

            DataTable dt = dalHomeDALBase.dbo_PR_MST_BOXCricket_SelectAll();
            return View("Index", dt);
        }
        #endregion

        #region Select All Allowed to book Grounds
        public IActionResult Grounds(int? BOXCricketID)
        {
            #region Load Dropdown For Slots 
            GetAllSlots();
            #endregion
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

        #region Booking Add Page Methods

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
                        TempData["successMessage"] = "Record Inserted Successfully.";
                    return RedirectToAction("Index");
                }
            }
            TempData["errorMessage"] = "Some error has occurred!";
            return RedirectToAction("Index");
        }
        #endregion

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
        public IActionResult GetAllSlots()
        {
            DataTable dtSlot = dalHomeDAL.dbo_PR_MST_Slot_Dropdown();

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

        #region Dropdown For Slot Validation By Date
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

        #region Dropdown For City 
        public IActionResult CityDropdown()
        {
            DataTable dtCity = dalHomeDAL.dbo_PR_LOC_City_SelectDropDownList();

            List<LOC_CityDropDownModel> LOC_CityDropdownByState_List = new List<LOC_CityDropDownModel>();
            foreach (DataRow dr in dtCity.Rows)
            {
                LOC_CityDropDownModel loc_citydropdownmodel = new LOC_CityDropDownModel();
                loc_citydropdownmodel.CityID = Convert.ToInt32(dr["CityID"]);
                loc_citydropdownmodel.CityName = dr["CityName"].ToString();
                LOC_CityDropdownByState_List.Add(loc_citydropdownmodel);
            }
            ViewBag.CityList = LOC_CityDropdownByState_List;
            var casecade = LOC_CityDropdownByState_List;
            return Json(casecade);
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

        #region BOXCricket Filter
        public IActionResult BOXCricketFilter(int CityID, decimal HourlyRate)
        {
            #region Load Dropdown For City 
            CityDropdown();
            #endregion

            DataTable dt = dalHomeDAL.dbo_PR_MST_BOXCricket_ByFilters(CityID, HourlyRate);
            return View("Index", dt);
        }
        #endregion

        #region Ground Filter
        public IActionResult GroundFilter(int GroundCapacity, decimal PerHourPrice)
        {
            DataTable dt = dalHomeDAL.dbo_PR_MST_Ground_ByFilters(GroundCapacity, PerHourPrice);
            return View("Grounds", dt);
        }
        #endregion

        #region AdminIndex Page Load Method

        #region GO TO AdminIndex Page
        [CheckAccess]
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

        #region GetBookingsData for chart
        [HttpGet]
        public List<object> GetBookingsData(string? selectedBookingTrendValue)
        {
            DataTable dt = dalHomeDAL.dbo_PR_GetBookingCountsByWeekdays(selectedBookingTrendValue);

            List<object> data = new List<object>();

            List<string> weekdays = new List<string>();
            List<int> bookings = new List<int>();

            foreach (DataRow dr in dt.Rows)
            {
                weekdays.Add(Convert.ToString(dr["Weekday"]));
                bookings.Add(Convert.ToInt32(dr["TotalBookings"]));
            }

            data.Add(weekdays);
            data.Add(bookings);

            return data;
        }
        #endregion

        #region GetRevenueData for chart
        [HttpGet]
        public List<object> GetRevenueData(string? selectedRevenueTrendValue)
        {
            DataTable dt = dalHomeDAL.dbo_PR_GetRevenueByPeriod(selectedRevenueTrendValue);

            List<object> data = new List<object>();

            List<string> dates = new List<string>();
            List<decimal> revenue = new List<decimal>();

            foreach (DataRow dr in dt.Rows)
            {
                dates.Add(((DateTime)dr["BookingDate"]).ToString("dd-MM-yyyy"));
                revenue.Add(Convert.ToDecimal(dr["TotalRevenue"]));
            }

            data.Add(dates);
            data.Add(revenue);

            return data;
        }
        #endregion

        #region Get Top Customers
        [HttpGet]
        public List<object> GetTopCustomersList(string? selectedTopCustomersValue)
        {
            DataTable dt = dalHomeDAL.dbo_PR_GetTopUserDetailsByPeriod(selectedTopCustomersValue);

            List<object> userList = new List<object>();

            foreach (DataRow dr in dt.Rows)
            {
                var user = new
                {
                    UserName = dr["UserName"].ToString(),
                    Contact = dr["Contact"].ToString(),
                    ProfilePhotoPath = dr["ProfilePhotoPath"].ToString(),
                    Email = dr["Email"].ToString(),
                    BookingCount = Convert.ToInt32(dr["BookingCount"])
                };

                userList.Add(user);
            }
            return userList;
        }

        #endregion

        #endregion

        #region Exception Heandling

        #region Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

        #region Page Not Found
        public IActionResult PageNotFound()
        {
            return View("PageNotFound");
        }
        #endregion

        #endregion
    }
}