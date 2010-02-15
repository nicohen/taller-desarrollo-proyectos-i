namespace ZoosManagementSystem.Web.Models
{
    using System;
    using System.Collections.Generic;

    using ViewData;

    public interface IZooCatalogRepository
    {
        IList<Environment> GetEnvironments();

        Environment GetEnvironment(Guid guid);

        IList<Animal> GetAnimals();

        IList<Animal> GetFreeAnimals();

        IList<Environment> SearchEnvironments(string searchCriteria);

        IList<Animal> SearchAnimals(string searchCriteria);
        
        void UpdateEnvironment(EnvironmentViewData environmentViewData);

        Guid CreateEnvironment(EnvironmentViewData environmentViewData);

        bool DeleteEnvironment(Guid environmentId);

        bool DeleteAnimal(Guid animalId);
    }
}