namespace ZoosManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web.Mvc;
    using ZoosManagementSystem.Web.Models;

    using ZoosManagementSystem.Web.ViewData;


    [HandleError]
    public class HomeController : Controller
    {

        private readonly IZooCatalogRepository repository;

        public HomeController(IZooCatalogRepository repository)
        {
            this.repository = repository;
        }

        public HomeController(): this(new SqlZooCatalogRepository())
        {
        }

        public ActionResult Index()
        {
            this.ViewData["Message"] = "Bienvenido al Sistema de Administración de Zoológicos Inteligente!";
            HomeViewData homeData = new HomeViewData();
            //homeData.environments = this.repository.GetEnvironments();
            //homeData.animals = this.repository.GetFreeAnimals();
            return this.View(homeData);
        }

        public ActionResult About()
        {
            return this.View();
        }
    }
}