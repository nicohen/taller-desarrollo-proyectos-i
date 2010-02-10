namespace ZoosManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web.Mvc;
    using ZoosManagementSystem.Web.Models;

    using Environment=ZoosManagementSystem.Web.Models.Environment;

    public class AdministrationController : Controller
    {
        private readonly IZooCatalogRepository repository;

        public AdministrationController(IZooCatalogRepository repository)
        {
            this.repository = repository;

            this.TempData["DeleteMessage"] = null;
            this.TempData["NoItemsMessage"] = null;
            this.TempData["DeleteSucess"] = null;
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
            this.TempData["DeleteMessage"] = null;
            this.TempData["NoItemsMessage"] = "No se encontraron ambientes.";
            this.TempData["SearchCriteria"] = searchCriteria;
            IList<Environment> environments = null;

            if (!string.IsNullOrEmpty(searchCriteria))
            {
                environments = this.repository.SearchEnvironments(searchCriteria); 
            }

            return this.View("Environments", environments);
        }

        public ActionResult DeleteEnvironment(string environmentId)
        {
            try
            {
                this.TempData["DeleteSucess"] = true;
                this.TempData["DeleteMessage"] = "El ambiente se eliminó correctamente.";
                var id = new System.Guid(environmentId);

                if (!this.repository.DeleteEnvironment(id))
                {
                    this.TempData["DeleteSucess"] = false;
                    this.TempData["DeleteMessage"] = string.Format(
                        CultureInfo.CurrentCulture, "No se encontró ningún ambiente cuyo Id sea {0}.", environmentId);
                }
            }
            catch (FormatException)
            {
                this.TempData["DeleteSucess"] = false;
                this.TempData["DeleteMessage"] = string.Format(
                    CultureInfo.CurrentCulture, "El formato del Id '{0}' es inválido.", environmentId);
            }
            catch(OverflowException)
            {
                this.TempData["DeleteSucess"] = false;
                this.TempData["DeleteMessage"] = string.Format(
                    CultureInfo.CurrentCulture, "El formato del Id '{0}' es inválido.", environmentId);
            }

            return this.Environments();                
        }

        public ActionResult Animals()
        {
            return this.View();
        }
    }
}