namespace ZoosManagementSystem.Web.Models
{
    using System;
    using System.Collections.Generic;

    using ViewData;

    public interface IZooCatalogRepository
    {
        IList<Animal> GetFreeAnimals();

        IList<Environment> GetEnvironments();

        Environment GetEnvironment(Guid guid);

        IList<Environment> SearchEnvironments(string searchCriteria);

        void UpdateEnvironment(EnvironmentViewData data);

        bool DeleteEnvironment(Guid environmentId);
    }
}