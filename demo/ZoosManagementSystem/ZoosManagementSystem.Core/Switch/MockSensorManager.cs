using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZoosManagmentSystem.Core.Foundation;
using ZoosManagementSystem.Core.MockEnvironmentActionsService;

namespace ZoosManagmentSystem.Core.Switch
{
    public class MockSensorManager : SensorManager
    {
        public MockSensorManager()
            : base("MockSensorManager", 3000)
        {
            EnvironmentActionsServiceClient environmentActionsClient = new EnvironmentActionsServiceClient();

        }

        public override void Start()
        {

        }

        protected override void ProcessData()
        {
            base.ProcessData();
        }
    }
}
