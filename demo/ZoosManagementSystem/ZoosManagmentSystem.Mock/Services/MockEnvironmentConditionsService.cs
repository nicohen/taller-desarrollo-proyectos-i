using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZoosManagementSystem.Interfaces;
using ZoosManagementSystem.Interfaces.DataContracts;

namespace ZoosManagmentSystem.Mock.Services
{
    public class MockEnvironmentConditionsService : IEnvironmentConditionsService
    {
        #region IEnvironmentConditionsService Members

        public EnvironmentConditions GetEnvironmentConditions(Guid environmentId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
