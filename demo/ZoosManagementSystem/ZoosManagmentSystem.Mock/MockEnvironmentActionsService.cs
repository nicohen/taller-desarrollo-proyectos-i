using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using ZoosManagementSystem.Interfaces;

namespace ZoosManagmentSystem.Mock
{
    public class MockEnvironmentActionsService : IEnvironmentActionsService
    {
        #region IEnvironmentActionsService Members

        public void ModifyTemperature(Guid environmentId, float temperatureOffset)
        {
            throw new NotImplementedException();
        }

        public void ModifyLuminosity(Guid environmentId, float luminosityOffset)
        {
            throw new NotImplementedException();
        }

        public void StartWatering(Guid environmentId)
        {
            throw new NotImplementedException();
        }

        public void StopWatering(Guid environmentId)
        {
            throw new NotImplementedException();
        }

        public void FeedingAnimal(Guid environmentId, Guid animalId, Guid feedingId, int amount)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
