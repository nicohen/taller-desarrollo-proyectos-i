using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoosManagementSystem.Core.Storage
{
    public class DbHelper
    {
        #region Fields

        private ZoosManagementSystemEntities entities;

        #endregion

        #region Methods

        public DbHelper()
        {
            this.entities = new ZoosManagementSystemEntities();
        }

        #region Sensor

        public List<Sensor> GetSensors()
        {
            var query = from e in this.entities.Sensor
                        select e;

            return query.ToList();
        }

        #endregion

        #region Environment

        public List<Environment> GetEnvironments()
        {
            var query = from e in this.entities.Environment
                        select e;

            return query.ToList();
        }

        #endregion

        #region Feeding

        #endregion

        #region FeedingTime

        public List<FeedingTime> GetFeedingTimesFromAnimal(Guid animalId)
        {
            var query = from e in this.entities.FeedingTime
                        where e.Animal.Id == animalId
                        select e;

            return query.ToList();
        }

        #endregion

        #region Environment Measures

        public void UpdateEnvironmentMeasures(Guid environmentId, float humidity, float temperature, float luminosity)
        {
            EnvironmentMeasure environmentMeasure = this.entities.EnvironmentMeasure.First(e => e.Environment.Id == environmentId);

            if (environmentMeasure != null)
            {
                environmentMeasure.Humidity = humidity;
                environmentMeasure.Temperature = temperature;
                environmentMeasure.Luminosity = luminosity;

                this.entities.SaveChanges();
            }
            else
            {
                EnvironmentMeasure newEnvironmentMeasure = new EnvironmentMeasure();

                newEnvironmentMeasure.Humidity = humidity;
                newEnvironmentMeasure.Temperature = temperature;
                newEnvironmentMeasure.Luminosity = luminosity;

                this.entities.AddToEnvironmentMeasure(newEnvironmentMeasure);
                this.entities.SaveChanges();
            }
        }

        #endregion


        #region TimeSlot

        public List<TimeSlot> GetEnvironmentTimeSlots(Guid environmentId)
        {
            var query = from e in this.entities.TimeSlot
                        where e.Environment.Id == environmentId
                        select e;

            return query.ToList();
        }

        #endregion

        #region Animal

        public List<Animal> GetAnimalsFromEnvironment(Guid environmentId)
        {
            var query = from e in this.entities.Animal
                        where e.Environment.Id == environmentId
                        select e;

            return query.ToList();
        }

        #endregion

        #endregion
    }
}
