using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using ZoosManagementSystem.Interfaces;
using ZoosManagmentSystem.Mock.EnvironmentEmulation;

namespace ZoosManagmentSystem.Mock.Services
{
    public class MockEnvironmentActionsService : IEnvironmentActionsService
    {
        #region IEnvironmentActionsService Members

        public void ModifyTemperature(Guid environmentId, float temperatureOffset)
        {
            EnvironmentSimulator.SetTemperature(environmentId, temperatureOffset);
        }

        public void ModifyLuminosity(Guid environmentId, float luminosityOffset)
        {
            EnvironmentSimulator.SetLuminosity(environmentId,luminosityOffset);
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
