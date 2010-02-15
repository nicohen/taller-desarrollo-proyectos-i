using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using ZoosManagementSystem.Interfaces;
using ZoosManagmentSystem.Mock.EnvironmentEmulation;
using ZoosManagmentSystem.Mock.Storage;

namespace ZoosManagmentSystem.Mock.Services
{
    public class MockEnvironmentActionsService : IEnvironmentActionsService
    {
        #region IEnvironmentActionsService Members

        public void ModifyTemperature(Guid environmentId, float temperatureOffset)
        {
            MainForm.LogInfo(string.Format("Recibida solicitud de modificación de temperatura. Offset: {0}", temperatureOffset));
            EnvironmentSimulator.SetTemperature(environmentId, temperatureOffset);
        }

        public void ModifyLuminosity(Guid environmentId, float luminosityOffset)
        {
            MainForm.LogInfo(string.Format("Recibida solicitud de modificación de luminosidad. Offset: {0}", luminosityOffset));
            EnvironmentSimulator.SetLuminosity(environmentId, luminosityOffset);
        }

        public void ModifyHumidity(Guid environmentId, float humidityOffset)
        {
            MainForm.LogInfo(string.Format("Recibida solicitud de modificación de humedad. Offset: {0}", humidityOffset));
            EnvironmentSimulator.SetHumidity(environmentId, humidityOffset);
        }

        public void FeedAnimal(Guid environmentId, Guid animalId, Guid feedingId, int amount)
        {
            Animal animal = DbHelper.GetAnimal(animalId);
            Feeding feeding = DbHelper.GetFeeding(feedingId);
            MainForm.LogInfo(string.Format("Recibida solicitud de alimentación. Animal: {0}." + System.Environment.NewLine + "Cantidad: {1}." + System.Environment.NewLine + " Comida: {2}", animal.Name, amount, feeding.Name));
        }

        #endregion
    }
}
