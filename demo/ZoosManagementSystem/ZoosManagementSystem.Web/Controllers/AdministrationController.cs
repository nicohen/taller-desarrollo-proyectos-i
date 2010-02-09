namespace ZoosManagementSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ZoosManagementSystem.Web.Models;

    public class AdministrationController : Controller
    {
        private readonly IZooCatalogRepository repository;

        public AdministrationController(IZooCatalogRepository repository)
        {
            this.repository = repository;
        }

        public AdministrationController(): this(new SqlZooCatalogRepository())
        {
        }

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
            var environments = this.repository.GetEnvironments();
            this.TempData["NoItemsMessage"] = "¡No hay ambientes disponibles para el Zoológico!";

            return this.View("Environments", environments);
        }

        public ActionResult SearchEnvironments(string searchCriteria)
        {
            this.TempData["NoItemsMessage"] = "No se encontraron ambientes.";
            this.TempData["SearchCriteria"] = searchCriteria;
            IList<Environment> environments = null;

            if (!string.IsNullOrEmpty(searchCriteria))
            {
                environments = this.repository.SearchEnvironments(searchCriteria); 
            }

            return this.View("Environments", environments);
        }

        public ActionResult Animals()
        {
            return this.View();
        }
    }
}