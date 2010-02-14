namespace ZoosManagementSystem.Web.ViewData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ZoosManagementSystem.Web.Models;

    public class HomeViewData
    {
        public IList<ZoosManagementSystem.Web.Models.Environment> environments
        { get; set; }

        public IList<Animal> animals
        { get; set; }

    }
}
