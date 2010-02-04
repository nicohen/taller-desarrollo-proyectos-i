namespace ZoosManagementSystem.Web.Controllers
{
    using System.Web.Mvc;

    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            this.ViewData["Message"] = "Bienvenido al Sistema de Administración de Zoológicos Inteligente!";

            return this.View();
        }

        public ActionResult About()
        {
            return this.View();
        }
    }
}