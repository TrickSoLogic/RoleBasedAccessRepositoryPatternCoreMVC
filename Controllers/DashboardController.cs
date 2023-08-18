using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RoleBasedAccess.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {

        public IActionResult Display()
        {
            return View();
        }
        public IActionResult AssetManagement()
        {
            return View();
        }

     }
}
