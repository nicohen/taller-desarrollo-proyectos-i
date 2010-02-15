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

            this.TempData["ActionSucess"] = true;
            this.TempData["EnvironmentStatMessage"] = null;
            this.ViewData["GraphTitle"] = this.repository.GetEnvironment(new Guid(environmentId)).Name;
            string tempGraph = "<graph caption='Temperatura' xAxisName='Fecha' yAxisName='*C' showNames='1' decimalPrecision='0' formatNumberScale='0'>"
                                          + "<set name='Jan' value='462' color='AFD8F8' />"
                                          + "<set name='Feb' value='857' color='F6BD0F' />"
                                          + "<set name='Mar' value='671' color='8BBA00' />"
                                          + "<set name='Apr' value='494' color='FF8E46' />"
                                          + "<set name='May' value='761' color='008E8E' />"
                                          + "<set name='Jun' value='960' color='D64646' />"
                                          + "<set name='Jul' value='629' color='8E468E' />"
                                          + "<set name='Aug' value='622' color='588526' />"
                                          + "<set name='Sep' value='376' color='B3AA00' />"
                                          + "<set name='Oct' value='494' color='008ED6' />"
                                        + "</graph>";
            this.ViewData["TempGraph"] = tempGraph;
            string humiGraph = "<graph caption='Humedad' xAxisName='Fecha' yAxisName='Perc' showNames='1' decimalPrecision='0' formatNumberScale='0'>"
                              + "<set name='Jan' value='462' color='AFD8F8' />"
                              + "<set name='Feb' value='857' color='F6BD0F' />"
                              + "<set name='Mar' value='671' color='8BBA00' />"
                              + "<set name='Apr' value='494' color='FF8E46' />"
                              + "<set name='May' value='761' color='008E8E' />"
                              + "<set name='Jun' value='960' color='D64646' />"
                              + "<set name='Jul' value='629' color='8E468E' />"
                              + "<set name='Aug' value='622' color='588526' />"
                              + "<set name='Sep' value='376' color='B3AA00' />"
                              + "<set name='Oct' value='494' color='008ED6' />"
                            + "</graph>";
            this.ViewData["HumiGraph"] = humiGraph;
            string lumiGraph = "<graph caption='Luminosidad' xAxisName='Fecha' yAxisName='Lux' showNames='1' decimalPrecision='0' formatNumberScale='0'>"
                              + "<set name='Jan' value='462' color='AFD8F8' />"
                              + "<set name='Feb' value='857' color='AFD8F8' />"
                              + "<set name='Mar' value='671' color='AFD8F8' />"
                              + "<set name='Apr' value='494' color='AFD8F8' />"
                              + "<set name='May' value='761' color='AFD8F8' />"
                              + "<set name='Jun' value='960' color='AFD8F8' />"
                              + "<set name='Jul' value='629' color='AFD8F8' />"
                              + "<set name='Aug' value='622' color='AFD8F8' />"
                              + "<set name='Sep' value='376' color='AFD8F8' />"
                              + "<set name='Oct' value='494' color='AFD8F8' />"
                            + "</graph>";
            this.ViewData["LumiGraph"] = lumiGraph;
                /*if (!this.repository.DeleteEnvironment(new Guid(environmentId)))
                {
                    this.TempData["ActionSucess"] = false;
                    this.TempData["EnvironmentStatMessage"] = string.Format(
                        CultureInfo.CurrentCulture, "No es posible mostrar las estadísticas para este ambiente", environmentId);
                }*/

            return this.Environments(); 
        }

        public ActionResult Animals()
        {
            return this.View();
        }
    }
}