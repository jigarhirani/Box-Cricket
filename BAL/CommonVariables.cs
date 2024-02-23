namespace BOXCricket.BAL
{
    public static class CommonVariables
    {
        //Provides access to the current 
        //Microsoft.AspNetCore.Http.IHttpContextAccessor.HttpContext

        private static IHttpContextAccessor _httpContextAccessor;
        static CommonVariables()
        {
            _httpContextAccessor = new HttpContextAccessor();
        }
        public static int? UserID()
        {
            //Initialize the UserID with null
            int? UserID = null;
            //check if session contains specified key?
            //if it contains then return the value contained by the key.
            if (_httpContextAccessor.HttpContext.Session.GetString("UserID") != null)
            {
                UserID = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("UserID"));
            }
            return UserID;
        }
        public static string? UserName()
        {
            string? UserName = null;
            if (_httpContextAccessor.HttpContext.Session.GetString("UserName") != null)
            {
                UserName = _httpContextAccessor.HttpContext.Session.GetString("UserName").ToString();
            }

            return UserName;
        }

        public static bool? IsAdmin()
        {
            // Retrieve session value
            string isAdminString = _httpContextAccessor.HttpContext.Session.GetString("IsAdmin");

            // Check if session value is not null or empty
            if (!string.IsNullOrEmpty(isAdminString))
            {
                // Parse session value into boolean
                if (bool.TryParse(isAdminString, out bool IsAdmin))
                {
                    return IsAdmin;
                }
                else
                {
                    // Log or handle invalid session value
                    throw new InvalidOperationException("Invalid value stored in session for 'IsAdmin'.");
                }
            }

            // Default value if session value is not set
            return false;
        }

        public static string? ProfilePhotoPath()
        {
            string? ProfilePhotoPath = null;
            if (_httpContextAccessor.HttpContext.Session.GetString("ProfilePhotoPath") != null)
            {
                ProfilePhotoPath = _httpContextAccessor.HttpContext.Session.GetString("ProfilePhotoPath");
            }

            return ProfilePhotoPath;
        }


    }
}