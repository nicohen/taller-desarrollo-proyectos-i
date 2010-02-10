using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZoosManagmentSystem.Core.Foundation;
using ZoosManagementSystem.Core.MockEnvironmentActionsService;
using ZoosManagementSystem.Core.MockEnvironmentConditionsService;

namespace ZoosManagmentSystem.Core.Switch
{
    public class MockSensorManager : SensorManager
    {
        #region Fields

        EnvironmentActionsServiceClient environmentActionsClient;
        EnvironmentConditionsServiceClient environmentConditionsServiceClient;

        #endregion

        #region Methods

        public MockSensorManager()
            : base("MockSensorManager", 3000)
        {
            this.environmentActionsClient = new EnvironmentActionsServiceClient();
            this.environmentConditionsServiceClient = new EnvironmentConditionsServiceClient();
        }

        public override void Start()
        {

        }

        protected override void ProcessData()
        {
            base.ProcessData();
        }

        #endregion
    }
}
