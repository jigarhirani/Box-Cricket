using BOXCricket.Areas.MST_User.Models;
using BOXCricket.BAL;
using BOXCricket.CF;
using BOXCricket.DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
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
        private IWebHostEnvironment webHostEnvironment;

        public MST_UserController(IConfiguration _configuration, IWebHostEnvironment _webHostEnvironment)
        {
            Configuration = _configuration;
            webHostEnvironment = _webHostEnvironment;
        }
        #endregion

        #region Login Page Methods

        #region Go To Login Page
        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }
        #endregion

        #region GO TO Google Login Page
        public async Task LoginWithGoogle()
        {
            HttpContext.Session.Clear();
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("LoginUsingGoogle")
                });
        }
        #endregion

        #region Login Using Google
        public async Task<IActionResult> LoginUsingGoogle()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var emailClaim = result.Principal.FindFirst(ClaimTypes.Email);
            var Email = emailClaim?.Value.ToString();

            if (Email != null)
            {
                string connstr = this.Configuration.GetConnectionString("MyConnection");
                DataTable dt = dalMST_UserDAL.dbo_PR_MST_User_Select_ByEmail(connstr, Email);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        HttpContext.Session.SetString("Email", dr["Email"].ToString());
                        HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                        HttpContext.Session.SetString("Password", dr["Password"].ToString());
                        HttpContext.Session.SetString("IsAdmin", dr["IsAdmin"].ToString());
                        HttpContext.Session.SetString("IsEmailConfirmed", dr["IsEmailConfirmed"].ToString());
                        HttpContext.Session.SetString("ProfilePhotoPath", dr["ProfilePhotoPath"].ToString());
                        break;
                    }
                }
                else
                {
                    TempData["errorMessage"] = "User Account Not Found SignUp!";
                    return RedirectToAction("Login", "MST_User");
                }
                if (HttpContext.Session.GetString("Email") != null && HttpContext.Session.GetString("Password") != null)
                {
                    if (CommonVariables.IsEmailConfirmed() == true)
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
                    else
                    {
                        TempData["emailverificationMessage"] = "Email is not verified try after verifying email!";
                    }

                }
            }
            return RedirectToAction("Login", "MST_User");
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
                        HttpContext.Session.SetString("IsEmailConfirmed", dr["IsEmailConfirmed"].ToString());
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
                    if (CommonVariables.IsEmailConfirmed() == true)
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
                    else
                    {
                        TempData["emailverificationMessage"] = "Email is not verified try after verifying email";
                    }

                }
            }
            return RedirectToAction("Login", "MST_User");
        }
        #endregion

        #endregion

        #region SignUp Page Mathods

        #region Go to Sign Up Page
        public IActionResult SignUp()
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

        #region GO TO Google Signup Page
        //public async Task SignUpWithGoogle()
        //{
        //    HttpContext.Session.Clear();
        //    await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
        //        new AuthenticationProperties
        //        {
        //            RedirectUri = Url.Action("SignUpUsingGoogle")
        //        });
        //}
        #endregion

        #region SignUp Using Google
        //public async Task<IActionResult> SignUpUsingGoogle()
        //{
        //    var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    var emailClaim = result.Principal.FindFirst(ClaimTypes.Email);
        //    var Email = emailClaim?.Value.ToString();
        //    var firstNameClaim = result.Principal.FindFirst(ClaimTypes.Name);
        //    var FirstName = firstNameClaim?.Value.ToString();
        //    var lastNameClaim = result.Principal.FindFirst(ClaimTypes.Surname);
        //    var LastName = lastNameClaim?.Value.ToString();
        //    //var profileImageClaim = result.Principal.FindFirst("urn:google:picture");
        //    //var ProfileImage = profileImageClaim?.Value.ToString();

        //    if (Email != null)
        //    {
        //        string connstr = this.Configuration.GetConnectionString("MyConnection");
        //        DataTable dt = dalMST_UserDAL.dbo_PR_MST_User_Select_ByEmail(connstr, Email);
        //        if (dt.Rows.Count > 0)
        //        {
        //            TempData["errorMessage"] = "Email Account already exists!";
        //            return RedirectToAction("Login", "MST_User");
        //        }
        //        else
        //        {
        //            //await Save(new MST_UserModel
        //            //{
        //            //    Email = Email,
        //            //    FirstName = FirstName,
        //            //    LastName = LastName,
        //            //    ProfilePhotoPath = ProfileImage,
        //            //    Password = Guid.NewGuid().ToString(),
        //            //});
        //        }
        //    }
        //    return RedirectToAction("Login", "MST_User");
        //}
        #endregion

        #region Send Confirmation Email
        public IActionResult SendConfirmationEmail(string Email, string token)
        {
            try
            {
                var confirmationLink = Url.Action("ConfirmEmail", "MST_User", new { email = Email, token = token }, Request.Scheme);
                string subject = "Confirm your email address";
                string message = System.IO.File.ReadAllText("wwwRoot/assets/resources/EmailConfirmationMail.html");
                message = message.Replace("{{confirmationLink}}", confirmationLink);
                CommonFunctions.SendEmail(Email, subject, message);

                TempData["successMessage"] = "Confirmation email sent successfully.";
                return RedirectToAction("Login", "MST_User");
            }
            catch (Exception)
            {
                TempData["errorMessage"] = "Some error has occurred!";
                return RedirectToAction("Login", "MST_User");
            }
        }
        #endregion

        #region Validation of email and Token (Send Welcome Mail)
        public IActionResult ConfirmEmail(string Email, string token)
        {
            if (Email != null && token != null)
            {
                if (Convert.ToBoolean(dalMST_UserDAL.dbo_PR_MST_User_EmailVarification_UpdateByPK(Email, token)))
                {
                    TempData["successMessage"] = "Email Verified you can login now.";

                    #region Send Welcome Mail
                    string subject = "Welcome to Box Cricket Hub";
                    string message = System.IO.File.ReadAllText("wwwRoot/assets/resources/WelcomeMail.html");
                    CommonFunctions.SendEmail(Email, subject, message);
                    #endregion
                    return RedirectToAction("Login", "MST_User");
                }
                return RedirectToAction("Login", "MST_User");
            }
            else
            {
                TempData["errorMessage"] = "Email Verification failed!";
                return RedirectToAction("Login", "MST_User");
            }
        }
        #endregion

        #endregion

        #region Profle
        [CheckAccess]
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

                    // Check for DBNull.Value before converting
                    if (dr["CountryID"] != DBNull.Value)
                        model.CountryID = Convert.ToInt32(dr["CountryID"].ToString());
                    else
                        model.CountryID = 0; // Or whatever default value makes sense

                    if (dr["StateID"] != DBNull.Value)
                        model.StateID = Convert.ToInt32(dr["StateID"].ToString());
                    else
                        model.StateID = 0; // Or whatever default value makes sense

                    if (dr["CityID"] != DBNull.Value)
                        model.CityID = Convert.ToInt32(dr["CityID"].ToString());
                    else
                        model.CityID = 0; // Or whatever default value makes sense

                    model.ProfilePhotoPath = dr["ProfilePhotoPath"].ToString();
                    ViewBag.EditImagePath = Convert.ToString(dr["ProfilePhotoPath"]);
                }
                StateDropdownByCountry(model.CountryID ?? 0);
                CityDropdownByState(model.StateID ?? 0);
                return View("Profile", model);
            }
            return RedirectToAction("AdminIndex", "Home");
            #endregion
        }
        #endregion

        #region Save
        [HttpPost]
        public async Task<IActionResult> Save(MST_UserModel modelMST_User)
        {
            #region File section
            if (modelMST_User.File != null && modelMST_User.File.Length > 0)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploadphoto", "profile");
                string fileName = DateTime.Now.ToString("yymmssfff") + "_" + Path.GetFileName(modelMST_User.File.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await modelMST_User.File.CopyToAsync(fileStream);
                }

                modelMST_User.ProfilePhotoPath = "/uploadphoto/profile/" + fileName; // Save the relative path to the database
            }

            #endregion

            string str = Configuration.GetConnectionString("MyConnection");

            bool result = false;

            if (modelMST_User.UserID == null)
            {
                #region To Initiate Token Generation and Verification mail send
                string Email = modelMST_User.Email;
                var token = CommonFunctions.GenerateToken(32);
                modelMST_User.VarificationToken = token;

                SendConfirmationEmail(Email, token);
                #endregion

                result = dalMST_UserDALBase.dbo_PR_MST_User_Insert(str, modelMST_User.FirstName, modelMST_User.LastName, modelMST_User.Password, modelMST_User.Email, modelMST_User.Contact, modelMST_User.ProfilePhotoPath, modelMST_User.CountryID, modelMST_User.StateID, modelMST_User.CityID, modelMST_User.VarificationToken, null, null);
            }
            else
            {
                result = dalMST_UserDALBase.dbo_PR_MST_User_UpdateByPK(str, modelMST_User.FirstName, modelMST_User.LastName, modelMST_User.Password, modelMST_User.Email, modelMST_User.Contact, modelMST_User.CountryID, modelMST_User.StateID, modelMST_User.CityID, modelMST_User.ProfilePhotoPath, DateTime.Now);
            }

            if (result)
            {
                if (modelMST_User.UserID == null)
                {
                    return RedirectToAction("Login", "MST_User");
                }
                else
                {
                    TempData["successMessage"] = "Data Updated Successfully.";
                }
            }
            else
            {
                TempData["errorMessage"] = "Some Error Occured";
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

        #region Change Password Page Methods

        #region GO TO Change Password Page
        [CheckAccess]
        public IActionResult ChangePassword()
        {
            return View();
        }
        #endregion 

        #region SavePassword
        [CheckAccess]
        [HttpPost]
        public IActionResult SavePassword(MST_User_PasswrodChange modelMST_User_PasswordChange)
        {
            if (ModelState.IsValid)
            {
                if (Convert.ToBoolean(dalMST_UserDAL.dbo_PR_MST_User_Password_UpdateByPK(modelMST_User_PasswordChange)))
                    TempData["successMessage"] = "Password Updated Successfully.";

                if (CommonVariables.IsAdmin() == false)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("AdminIndex", "Home");
                }
            }
            TempData["errorMessage"] = "Some error has occurred!";
            return RedirectToAction("ChangePassword");

        }
        #endregion

        #endregion

        #region Forgot Password Page Methods (Reset Password)

        #region Go to Forgot Password Page
        public IActionResult ForgotPassword()
        {
            return View();
        }
        #endregion

        #region Validate Email & Send Reset password link
        public IActionResult ValidateEmail(string Email)
        {
            string connstr = this.Configuration.GetConnectionString("MyConnection");
            string error = null;
            bool result = false;

            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("Login", "MST_User");
            }
            else
            {
                DataTable dt = dalMST_UserDAL.dbo_PR_MST_User_Select_ByEmail(connstr, Email);
                if (dt.Rows.Count > 0)
                {
                    #region To Initiate Token Generation and Verification mail send
                    var token = CommonFunctions.GenerateToken(32);
                    string VarificationToken = token;
                    ForgotPasswordEmail(Email, token);
                    #endregion

                    result = dalMST_UserDAL.dbo_PR_MST_User_VarificationToken_UpdateByEmail(Email, VarificationToken);
                    if (result)
                    {
                        return RedirectToAction("Login", "MST_User");
                    }
                    else
                    {
                        TempData["errorMessage"] = "Some Error Occured!";
                    }
                }
                else
                {
                    TempData["errorMessage"] = "No Account Associated with Provided Email!";
                }
            }
            return RedirectToAction("ForgotPassword", "MST_User");
        }
        #endregion

        #region Send Forgot Password Email
        public IActionResult ForgotPasswordEmail(string Email, string token)
        {
            try
            {
                var ResetPasswordLink = Url.Action("ResetPasswrod", "MST_User", new { email = Email, token = token }, Request.Scheme);
                string subject = "Reset your Password";
                string message = System.IO.File.ReadAllText("wwwRoot/assets/resources/ResetPasswordMail.html");
                message = message.Replace("{{ResetPasswordLink}}", ResetPasswordLink);
                CommonFunctions.SendEmail(Email, subject, message);

                TempData["successMessage"] = "Reset Password email sent on your Registered Mail Address.";
                return RedirectToAction("Login", "MST_User");
            }
            catch (Exception)
            {
                TempData["errorMessage"] = "Some error has occurred!";
                return RedirectToAction("Login", "MST_User");
            }
        }
        #endregion

        #region Go TO Reset Password
        public IActionResult ResetPasswrod(string? Email, string? token)
        {
            if (Email != null && token != null)
            {
                var viewModel = new MST_User_PasswrodReset
                {
                    Email = Email,
                };
                return View("ResetPassword", viewModel);
            }
            else
            {
                TempData["errorMessage"] = "Verification failed try again!";
                return RedirectToAction("Login", "MST_User");
            }
        }
        #endregion

        #region Save Reset Password
        [HttpPost]
        public IActionResult SaveResetPassword(MST_User_PasswrodReset modelMST_User_PasswrodReset, string Email)
        {
            if (ModelState.IsValid)
            {
                if (Convert.ToBoolean(dalMST_UserDAL.dbo_PR_MST_User_PasswordReset_Update(Email, modelMST_User_PasswrodReset)))
                    TempData["successMessage"] = "Password Reset Successfully Login With New password.";
                TempData["errorMessage"] = "Some error has occurred!";
            }
            return RedirectToAction("Login", "MST_User");

        }
        #endregion

        #endregion

        #region Dropdown Methods

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