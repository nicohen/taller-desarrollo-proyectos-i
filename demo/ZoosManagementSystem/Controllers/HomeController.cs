namespace ZoosManagementSystem.Controllers
{
    using System.Web.Mvc;

    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Bienvenido al Sistema de Administración de Zoológicos Inteligente!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
