namespace ZoosManagementSystem.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Data.EntityClient;

    using ViewData;

    public class SqlZooCatalogRepository : IZooCatalogRepository, IDisposable
    {
        private EntityConnection connection;
        private bool isDisposed;

        public SqlZooCatalogRepository()
        {
        }

        public SqlZooCatalogRepository(string connectionString)
        {
            this.connection = new EntityConnection(connectionString);
            this.connection.Open();
        }

        private ZoosManagementSystemEntities EntityContext
        {
            get
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException("The Zoo catalog repository object has been disposed.");
                }

                return this.connection == null
                           ? new ZoosManagementSystemEntities()
                           : new ZoosManagementSystemEntities(this.connection);
            }
        }

        public IList<Feeding> GetFeedings()
        {
            using (var entities = this.EntityContext)
            {
                return entities.Feeding
                    .Include("FeedingTime")
                    .OrderBy(f => f.Name)
                    .ToList();
            }
        }

        public IList<Animal> GetAnimals()
        {
            using (var entities = this.EntityContext)
            {
                var animals = entities.Animal
                    .Include("Environment")
                    .Include("FeedingTime")
                    .Include("Responsible")
                    .Include("HealthMeasure")
                    .OrderBy(env => env.Species)
                    .ThenBy(env => env.Name)
                    .ToList();

                foreach (var animal in animals)
                {
                    foreach (var feedingTime in animal.FeedingTime)
                    {
                        if (!feedingTime.FeedingReference.IsLoaded)
                        {
                            feedingTime.FeedingReference.Load();
                        }
                    }
                }

                return animals.ToList();
            }
        }

        public IList<Responsible> GetResponsibles()
        {
            using (var entities = this.EntityContext)
            {
                return entities.Responsible
                    .Include("Animal")
                    .OrderBy(env => env.LastName)
                    .ThenBy(env => env.Name)
                    .ToList();
            }
        }

        public HealthMeasure GetHealthMeasure(Guid healthMeasureId)
        {
            using (var entities = this.EntityContext)
            {
                return entities.HealthMeasure
                    .Include("Animal")
                    .OrderBy(hm => hm.MeasurementDate)
                    .FirstOrDefault(hm => hm.Id == healthMeasureId);
            }
        }

        public IList<Environment> GetEnvironments()
        {
            using (var entities = this.EntityContext)
            {
                return entities.Environment
                    .Include("Animal")
                    .Include("Sensor")
                    .Include("TimeSlot")
                    .Include("EnvironmentMeasure")
                    .OrderBy(env => env.Name)
                    .ToList();
            }
        }

        public Environment GetEnvironment(Guid environmentId)
        {
            using (var entities = this.EntityContext)
            {
                return entities.Environment
                    .Include("Animal")
                    .Include("Sensor")
                    .Include("TimeSlot")
                    .Include("EnvironmentMeasure")
                    .FirstOrDefault(e => e.Id == environmentId);
            }
        }

        public Animal GetAnimal(Guid animalId)
        {
            using (var entities = this.EntityContext)
            {
                var animal = entities.Animal
                    .Include("Environment")
                    .Include("FeedingTime")
                    .Include("Responsible")
                    .Include("HealthMeasure")
                    .FirstOrDefault(a => a.Id == animalId);

                if (animal != null)
                {
                    foreach (var feedingTime in animal.FeedingTime)
                    {
                        if (!feedingTime.FeedingReference.IsLoaded)
                        {
                            feedingTime.FeedingReference.Load();
                        }
                    }
                }

                return animal;
            }
        }

        public IList<Animal> GetFreeAnimals()
        {
            using (var entities = this.EntityContext)
            {
                return entities.Animal
                    .Include("Environment")
                    .Include("FeedingTime")
                    .Include("Responsible")
                    .Where(a => a.Environment == null)
                    .ToList();
            }
        }

        public IList<Environment> SearchEnvironments(string searchCriteria)
        {
            using (var entities = this.EntityContext)
            {
                var filteredEnvironments = entities.Environment
                    .Include("Animal")
                    .Include("TimeSlot")
                    .OrderBy(env => env.Name)
                    .ToList();

                foreach (var criteria in searchCriteria.ToLowerInvariant().Split())
                {
                    filteredEnvironments
                        = filteredEnvironments
                            .Where(env => env.Id.ToString().ToLowerInvariant() == criteria
                                          || env.Name.ToLowerInvariant().Contains(criteria)
                                          || env.Description.ToLowerInvariant().Contains(criteria)
                                          || env.Type.ToLowerInvariant().Contains(criteria)
                                          || env.Surface.ToString(CultureInfo.CurrentCulture) == criteria
                                          || env.Animal.Any(a => a.Name.ToLowerInvariant().Contains(criteria)
                                                                 || a.Species.ToLowerInvariant().Contains(criteria)))
                            .ToList();
                }

                return filteredEnvironments;
            }
        }

        public IList<Animal> SearchAnimals(string searchCriteria)
        {
            using (var entities = this.EntityContext)
            {
                var filteredAnimals = entities.Animal
                    .Include("Environment")
                    .Include("FeedingTime")
                    .Include("Responsible")
                    .Include("HealthMeasure")
                    .ToList();

                foreach (var animal in filteredAnimals)
                {
                    foreach (var feedingTime in animal.FeedingTime)
                    {
                        if (!feedingTime.FeedingReference.IsLoaded)
                        {
                            feedingTime.FeedingReference.Load();
                        }
                    }
                }

                foreach (var criteria in searchCriteria.ToLowerInvariant().Split())
                {
                    filteredAnimals = filteredAnimals
                            .Where(a => a.Id.ToString().ToLowerInvariant() == criteria
                                          || a.Name.ToLowerInvariant().Contains(criteria)
                                          || a.Species.ToLowerInvariant().Contains(criteria)
                                          || a.BirthDate.ToString("yyyy/MM/dd").Contains(criteria)
                                          || a.Responsible.Name.ToLowerInvariant().Contains(criteria)
                                          || a.Responsible.LastName.ToLowerInvariant().Contains(criteria)
                                          || a.Responsible.Email.ToLowerInvariant().Contains(criteria)
                                          || a.NextHealthMeasure.ToString("yyyy/MM/dd").Contains(criteria))
                            .ToList();
                }

                return filteredAnimals
                    .OrderBy(a => a.Species)
                    .ThenBy(a => a.Name)
                    .ToList();
            }
        }

        public void UpdateEnvironment(EnvironmentViewData environmentViewData)
        {
            using (var entities = this.EntityContext)
            {
                var id = new Guid(environmentViewData.EnvironmentId);
                var environmentEntity = entities.Environment
                    .Include("Animal")
                    .Include("Sensor")
                    .Include("TimeSlot")
                    .Include("EnvironmentMeasure")
                    .Where(e => e.Id == id)
                    .FirstOrDefault();

                if (environmentEntity == null)
                {
                    throw new ArgumentException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            "No existe ningún ambiente con el Id {0} para actualizar.",
                            environmentViewData.EnvironmentId));
                }

                this.SaveOrUpdateEnvironment(environmentViewData, environmentEntity, entities);

                entities.SaveChanges();
            }
        }

        public void UpdateAnimal(AnimalViewData animalViewData)
        {
            using (var entities = this.EntityContext)
            {
                var id = new Guid(animalViewData.AnimalId);
                var animalEntity = entities.Animal
                    .Include("Environment")
                    .Include("FeedingTime")
                    .Include("Responsible")
                    .Include("HealthMeasure")
                    .FirstOrDefault(e => e.Id == id);

                if (animalEntity == null)
                {
                    throw new ArgumentException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            "No existe ningún animal con el Id {0} para actualizar.",
                            animalViewData.EnvironmentId));
                }

                this.SaveOrUpdateAnimal(animalViewData, animalEntity, entities);

                entities.SaveChanges();
            }
        }

        public void UpdateHealthMeasure(HealthMeasureViewData healthMeasureViewData)
        {
            using (var entities = this.EntityContext)
            {
                var id = new Guid(healthMeasureViewData.HealthMeasureId);
                var healthMeasureEntity = entities.HealthMeasure
                    .Include("Animal")
                    .FirstOrDefault(e => e.Id == id);

                if (healthMeasureEntity == null)
                {
                    throw new ArgumentException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            "No existe ningún examen de salud con el Id {0} para actualizar.",
                            healthMeasureViewData.HealthMeasureId));
                }

                this.SaveOrUpdateHealthMeasure(healthMeasureViewData, healthMeasureEntity, entities);

                entities.SaveChanges();
            }
        }

        public Guid CreateEnvironment(EnvironmentViewData environmentViewData)
        {
            using (var entities = this.EntityContext)
            {
                var environmentEntity = new Environment { Id = Guid.NewGuid() };

                entities.AddToEnvironment(environmentEntity);
                this.SaveOrUpdateEnvironment(environmentViewData, environmentEntity, entities);
                entities.SaveChanges();
                
                return environmentEntity.Id;
            }
        }

        public Guid CreateHealthMeasure(HealthMeasureViewData healthMeasureViewData)
        {
            using (var entities = this.EntityContext)
            {
                var healthMeasureEntity = new HealthMeasure { Id = Guid.NewGuid() };

                entities.AddToHealthMeasure(healthMeasureEntity);
                this.SaveOrUpdateHealthMeasure(healthMeasureViewData, healthMeasureEntity, entities);
                entities.SaveChanges();

                return healthMeasureEntity.Id;
            }
        }

        public Guid CreateAnimal(AnimalViewData animalViewData)
        {
            using (var entities = this.EntityContext)
            {
                var animalEntity = new Animal { Id = Guid.NewGuid() };

                entities.AddToAnimal(animalEntity);
                this.SaveOrUpdateAnimal(animalViewData, animalEntity, entities);
                entities.SaveChanges();

                return animalEntity.Id;
            }
        }

        public bool DeleteEnvironment(Guid environmentId)
        {
            using (var entities = this.EntityContext)
            {
                var environment = entities.Environment
                    .Include("Animal")
                    .Include("Sensor")
                    .Include("TimeSlot")
                    .Include("EnvironmentMeasure")
                    .FirstOrDefault(env => env.Id == environmentId);

                if (environment != null)
                {
                    foreach (var animal in environment.Animal.ToList())
                    {
                        animal.Environment = null;
                    }

                    foreach (var measure in environment.EnvironmentMeasure.ToList())
                    {
                        entities.DeleteObject(measure);
                    }

                    foreach (var sensor in environment.Sensor.ToList())
                    {
                        entities.DeleteObject(sensor);
                    }

                    foreach (var timeSlot in environment.TimeSlot.ToList())
                    {
                        entities.DeleteObject(timeSlot);
                    }

                    entities.DeleteObject(environment);
                    entities.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        public bool DeleteAnimal(Guid animalId)
        {
            using (var entities = this.EntityContext)
            {
                var animal = entities.Animal
                    .Include("Environment")
                    .Include("FeedingTime")
                    .Include("Responsible")
                    .Include("HealthMeasure")
                    .FirstOrDefault(a => a.Id == animalId);

                if (animal != null)
                {
                    foreach (var feedingTime in animal.FeedingTime.ToList())
                    {
                        entities.DeleteObject(feedingTime);
                    }

                    foreach (var healthMeasure in animal.HealthMeasure.ToList())
                    {
                        entities.DeleteObject(healthMeasure);
                    }

                    entities.DeleteObject(animal);
                    entities.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (this.connection != null)
            {
                this.connection.Dispose();
                this.connection = null;
            }

            this.isDisposed = true;
        }


        private void SaveOrUpdateEnvironment(EnvironmentViewData environmentViewData, Environment environmentEntity, ZoosManagementSystemEntities entities)
        {
            environmentEntity.Name = environmentViewData.Name;
            environmentEntity.Description = environmentViewData.Description;
            environmentEntity.Surface = environmentViewData.Surface;
            environmentEntity.Type = environmentViewData.Type;

            foreach (var animal in environmentViewData.Animals.Where(a => !a.AnimalStatus.Equals("None", StringComparison.InvariantCultureIgnoreCase) && !a.AnimalStatus.Equals("Original", StringComparison.InvariantCultureIgnoreCase)))
            {
                var animalId = new Guid(animal.AnimalId);
                var animalEntity = entities.Animal
                    .Include("Environment")
                    .Where(a => a.Id == animalId)
                    .FirstOrDefault();

                if (animalEntity == null)
                {
                    throw new ArgumentException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            "No existe ningún animal con el Id {0} para actualizar.",
                            animal.AnimalId));
                }

                animalEntity.Environment = animal.AnimalStatus.Equals(
                                               "Remove", StringComparison.InvariantCultureIgnoreCase)
                                               ? null
                                               : environmentEntity;
            }

            foreach (var timeSlot in environmentViewData.TimeSlots.Where(ts => !ts.TimeSlotStatus.Equals("None", StringComparison.InvariantCultureIgnoreCase)))
            {
                TimeSlot timeSlotEntity = null;
                if (timeSlot.TimeSlotStatus.Equals("New", StringComparison.InvariantCultureIgnoreCase))
                {
                    timeSlotEntity = new TimeSlot
                    {
                        Environment = environmentEntity,
                        Id = Guid.NewGuid()
                    };
                    entities.AddToTimeSlot(timeSlotEntity);
                }
                else
                {
                    var timeSlotId = new Guid(timeSlot.TimeSlotId);
                    timeSlotEntity = entities.TimeSlot
                        .Include("Environment")
                        .Where(ts => ts.Id == timeSlotId)
                        .FirstOrDefault();

                    if (timeSlotEntity == null)
                    {
                        throw new ArgumentException(
                            string.Format(
                                CultureInfo.CurrentCulture,
                                "No existe ningún intervalo de tiempo con el Id {0} para actualizar.",
                                timeSlot.TimeSlotId));
                    }
                }

                if (timeSlot.TimeSlotStatus.Equals("Remove", StringComparison.InvariantCultureIgnoreCase))
                {
                    entities.DeleteObject(timeSlotEntity);
                }
                else
                {
                    timeSlotEntity.InitialTime = TimeSpan.Parse(timeSlot.InitialTime);
                    timeSlotEntity.FinalTime = TimeSpan.Parse(timeSlot.FinalTime);
                    timeSlotEntity.TemperatureMin = timeSlot.TemperatureMin;
                    timeSlotEntity.TemperatureMax = timeSlot.TemperatureMax;
                    timeSlotEntity.HumidityMin = timeSlot.HumidityMin;
                    timeSlotEntity.HumidityMax = timeSlot.HumidityMax;
                    timeSlotEntity.LuminosityMin = timeSlot.LuminosityMin;
                    timeSlotEntity.LuminosityMax = timeSlot.LuminosityMax;
                }
            }
        }

        private void SaveOrUpdateAnimal(AnimalViewData animalViewData, Animal animalEntity, ZoosManagementSystemEntities entities)
        {
            animalEntity.Name = animalViewData.Name;
            animalEntity.Species = animalViewData.Species;
            animalEntity.Sex = (animalViewData.Sex.ToLowerInvariant() == "macho") ? "M" : "F";
            animalEntity.Cost = animalViewData.Cost;
            animalEntity.BornInCaptivity = animalViewData.BornInCaptivity;
            animalEntity.BirthDate = DateTime.Parse(animalViewData.BirthDate);
            animalEntity.NextHealthMeasure = DateTime.Parse(animalViewData.NextHealthMeasure);

            var responsibleId = new Guid(animalViewData.ResponsibleId);
            animalEntity.Responsible = entities.Responsible.FirstOrDefault(r => r.Id == responsibleId);
            
            if (!string.IsNullOrEmpty(animalViewData.EnvironmentId))
            {
                var environmentId = new Guid(animalViewData.EnvironmentId);
                animalEntity.Environment = entities.Environment.FirstOrDefault(r => r.Id == environmentId);
            }
            else
            {
                animalEntity.Environment = null;
            }

            foreach (var feedingTime in animalViewData.FeedingTimes.Where(ft => !ft.FeedingTimeStatus.Equals("None", StringComparison.InvariantCultureIgnoreCase)))
            {
                FeedingTime feedingTimeEntity = null;
                if (feedingTime.FeedingTimeStatus.Equals("New", StringComparison.InvariantCultureIgnoreCase))
                {
                    feedingTimeEntity = new FeedingTime
                        {
                            Id = Guid.NewGuid(),
                            Animal = animalEntity
                        }; 

                    entities.AddToFeedingTime(feedingTimeEntity);
                }
                else
                {
                    var feedingTimeId = new Guid(feedingTime.FeedingTimeId);
                    feedingTimeEntity = entities.FeedingTime
                        .Include("Feeding")
                        .Where(ft => ft.Id == feedingTimeId)
                        .FirstOrDefault();

                    if (feedingTimeEntity == null)
                    {
                        throw new ArgumentException(
                            string.Format(
                                CultureInfo.CurrentCulture,
                                "No existe ningún horario de alimentación con el Id {0} para actualizar.",
                                feedingTime.FeedingTimeId));
                    }
                }

                if (feedingTime.FeedingTimeStatus.Equals("Remove", StringComparison.InvariantCultureIgnoreCase))
                {
                    entities.DeleteObject(feedingTimeEntity);
                }
                else
                {
                    var feedingId = new Guid(feedingTime.FeedingId);

                    feedingTimeEntity.Amount = feedingTime.Amount;
                    feedingTimeEntity.Time = TimeSpan.Parse(feedingTime.Time);
                    feedingTimeEntity.Feeding = entities.Feeding.FirstOrDefault(f => f.Id == feedingId);
                }
            }
        }

        private void SaveOrUpdateHealthMeasure(HealthMeasureViewData healthMeasureViewData, HealthMeasure healthMeasureEntity, ZoosManagementSystemEntities entities)
        {
            healthMeasureEntity.MeasurementDate = DateTime.Parse(healthMeasureViewData.MeasurementDate);
            healthMeasureEntity.Height = healthMeasureViewData.Height;
            healthMeasureEntity.Weight = healthMeasureViewData.Weight;
            healthMeasureEntity.Temperature = healthMeasureViewData.Temperature;
            healthMeasureEntity.Notes = healthMeasureViewData.Notes;
            healthMeasureEntity.Vaccine = healthMeasureViewData.Vaccine;

            var animalId = new Guid(healthMeasureViewData.AnimalId);
            healthMeasureEntity.Animal = entities.Animal.FirstOrDefault(a => a.Id == animalId);
        }
    }
}
