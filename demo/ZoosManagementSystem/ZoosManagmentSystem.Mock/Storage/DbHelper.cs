using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoosManagmentSystem.Mock.Storage
{
    public class DbHelper
    {
        #region Fields

        private static ZoosManagementSystemEntities entities;

        #endregion

        #region Methods

        static DbHelper()
        {
            DbHelper.entities = new ZoosManagementSystemEntities();
        }

        #region Sensor

        public static List<Sensor> GetAllSensors()
        {
            var query = from e in DbHelper.entities.Sensor
                        select e;

            return query.ToList();
        }

        #endregion

        #region Environment

        public static List<Environment> GetEnvironments()
        {
            var query = from e in DbHelper.entities.Environment
                        select e;

            return query.ToList();
        }

        public static List<Animal> GetAllAnimals()
        {
            var query = from e in DbHelper.entities.Animal
                        select e;

            return query.ToList();
        }

        #endregion

        #region Animal

        public static List<Animal> GetAnimalsFromEnvironment(Guid environmentId)
        {
            var query = from e in DbHelper.entities.Animal
                        where e.Environment.Id == environmentId
                        select e;

            return query.ToList();
        }

        public static Animal GetAnimal(Guid animalId)
        {
            var query = from e in DbHelper.entities.Animal
                        where e.Id == animalId
                        select e;

            return query.First();
        }

        #endregion

        #region FeedingTime

        public List<FeedingTime> GetFeedingTimesFromAnimal(Guid animalId)
        {
            var query = from e in DbHelper.entities.FeedingTime.Include("Animal.Environment")
                        where e.Animal.Id == animalId
                        select e;

            return query.ToList();
        }

        #endregion

        #region Feeding

        public static Feeding GetFeeding(Guid feedingId)
        {
            var query = from e in DbHelper.entities.Feeding
                        where e.Id == feedingId
                        select e;

            return query.First();
        }

        #endregion

        #endregion
    }
}
