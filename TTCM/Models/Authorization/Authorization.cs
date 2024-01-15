using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Mvc;
namespace TTCM.Models.Authorization

{
    public class Authorization : AuthorizeAttribute
    {
        //public override void OnAuthorization(AuthorizationContext filterContext)
        /*{
            // Kiểm tra quyền truy cập của người dùng
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // Lấy role của người dùng từ CSDL hoặc từ các thông tin khác
                int userRole = 0; // Mặc định là 0 (User)

                // Kiểm tra role của người dùng
                if (userRole == 1)
                {
                    // Cho phép truy cập tài nguyên
                }
                else
                {
                    // Từ chối truy cập tài nguyên
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
            }
            //else
            {
                // Chuyển hướng đến trang đăng nhập
               // filterContext.Result = new HttpUnauthorizedResult();
            //}
        }*/
    }
}
