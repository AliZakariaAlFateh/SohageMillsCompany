using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sohag__Mills_Company.Controllers
{
    //[Authorize(Roles ="Admin")]
    [Authorize]
    public class DashBordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
