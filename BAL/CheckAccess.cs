using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BOXCricket.BAL
{
    public class CheckAccess : ActionFilterAttribute, IAuthorizationFilter
    {
        //When User ID is not availale or removed from session, it will redirect to login page
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("UserID") == null)
            {
                filterContext.Result = new RedirectResult("~/SEC_User/SEC_User/SEC_Login");
            }
        }

        // Once we logout (session is cleared) then we can not go back to the previous screen, We must login to proceed further.

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            filterContext.HttpContext.Response.Headers["Expires"] = "-1";
            filterContext.HttpContext.Response.Headers["Pragma"] = "no-cache";
            base.OnResultExecuting(filterContext);
        }
    }
}
