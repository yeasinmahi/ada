using System.Web.Mvc;
using Serilog;

namespace SerilogTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ILogger logger = new LoggerConfiguration()
                .WriteTo.File("a.txt")
                .CreateLogger();

            Log.Logger = logger;

            Log.Information("Add User");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            Log.Information("Add User");
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}