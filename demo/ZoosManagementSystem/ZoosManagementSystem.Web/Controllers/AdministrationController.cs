namespace ZoosManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web.Mvc;
    using System.Linq;
    using System.Web.Routing;

    using ZoosManagementSystem.Web.Models;
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

            if (this.Request.Path.EndsWith("/Administration/Environments"))
            {
                this.TempData["EnvironmentMessage"] = null;
                this.TempData["ActionSucess"] = null;
                this.TempData["SearchCriteria"] = null;
            }

            return this.View("Environments", environments);
        }

        public ActionResult SearchEnvironments(string searchCriteria)
        {
            this.TempData["NoItemsMessage"] = "No se encontraron ambientes.";
            this.TempData["SearchCriteria"] = searchCriteria;
            IList<Models.Environment> environments = null;

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
                this.TempData["ActionSucess"] = true;
                this.TempData["EnvironmentMessage"] = "El ambiente se eliminó correctamente.";

                if (!this.repository.DeleteEnvironment(new Guid(environmentId)))
                {
                    this.TempData["ActionSucess"] = false;
                    this.TempData["EnvironmentMessage"] = string.Format(
                        CultureInfo.CurrentCulture, "No se encontró ningún ambiente cuyo Id sea {0}.", environmentId);
                }
            }
            catch (FormatException)
            {
                this.TempData["ActionSucess"] = false;
                this.TempData["EnvironmentMessage"] = string.Format(
                    CultureInfo.CurrentCulture, "El formato del Id '{0}' es inválido.", environmentId);
            }
            catch(OverflowException)
            {
                this.TempData["ActionSucess"] = false;
                this.TempData["EnvironmentMessage"] = string.Format(
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

        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("EditEnvironment")]
        public ActionResult SaveEnvironment(string environmentId)
        {
            var environmentViewData = new EnvironmentViewData();
            var updateModelResult = this.TryUpdateModel<EnvironmentViewData>(environmentViewData, null, null, new [] { "EnvironmentId", "freeanimals" });

            if (!updateModelResult)
            {
                environmentViewData.FreeAnimals = this.repository.GetFreeAnimals()
                    .Select(a => a.ToViewData())
                    .ToList();
                return View(environmentViewData);
            }

            environmentViewData.EnvironmentId = environmentId;
            this.SaveOrUpdateEnvironment(environmentViewData);
            this.TempData["ActionSucess"] = true;
            this.TempData["EnvironmentMessage"] = "Se editó correctamente el ambiente";

            return this.RedirectToRoute("SearchEnvironment", new { searchCriteria = environmentViewData.EnvironmentId });
        }

        public ActionResult Animals()
        {
            return this.View();
        }

        private void SaveOrUpdateEnvironment(EnvironmentViewData data)
        {
            //TODO
        }
    }
}