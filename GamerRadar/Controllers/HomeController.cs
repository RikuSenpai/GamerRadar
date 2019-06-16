using System.Web.Mvc;

namespace GamerRadar.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Capture()
        {
            return View("Index");
        }
    }
}