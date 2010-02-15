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

        public ActionResult NewEnvironment()
        {
            var environmentViewData = new EnvironmentViewData
                {
                    FreeAnimals = this.repository.GetFreeAnimals()
                            .Select(a => a.ToViewData(this.repository))
                            .ToList()
                };
            
            return this.View("EditEnvironment", environmentViewData);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("NewEnvironment")]
        public ActionResult SaveNewEnvironment()
        {
            var environmentViewData = new EnvironmentViewData();
            var updateModelResult = this.TryUpdateModel<EnvironmentViewData>(environmentViewData, null, null, new[] { "EnvironmentId", "freeanimals" });

            if (!updateModelResult)
            {
                environmentViewData.FreeAnimals = this.repository.GetFreeAnimals()
                    .Select(a => a.ToViewData(this.repository))
                    .ToList();
                return View("EditEnvironment", environmentViewData);
            }

            Guid environmentId = Guid.Empty;
            try
            {
                environmentId = this.repository.CreateEnvironment(environmentViewData);
                this.TempData["ActionSucess"] = true;
                this.TempData["EnvironmentMessage"] = "Se creó correctamente el ambiente";
            }
            catch (Exception exception)
            {
                this.TempData["ActionSucess"] = false;
                this.TempData["EnvironmentMessage"] = exception.Message;
            }

            return this.RedirectToRoute("SearchEnvironment", new { searchCriteria = environmentId.ToString() });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("EditEnvironment")]
        public ActionResult UpdateEnvironment(string environmentId)
        {
            var environmentViewData = new EnvironmentViewData();
            var updateModelResult = this.TryUpdateModel<EnvironmentViewData>(environmentViewData, null, null, new [] { "EnvironmentId", "freeanimals" });

            if (!updateModelResult)
            {
                environmentViewData.FreeAnimals = this.repository.GetFreeAnimals()
                    .Select(a => a.ToViewData(this.repository))
                    .ToList();
                return View(environmentViewData);
            }

            try
            {
                environmentViewData.EnvironmentId = environmentId;
                this.repository.UpdateEnvironment(environmentViewData);
                this.TempData["ActionSucess"] = true;
                this.TempData["EnvironmentMessage"] = "Se editaron correctamente los datos del ambiente";
            }
            catch (Exception exception)
            {
                this.TempData["ActionSucess"] = false;
                this.TempData["EnvironmentMessage"] = exception.Message;
            }

            return this.RedirectToRoute("SearchEnvironment", new { searchCriteria = environmentViewData.EnvironmentId });
        }

        public ActionResult Animals()
        {
            var animals = this.repository.GetAnimals();
            this.TempData["NoItemsMessage"] = "¡No hay animales disponibles para el Zoológico!";

            if (this.Request.Path.EndsWith("/Administration/Animals"))
            {
                this.TempData["AnimalMessage"] = null;
                this.TempData["ActionSucess"] = null;
                this.TempData["SearchCriteria"] = null;
            }

            return this.View("Animals", animals);
        }

        public ActionResult SearchAnimals(string searchCriteria)
        {
            this.TempData["NoItemsMessage"] = "No se encontraron animales.";
            this.TempData["SearchCriteria"] = searchCriteria;
            IList<Models.Animal> animals = null;

            if (!string.IsNullOrEmpty(searchCriteria))
            {
                animals = this.repository.SearchAnimals(searchCriteria);
            }

            return this.View("Animals", animals);
        }

        public ActionResult DeleteAnimal(string animalId)
        {
            try
            {
                this.TempData["ActionSucess"] = true;
                this.TempData["AnimalMessage"] = "El animal se eliminó correctamente.";

                if (!this.repository.DeleteAnimal(new Guid(animalId)))
                {
                    this.TempData["ActionSucess"] = false;
                    this.TempData["AnimalMessage"] = string.Format(
                        CultureInfo.CurrentCulture, "No se encontró ningún animal cuyo Id sea {0}.", animalId);
                }
            }
            catch (FormatException)
            {
                this.TempData["ActionSucess"] = false;
                this.TempData["AnimalMessage"] = string.Format(
                    CultureInfo.CurrentCulture, "El formato del Id '{0}' es inválido.", animalId);
            }
            catch (OverflowException)
            {
                this.TempData["ActionSucess"] = false;
                this.TempData["AnimalMessage"] = string.Format(
                    CultureInfo.CurrentCulture, "El formato del Id '{0}' es inválido.", animalId);
            }

            return this.Animals();
        }

        public ActionResult EditAnimal(string animalId)
        {
            AnimalViewData environmentViewData = null;

            try
            {
                this.TempData["EditMessage"] = null;
                var animal = this.repository.GetAnimal(new Guid(animalId));

                if (animal == null)
                {
                    this.TempData["EditMessage"] = string.Format(
                        CultureInfo.CurrentCulture, "No se encontró ningún animal cuyo Id sea {0}.", animalId);
                }
                else
                {
                    environmentViewData = animal.ToViewData(this.repository);
                }
            }
            catch (FormatException)
            {
                this.TempData["EditMessage"] = string.Format(
                    CultureInfo.CurrentCulture, "El formato del Id '{0}' es inválido.", animalId);
            }
            catch (OverflowException)
            {
                this.TempData["EditMessage"] = string.Format(
                    CultureInfo.CurrentCulture, "El formato del Id '{0}' es inválido.", animalId);
            }

            return this.View(environmentViewData);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("EditAnimal")]
        public ActionResult UpdateAnimal(string animalId)
        {
            var animalViewData = new AnimalViewData();
            var updateModelResult = this.TryUpdateModel<AnimalViewData>(animalViewData, null, null, new[] { "AnimalId", "freeanimals" });

            if (!updateModelResult)
            {
                animalViewData.AnimalId = animalId;
                animalViewData.FeedingsAvailable = this.repository.GetFeedings()
                    .Select(f => f.ToViewData())
                    .ToList();
                animalViewData.ResponsiblesAvailable= this.repository.GetResponsibles()
                    .Select(r => r.ToViewData())
                    .ToList();
                animalViewData.EnvironmentsAvailable = this.repository.GetEnvironments()
                    .Select(r => r.ToViewData(this.repository))
                    .ToList();
                return View(animalViewData);
            }

            try
            {
                animalViewData.AnimalId = animalId;
                this.repository.UpdateAnimal(animalViewData);
                this.TempData["ActionSucess"] = true;
                this.TempData["EnvironmentMessage"] = "Se editaron correctamente los datos del animal";
            }
            catch (Exception exception)
            {
                this.TempData["ActionSucess"] = false;
                this.TempData["EnvironmentMessage"] = exception.Message;
            }

            return this.RedirectToRoute("SearchAnimals", new { searchCriteria = animalViewData.AnimalId });
        }

        public ActionResult NewAnimal()
        {
            var environmentViewData = new AnimalViewData
            {
                EnvironmentsAvailable = this.repository.GetEnvironments()
                        .Select(e => e.ToViewData(this.repository))
                        .ToList(),
                ResponsiblesAvailable = this.repository.GetResponsibles()
                    .Select(r => r.ToViewData())
                    .ToList(),
                FeedingsAvailable = this.repository.GetFeedings()
                    .Select(f => f.ToViewData())
                    .ToList()
            };

            return this.View(environmentViewData);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("NewAnimal")]
        public ActionResult SaveNewAnimal()
        {
            var animalViewData = new AnimalViewData();
            var updateModelResult = this.TryUpdateModel<AnimalViewData>(animalViewData, null, null, new[] { "freeanimals" });

            if (!updateModelResult)
            {
                animalViewData.EnvironmentsAvailable = this.repository.GetEnvironments()
                        .Select(e => e.ToViewData(this.repository))
                        .ToList();
                animalViewData.ResponsiblesAvailable = this.repository.GetResponsibles()
                    .Select(r => r.ToViewData())
                    .ToList();
                animalViewData.FeedingsAvailable = this.repository.GetFeedings()
                    .Select(f => f.ToViewData())
                    .ToList();
                return View(animalViewData);
            }

            var animalId = Guid.Empty;
            try
            {
                animalId = this.repository.CreateAnimal(animalViewData);
                this.TempData["ActionSucess"] = true;
                this.TempData["EnvironmentMessage"] = "Se creó correctamente el ambiente";
            }
            catch (Exception exception)
            {
                this.TempData["ActionSucess"] = false;
                this.TempData["EnvironmentMessage"] = exception.Message;
            }

            return this.RedirectToRoute("SearchAnimals", new { searchCriteria = animalId.ToString() });
        }

        public ActionResult EditHealthMeasure(string healthMeasureId)
        {
            HealthMeasureViewData healthMeasureViewData = null;

            try
            {
                this.TempData["EditMessage"] = null;
                var healthMeasure = this.repository.GetHealthMeasure(new Guid(healthMeasureId));

                if (healthMeasure == null)
                {
                    this.TempData["EditMessage"] = string.Format(
                        CultureInfo.CurrentCulture, "No se encontró ningún examen médico cuyo Id sea {0}.", healthMeasureId);
                }
                else
                {
                    healthMeasureViewData = healthMeasure.ToViewData(this.repository);
                }
            }
            catch (FormatException)
            {
                this.TempData["EditMessage"] = string.Format(
                    CultureInfo.CurrentCulture, "El formato del Id '{0}' es inválido.", healthMeasureId);
            }
            catch (OverflowException)
            {
                this.TempData["EditMessage"] = string.Format(
                    CultureInfo.CurrentCulture, "El formato del Id '{0}' es inválido.", healthMeasureId);
            }

            return this.View(healthMeasureViewData);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("EditHealthMeasure")]
        public ActionResult UpdateHealthMeasure(string healthMeasureId)
        {
            var healthMeasureViewData = new HealthMeasureViewData();
            var updateModelResult = this.TryUpdateModel<HealthMeasureViewData>(healthMeasureViewData);

            if (!updateModelResult)
            {
                healthMeasureViewData.HealthMeasureId = healthMeasureId;
                healthMeasureViewData.AnimalsAvailable =
                    this.repository.GetAnimals().Select(
                        a =>
                        new AnimalViewData
                            {
                                AnimalId = a.Id.ToString(),
                                Name = a.Name,
                                Species = a.Species,
                                Sex = (a.Sex.ToLowerInvariant() == "m") ? "Macho" : "Hembra",
                            }).ToList();
                return View(healthMeasureViewData);
            }

            try
            {
                healthMeasureViewData.HealthMeasureId = healthMeasureId;
                this.repository.UpdateHealthMeasure(healthMeasureViewData);
                this.TempData["ActionSucess"] = true;
                this.TempData["EnvironmentMessage"] = "Se editaron correctamente los datos del animal";
            }
            catch (Exception exception)
            {
                this.TempData["ActionSucess"] = false;
                this.TempData["EnvironmentMessage"] = exception.Message;
            }

            return this.RedirectToRoute("SearchAnimals", new { searchCriteria = healthMeasureViewData.AnimalId });
        }

        public ActionResult NewHealthMeasure(string animalId)
        {
            var healthMeasureViewData = new HealthMeasureViewData
                {
                    AnimalsAvailable =
                        this.repository.GetAnimals().Select(
                        a =>
                        new AnimalViewData
                            {
                                AnimalId = a.Id.ToString(),
                                Name = a.Name,
                                Species = a.Species,
                                Sex = (a.Sex.ToLowerInvariant() == "m") ? "Macho" : "Hembra",
                            }).ToList(),
                    AnimalId = animalId,
                    MeasurementDate = DateTime.Now.ToString("yyyy/MM/dd")
                };

            return this.View("EditHealthMeasure", healthMeasureViewData);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("NewHealthMeasure")]
        public ActionResult SaveNewHealthMeasure(string animalId)
        {
            var healthMeasureViewData = new HealthMeasureViewData();
            var updateModelResult = this.TryUpdateModel<HealthMeasureViewData>(healthMeasureViewData);

            if (!updateModelResult)
            {
                healthMeasureViewData.AnimalsAvailable =
                   this.repository.GetAnimals().Select(
                       a =>
                       new AnimalViewData
                       {
                           AnimalId = a.Id.ToString(),
                           Name = a.Name,
                           Species = a.Species,
                           Sex = (a.Sex.ToLowerInvariant() == "m") ? "Macho" : "Hembra",
                       }).ToList();
                return View("EditHealthMeasure", healthMeasureViewData);
            }

            try
            {
                this.repository.CreateHealthMeasure(healthMeasureViewData);
                this.TempData["ActionSucess"] = true;
                this.TempData["AnimalMessage"] = "Se creó correctamente el registro del examen médico";
            }
            catch (Exception exception)
            {
                this.TempData["ActionSucess"] = false;
                this.TempData["AnimalMessage"] = exception.Message;
            }

            return this.RedirectToRoute("SearchAnimals", new { searchCriteria = healthMeasureViewData.AnimalId });
        }
    }
}