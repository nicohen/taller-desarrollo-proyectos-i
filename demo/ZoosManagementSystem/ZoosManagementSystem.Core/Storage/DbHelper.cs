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

        #endregion
    }
}
