namespace ZoosManagementSystem.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Data.EntityClient;

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

        public Environment GetEnvironment(Guid guid)
        {
            return new Environment();
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
