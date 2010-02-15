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

                    tempGraph = "<graph caption='Temperatura' xAxisName='Fecha' yAxisName='*C' showNames='1' decimalPrecision='1' formatNumberScale='0'>" + tempGraph;
                    humiGraph = "<graph caption='Humedad' xAxisName='Fecha' yAxisName='Percentage' showNames='1' decimalPrecision='1' formatNumberScale='0'>" + humiGraph;
                    lumiGraph = "<graph caption='Luminosidad' xAxisName='Fecha' yAxisName='Lux' showNames='1' decimalPrecision='1' formatNumberScale='0'>" + lumiGraph;

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
            return this.View();
        }
    }
}