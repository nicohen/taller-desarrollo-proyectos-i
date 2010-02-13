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
            MainForm.LogInfo(string.Format("Received temperature modification request. Offset: {0}", temperatureOffset));
            EnvironmentSimulator.SetTemperature(environmentId, temperatureOffset);
        }

        public void ModifyLuminosity(Guid environmentId, float luminosityOffset)
        {
            MainForm.LogInfo(string.Format("Received luminosity modification request. Offset: {0}", luminosityOffset));
            EnvironmentSimulator.SetLuminosity(environmentId, luminosityOffset);
        }

        public void ModifyHumidity(Guid environmentId, float humidityOffset)
        {
            MainForm.LogInfo(string.Format("Received humidity modification request. Offset: {0}", humidityOffset));
            EnvironmentSimulator.SetHumidity(environmentId, humidityOffset);
        }

       
        public void FeedAnimal(Guid environmentId, Guid animalId, Guid feedingId, int amount)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
