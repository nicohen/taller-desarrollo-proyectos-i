namespace ZoosManagementSystem.Web.Controllers
{
    using System.Web.Mvc;

    public class StatisticsController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Surveys()
        {
            return this.View();
        }

        public ActionResult Environments()
        {
            return this.View();
        }

        public ActionResult Animals()
        {
            return this.View();
        }
    }
}