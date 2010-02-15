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

    public class StatisticsController : Controller
    {
        private readonly IZooCatalogRepository repository;

        public StatisticsController(IZooCatalogRepository repository)
        {
            this.repository = repository;
        }

        public StatisticsController(): this(new SqlZooCatalogRepository())
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

            if (this.Request.Path.EndsWith("/Statistics/Environments"))
            {
                this.TempData["EnvironmentStatMessage"] = "Seleccione un ambiente para ver las estadísticas";
                this.TempData["ActionSucess"] = true;
                this.TempData["SearchCriteria"] = null;
            }

            return this.View("Environments", environments);
        }

        public ActionResult ViewEnvironmentStats(string environmentId)
        {

            try
            {
                this.TempData["EnvironmentStatMessage"] = null;
                var environment = this.repository.GetEnvironment(new Guid(environmentId));

                if (environment == null)
                {
                    this.TempData["EnvironmentStatMessage"] = string.Format(
                        CultureInfo.CurrentCulture, "No se encontró ningún ambiente cuyo Id sea {0}.", environmentId);
                }
                else
                {
                    this.ViewData["GraphTitle"] = environment.Name;
                    string tempGraph = "";
                    string humiGraph = "";
                    string lumiGraph = "";
                    
                    var ems = environment.EnvironmentMeasure.OrderByDescending(em => em.MeasurementDate).Take(10).ToList();

                    foreach (var em in ems)
                    {
                        tempGraph = "<set name='" + String.Format("{0:dd-MM}", em.MeasurementDate) + "' value='" + em.Temperature.ToString().Replace(',', '.') + "' color='F6BD0F' />" + tempGraph;
                        humiGraph = "<set name='" + String.Format("{0:dd-MM}", em.MeasurementDate) + "' value='" + em.Humidity.ToString().Replace(',', '.') + "' color='8BBA00' />" + humiGraph;
                        lumiGraph = "<set name='" + String.Format("{0:dd-MM}", em.MeasurementDate) + "' value='" + em.Luminosity.ToString().Replace(',', '.') + "' color='AFD8F8' />" + lumiGraph;
                    }

                    tempGraph = "<graph caption='Temperatura' xAxisName='Fecha(dd-mm)' yAxisName='*C' showNames='1' decimalPrecision='1' formatNumberScale='0'>" + tempGraph;
                    humiGraph = "<graph caption='Humedad' xAxisName='Fecha(dd-mm)' yAxisName='Percentage' showNames='1' decimalPrecision='1' formatNumberScale='0'>" + humiGraph;
                    lumiGraph = "<graph caption='Luminosidad' xAxisName='Fecha(dd-mm)' yAxisName='Lux' showNames='1' decimalPrecision='1' formatNumberScale='0'>" + lumiGraph;

                    tempGraph += "</graph>";
                    this.ViewData["TempGraph"] = tempGraph;

                    humiGraph += "</graph>";
                    this.ViewData["HumiGraph"] = humiGraph;

                    lumiGraph += "</graph>";
                    this.ViewData["LumiGraph"] = lumiGraph;
                }
            }
            catch (FormatException)
            {
                this.TempData["EnvironmentStatMessage"] = string.Format(
                    CultureInfo.CurrentCulture, "El formato del Id '{0}' es inválido.", environmentId);
            }
            catch (OverflowException)
            {
                this.TempData["EnvironmentStatMessage"] = string.Format(
                    CultureInfo.CurrentCulture, "El formato del Id '{0}' es inválido.", environmentId);
            }
 
            return this.Environments(); 
        }

        public ActionResult Animals()
        {
            var animals = this.repository.GetAnimals();
            this.TempData["NoItemsMessage"] = "¡No hay animales disponibles para el Zoológico!";

            if (this.Request.Path.EndsWith("/Statistics/Animals"))
            {
                this.TempData["AnimalStatMessage"] = "Seleccione un animal para ver las estadísticas";
                this.TempData["ActionSucess"] = true;
                this.TempData["SearchCriteria"] = null;
            }

            return this.View("Animals", animals);
        }

        public ActionResult ViewAnimalStats(string animalId)
        {

            try
            {
                this.TempData["AnimalStatMessage"] = null;
                var animal = this.repository.GetAnimal(new Guid(animalId));

                if (animal == null)
                {
                    this.TempData["AnimalStatMessage"] = string.Format(
                        CultureInfo.CurrentCulture, "No se encontró ningún animal cuyo Id sea {0}.", animalId);
                }
                else
                {
                    this.ViewData["GraphTitle"] = animal.Name;
                    string heightGraph = "";
                    string weightGraph = "";
                    string tempGraph = "";

                    var hms = animal.HealthMeasure.OrderByDescending(hm => hm.MeasurementDate).Take(10).ToList();
                    IList<String> vaccines = new List<String>();

                    foreach (var hm in hms)
                    {
                        heightGraph = "<set name='" + String.Format("{0:MM-yy}", hm.MeasurementDate) + "' value='" + hm.Height + "' color='F6BD0F' />" + heightGraph;
                        weightGraph = "<set name='" + String.Format("{0:MM-yy}", hm.MeasurementDate) + "' value='" + hm.Weight + "' color='8BBA00' />" + weightGraph;
                        tempGraph = "<set name='" + String.Format("{0:MM-yy}", hm.MeasurementDate) + "' value='" + hm.Temperature.ToString().Replace(',', '.') + "' color='AFD8F8' />" + tempGraph;
                        vaccines.Add(String.Format("{0:MM-yy}", hm.MeasurementDate) + ": " + hm.Vaccine);
                    }

                    heightGraph = "<graph caption='Altura' xAxisName='Fecha(mm-yy)' yAxisName='Cm' showNames='1' decimalPrecision='1' formatNumberScale='0'>" + heightGraph;
                    weightGraph = "<graph caption='Peso' xAxisName='Fecha(mm-yy)' yAxisName='Kg' showNames='1' decimalPrecision='1' formatNumberScale='0'>" + weightGraph;
                    tempGraph = "<graph caption='Temperatura' xAxisName='Fecha(mm-yy)' yAxisName='*C' showNames='1' decimalPrecision='1' formatNumberScale='0'>" + tempGraph;

                    heightGraph += "</graph>";
                    this.ViewData["HeightGraph"] = heightGraph;

                    weightGraph += "</graph>";
                    this.ViewData["WeightGraph"] = weightGraph;

                    tempGraph += "</graph>";
                    this.ViewData["TempGraph"] = tempGraph;

                    this.ViewData["Vaccines"] = vaccines;
                }
            }
            catch (FormatException)
            {
                this.TempData["AnimalStatMessage"] = string.Format(
                    CultureInfo.CurrentCulture, "El formato del Id '{0}' es inválido.", animalId);
            }
            catch (OverflowException)
            {
                this.TempData["AnimalStatMessage"] = string.Format(
                    CultureInfo.CurrentCulture, "El formato del Id '{0}' es inválido.", animalId);
            }

            return this.Animals();
        }
    }
}