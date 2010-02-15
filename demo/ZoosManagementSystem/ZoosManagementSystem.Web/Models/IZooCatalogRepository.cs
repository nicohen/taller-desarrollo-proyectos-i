namespace ZoosManagementSystem.Web.Models
{
    using System;
    using System.Collections.Generic;

    using ViewData;

    public interface IZooCatalogRepository
    {
        IList<Environment> GetEnvironments();

        IList<Feeding> GetFeedings();

        IList<Animal> GetAnimals();

        IList<Responsible> GetResponsibles();

        IList<Animal> GetFreeAnimals();

        Environment GetEnvironment(Guid environmentId);

        Animal GetAnimal(Guid animalId);

        HealthMeasure GetHealthMeasure(Guid healthMeasureId);

        IList<Environment> SearchEnvironments(string searchCriteria);

        IList<Animal> SearchAnimals(string searchCriteria);
        
        void UpdateEnvironment(EnvironmentViewData environmentViewData);

        void UpdateAnimal(AnimalViewData animalViewData);

        void UpdateHealthMeasure(HealthMeasureViewData healthMeasureViewData);

        Guid CreateEnvironment(EnvironmentViewData environmentViewData);

        bool DeleteEnvironment(Guid environmentId);

        bool DeleteAnimal(Guid animalId);
    }
}