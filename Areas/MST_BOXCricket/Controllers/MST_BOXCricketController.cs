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


        private IWebHostEnvironment webHostEnvironment;
        public MST_BOXCricketController(IWebHostEnvironment _webHostEnvironment)
        {
            webHostEnvironment = _webHostEnvironment;
        }

        #region Select All 
        public IActionResult BOXCricketList()
        {
            DataTable dt = dalMST_BOXCricketDALBase.dbo_PR_MST_BOXCricket_SelectAll_ByOwnerID();
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
                        model.BOXCricketImagePath1 = dr["BOXCricketImagePath1"].ToString();
                        model.BOXCricketImagePath2 = dr["BOXCricketImagePath2"].ToString();
                        ViewBag.EditBOXCricketImagePath1 = Convert.ToString(dr["BOXCricketImagePath1"]);
                        ViewBag.EditBOXCricketImagePath2 = Convert.ToString(dr["BOXCricketImagePath2"]);
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
                #region BOXCricketImagePath section
                // Iterate over all BOXCricket images in the model
                for (int i = 1; i <= 2; i++)
                {
                    var BOXCricketImageProperty = $"BOXCricketImage{i}";
                    var BOXCricketImagePathProperty = $"BOXCricketImagePath{i}";

                    var BOXCricketImage = (IFormFile)modelMST_BOXCricket.GetType().GetProperty(BOXCricketImageProperty).GetValue(modelMST_BOXCricket);
                    var BOXCricketImagePath = (string)modelMST_BOXCricket.GetType().GetProperty(BOXCricketImagePathProperty).GetValue(modelMST_BOXCricket);

                    if (BOXCricketImage != null)
                    {
                        // Determine uploaded BOXCricketImage type and create folder according to file type
                        string FilePath = "wwwroot\\uploadphoto/boxcricket/";
                        //string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                        string path = Path.Combine(webHostEnvironment.ContentRootPath, FilePath);

                        // Upload BOXCricketImage to the root directory of the project
                        string folderPath = Path.Combine(path, BOXCricketImage.FileName);
                        using (var stream = new FileStream(folderPath, FileMode.Create))
                        {
                            BOXCricketImage.CopyTo(stream);
                        }
                        //using (FileStream fs = System.IO.File.Create(folderPath))
                        //{
                        //    BOXCricketImage.CopyTo(fs);
                        //}

                        // Store BOXCricketImage path in the model
                        //modelMST_BOXCricket.GetType().GetProperty(BOXCricketImagePathProperty).SetValue(modelMST_BOXCricket, "/" + FilePath + "/" + BOXCricketImage.FileName);
                        modelMST_BOXCricket.GetType().GetProperty(BOXCricketImagePathProperty).SetValue(modelMST_BOXCricket, FilePath.Replace("wwwroot\\", "/") + BOXCricketImage.FileName);
                    }
                }
                #endregion

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
            DataTable dt = dalMST_BOXCricketDAL.dbo_PR_MST_BOXCricket_Search_ByFilters(BOXCricketName);
            return View("BOXCricketList", dt);

        }
        #endregion
    }
}
