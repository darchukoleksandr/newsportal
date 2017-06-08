using System.Web.Mvc;

namespace Web_MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
//            if (Request.Params["lang"] == "ua")
//                return View("Index.ua");
//            return View("Index.en");
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}