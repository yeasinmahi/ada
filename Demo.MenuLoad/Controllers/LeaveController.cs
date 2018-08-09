using System.Web.Mvc;

namespace Demo.MenuLoad.Controllers
{
    public class LeaveController : Controller
    {
        // GET: Leave
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminLeave()
        {
            return View();
        }
    }
}