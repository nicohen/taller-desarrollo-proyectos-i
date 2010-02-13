using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZoosManagementSystem.Interfaces;
using ZoosManagementSystem.Interfaces.DataContracts;
using ZoosManagmentSystem.Mock.Storage;
using ZoosManagmentSystem.Mock.EnvironmentEmulation;

namespace ZoosManagmentSystem.Mock.Services
{
    public class MockEnvironmentConditionsService : IEnvironmentConditionsService
    {
        #region IEnvironmentConditionsService Members

        public EnvironmentConditions GetEnvironmentConditions(Guid environmentId)
        {
            EnvironmentMeasure measures = EnvironmentSimulator.GetEnvironmentMeasures(environmentId);

            EnvironmentConditions conditions = new EnvironmentConditions()
            {
                EnvironmentId = environmentId,
                Humidity = (float)measures.Humidity,
                Luminosity = (float)measures.Luminosity,
                MeasureTime = DateTime.Now,
                Temperature = (float)measures.Temperature
            };

            return conditions;
        }

        #endregion
    }
}
