namespace ZoosManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web.Mvc;
    using ZoosManagementSystem.Web.Models;

    using Environment=ZoosManagementSystem.Web.Models.Environment;
    using ZoosManagementSystem.Web.ViewData;

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

            if (this.Request.Path == "/ZoosManagementSystem/Administration/Environments")
            {
                this.TempData["DeleteMessage"] = null;
                this.TempData["DeleteSucess"] = null;
                this.TempData["SearchCriteria"] = null;

            }

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

                if (!this.repository.DeleteEnvironment(new Guid(environmentId)))
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

        public ActionResult EditEnvironment(string environmentId)
        {
            EnvironmentViewData environmentViewData = null;

            try
            {
                this.TempData["EditMessage"] = null;
                var environment = this.repository.GetEnvironment(new Guid(environmentId));

                if (environment == null)
                {
                    this.TempData["EditMessage"] = string.Format(
                        CultureInfo.CurrentCulture, "No se encontró ningún ambiente cuyo Id sea {0}.", environmentId);
                }
                else
                {
                    environmentViewData = environment.ToViewData(this.repository);
                }
            }
            catch (FormatException)
            {
                this.TempData["EditMessage"] = string.Format(
                    CultureInfo.CurrentCulture, "El formato del Id '{0}' es inválido.", environmentId);
            }
            catch (OverflowException)
            {
                this.TempData["EditMessage"] = string.Format(
                    CultureInfo.CurrentCulture, "El formato del Id '{0}' es inválido.", environmentId);
            }

            return this.View(environmentViewData);  
        }

        public ActionResult Animals()
        {
            return this.View();
        }
    }
}