using BOXCricket.Areas.MST_User.Models;
using BOXCricket.BAL;
using BOXCricket.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static BOXCricket.Models.LOC_DropDownModel;


namespace BOXCricket.Areas.MST_User.Controllers
{
    [Area("MST_User")]
    [Route("MST_User/[controller]/[action]")]
    public class MST_UserController : Controller
    {
        MST_UserDALBase dalMST_UserDALBase = new MST_UserDALBase();
        MST_UserDAL dalMST_UserDAL = new MST_UserDAL();


        #region Config

        private IConfiguration Configuration;
        public MST_UserController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #endregion

        #region Go To Login Page
        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }
        #endregion

        #region Login

        [HttpPost]
        public IActionResult Login(MST_UserModel modelMST_User)
        {
            string connstr = this.Configuration.GetConnectionString("MyConnection");
            string error = null;
            if (modelMST_User.Email == null)
            {
                error += "Email Address is required";
            }
            if (modelMST_User.Password == null)
            {
                error += "<br/>Password is required";
            }

            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("Login", "MST_User");
            }
            else
            {
                DataTable dt = dalMST_UserDAL.dbo_PR_MST_User_Select_ByEmailPassword(connstr, modelMST_User.Email, modelMST_User.Password);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        HttpContext.Session.SetString("Email", dr["Email"].ToString());
                        HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                        HttpContext.Session.SetString("Password", dr["Password"].ToString());
                        HttpContext.Session.SetString("IsAdmin", dr["IsAdmin"].ToString());
                        HttpContext.Session.SetString("ProfilePhotoPath", dr["ProfilePhotoPath"].ToString());
                        break;
                    }
                }
                else
                {
                    TempData["Error"] = "User Name or Password is invalid!";
                    return RedirectToAction("Login", "MST_User");
                }
                if (HttpContext.Session.GetString("Email") != null && HttpContext.Session.GetString("Password") != null)
                {
                    if (CommonVariables.IsAdmin() == false)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("AdminIndex", "Home");
                    }
                }
            }
            return RedirectToAction("Login", "MST_User");
        }
        #endregion

        #region Go to Sign Up Page
        public IActionResult SingUP()
        {
            #region Dropdown For Country           
            DataTable dtCountry = dalMST_UserDAL.dbo_PR_LOC_Country_SelectByDropdownList();

            List<LOC_CountryDropDownModel> LOC_CountryDropdown_List = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr in dtCountry.Rows)
            {
                LOC_CountryDropDownModel loc_countrydropdownmodel = new LOC_CountryDropDownModel();
                loc_countrydropdownmodel.CountryID = Convert.ToInt32(dr["CountryID"]);
                loc_countrydropdownmodel.CountryName = dr["CountryName"].ToString();
                LOC_CountryDropdown_List.Add(loc_countrydropdownmodel);
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

            return View();
        }
        #endregion

        #region Profle
        public IActionResult Profile()
        {

            #region Dropdown For Country           
            DataTable dtCountry = dalMST_UserDAL.dbo_PR_LOC_Country_SelectByDropdownList();

            List<LOC_CountryDropDownModel> LOC_CountryDropdown_List = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr in dtCountry.Rows)
            {
                LOC_CountryDropDownModel loc_countrydropdownmodel = new LOC_CountryDropDownModel();
                loc_countrydropdownmodel.CountryID = Convert.ToInt32(dr["CountryID"]);
                loc_countrydropdownmodel.CountryName = dr["CountryName"].ToString();
                LOC_CountryDropdown_List.Add(loc_countrydropdownmodel);
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

            DataTable dt = dalMST_UserDALBase.dbo_PR_MST_User_SelectByPK(Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            if (dt.Rows.Count > 0)
            {
                MST_UserModel model = new MST_UserModel();
                foreach (DataRow dr in dt.Rows)
                {
                    model.UserID = Convert.ToInt32(dr["UserID"]);
                    model.FirstName = dr["FirstName"].ToString();
                    model.LastName = dr["LastName"].ToString();
                    model.Email = dr["Email"].ToString();
                    model.Contact = dr["Contact"].ToString();
                    model.CountryID = Convert.ToInt32(dr["CountryID"].ToString());
                    model.StateID = Convert.ToInt32(dr["StateID"].ToString());
                    model.CityID = Convert.ToInt32(dr["CityID"].ToString());
                    model.ProfilePhotoPath = dr["ProfilePhotoPath"].ToString();
                    ViewBag.EditImagePath = Convert.ToString(dr["ProfilePhotoPath"]);
                }
                StateDropdownByCountry(model.CountryID);
                CityDropdownByState(model.StateID);
                return View("Profile", model);
            }
            return RedirectToAction("AdminIndex", "Home");
        }
        #endregion

        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(MST_UserModel modelMST_User)
        {
            #region File section
            if (modelMST_User.File != null)
            {
                string FilePath = "wwwroot\\UploadPhoto";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileNameWithPath = Path.Combine(path, modelMST_User.File.FileName);

                modelMST_User.ProfilePhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelMST_User.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelMST_User.File.CopyTo(stream);
                }
            }
            #endregion

            string str = Configuration.GetConnectionString("MyConnection");

            bool result = false;

            if (modelMST_User.UserID == null)
            {
                result = dalMST_UserDALBase.dbo_PR_MST_User_Insert(str, modelMST_User.FirstName, modelMST_User.LastName, modelMST_User.Password, modelMST_User.Email, modelMST_User.Contact, modelMST_User.ProfilePhotoPath, modelMST_User.CountryID, modelMST_User.StateID, modelMST_User.CityID, null, null);
            }
            else
            {
                result = dalMST_UserDALBase.dbo_PR_MST_User_UpdateByPK(str, modelMST_User.FirstName, modelMST_User.LastName, modelMST_User.Password, modelMST_User.Email, modelMST_User.Contact, modelMST_User.CountryID, modelMST_User.StateID, modelMST_User.CityID, modelMST_User.ProfilePhotoPath, null, Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            }

            if (result)
            {
                TempData["alertClass"] = "alert alert-success";
                TempData["alertDisplay"] = "show";
                if (modelMST_User.UserID == null)
                {
                    TempData["alertTitle"] = "Inserted";
                    TempData["alertMessage"] = "Data Inserted Successfully";
                }
                else
                {
                    TempData["alertTitle"] = "Updated";
                    TempData["alertMessage"] = "Data Updated Successfully";
                }
            }
            else
            {
                TempData["alertDisplay"] = "show";
                TempData["alertClass"] = "alert alert-danger";
                TempData["alertTitle"] = "Error";
                TempData["alertMessage"] = "Some Error Occured";
            }
            if (CommonVariables.IsAdmin() == false)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("AdminIndex", "Home");
            }
        }
        #endregion        

        #region Change Password Page
        public IActionResult ChangePassword()
        {
            return View();
        }
        #endregion 

        #region SavePassword
        [HttpPost]
        public IActionResult SavePassword(MST_User_PasswrodChange modelMST_User_PasswordChange)
        {
            if (ModelState.IsValid)
            {
                if (Convert.ToBoolean(dalMST_UserDAL.dbo_PR_MST_User_Password_UpdateByPK(modelMST_User_PasswordChange)))
                    TempData["successMessage"] = "Password Updated Successfully";

                if (CommonVariables.IsAdmin() == false)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("AdminIndex", "Home");
                }
            }
            TempData["errorMessage"] = "Some error has occurred";
            return RedirectToAction("ChangePassword");
        }
        #endregion      

        #region Go to Forgot Password Page
        public IActionResult ForgotPassword()
        {
            return View();
        }
        #endregion

        #region Dropdown For State
        public IActionResult StateDropdownByCountry(int CountryID)
        {
            DataTable dtState = dalMST_UserDAL.dbo_PR_LOC_State_DropdownByCountry(CountryID);

            List<LOC_StateDropDownModel> LOC_StateDropdownByCountry_List = new List<LOC_StateDropDownModel>();
            foreach (DataRow dr in dtState.Rows)
            {
                LOC_StateDropDownModel loc_statedropdownmodel = new LOC_StateDropDownModel();
                loc_statedropdownmodel.StateID = Convert.ToInt32(dr["StateID"]);
                loc_statedropdownmodel.StateName = dr["StateName"].ToString();
                LOC_StateDropdownByCountry_List.Add(loc_statedropdownmodel);
            }
            var casecade = LOC_StateDropdownByCountry_List;
            ViewBag.StateList = LOC_StateDropdownByCountry_List;
            return Json(casecade);
        }
        #endregion

        #region Dropdown For City 
        public IActionResult CityDropdownByState(int StateID)
        {
            DataTable dtCity = dalMST_UserDAL.dbo_PR_LOC_City_DropdownByState(StateID);

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

        #region Logout
        //Logout action to clear current session and redirect user to login page
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        #endregion                       
    }

}