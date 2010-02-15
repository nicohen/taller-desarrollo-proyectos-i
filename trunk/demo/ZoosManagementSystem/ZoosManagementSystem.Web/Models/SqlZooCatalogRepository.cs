﻿namespace ZoosManagementSystem.Web.Models
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
                    .Where(e => e.Id == environmentId)
                    .FirstOrDefault();
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

                entities.SaveChanges();
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

                    entities.SaveChanges();
                    entities.DeleteObject(environment);
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
    }
}