using BOXCricket.Areas.SEC_User.Models;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace BOXCricket.Areas.SEC_User.Controllers
{
    [Area("SEC_User")]
    [Route("SEC_User/[controller]/[action]")]
    public class SEC_UserController : Controller
    {

        #region Config

        private IConfiguration Configuration;
        public SEC_UserController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #endregion

        #region SEC_Login
        public IActionResult SEC_Login()
        {
            HttpContext.Session.Clear();
            return View();
        }
        #endregion

        #region Login
        [HttpPost]
        public IActionResult Login(SEC_UserModel sEC_UserModel)
        {
            string ErrorMsg = string.Empty;
            if (string.IsNullOrEmpty(sEC_UserModel.UserName))
            {
                ErrorMsg += "User Name is Required";
            }
            if (string.IsNullOrEmpty(sEC_UserModel.Password))
            {
                ErrorMsg += "<br/>Password is Required";
            }
            if (ModelState.IsValid)
            {
                SqlConnection conn = new SqlConnection(Configuration.GetConnectionString("MyConnection"));

                conn.Open();
                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_SEC_User_Login";
                objCmd.Parameters.AddWithValue("@UserName", sEC_UserModel.UserName);
                objCmd.Parameters.AddWithValue("@Password", sEC_UserModel.Password);
                SqlDataReader objSDR = objCmd.ExecuteReader();
                DataTable dtLogin = new DataTable();
                // Check if Data Reader has Rows or not
                // if row(s) does not exists that means either username or password or both are invalid.
                if (!objSDR.HasRows)
                {
                    TempData["ErrorMessage"] = "Invalid User Name or Password";
                    return RedirectToAction("SEC_Login", "SEC_User");
                }
                else
                {
                    dtLogin.Load(objSDR);
                    //Load the retrived data to session through HttpContext.
                    foreach (DataRow dr in dtLogin.Rows)
                    {
                        HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                        HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        //HttpContext.Session.SetString("MobileNo", dr["MobileNo"].ToString());
                        //HttpContext.Session.SetString("Email", dr["Email"].ToString());
                        HttpContext.Session.SetString("Password", dr["Password"].ToString());

                    }

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["ErrorMessage"] = ErrorMsg;
                return RedirectToAction("SEC_Login", "SEC_User");
            }
        }
        #endregion

        #region Logout
        //Logout action to clear current session and redirect user to login page
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SEC_Login", "SEC_User");
        }
        #endregion

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult SingUP()
        {
            return View();
        }
    }

}
