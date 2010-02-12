namespace ZoosManagementSystem.Web.Models
{
    using System;
    using System.Collections.Generic;

    public interface IZooCatalogRepository
    {
        IList<Environment> GetEnvironments();

        Environment GetEnvironment(Guid guid);

        IList<Environment> SearchEnvironments(string searchCriteria);

        bool DeleteEnvironment(Guid environmentId);
    }
}